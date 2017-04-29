using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Graphics.DirectX;
using Windows.Perception.Spatial;
using Windows.Perception.Spatial.Surfaces;
using Urho.SharpReality;

namespace Urho
{
	public class SpatialMappingManager
	{
		SpatialSurfaceObserver observer;
		SpatialCoordinateSystem currentCoordinateSystem;
		readonly SemaphoreSlim spatialObserverSemaphore = new SemaphoreSlim(1);
		static readonly Dictionary<Guid, DateTimeOffset> UpdateCache = new Dictionary<Guid, DateTimeOffset>();
		StereoApplication currentHoloApp;
		int trianglesPerCubicMeter;

		public Color DefaultColor { get; set; } = Color.Transparent;
		public bool ConvertToLeftHanded { get; set; }
		public bool OnlyAdd { get; set; }

		SpatialSurfaceMeshOptions options = new SpatialSurfaceMeshOptions
			{
				//TODO: check if supported?
				VertexPositionFormat = DirectXPixelFormat.R32G32B32A32Float,
				VertexNormalFormat = DirectXPixelFormat.R32G32B32A32Float,
				TriangleIndexFormat = DirectXPixelFormat.R16UInt,
				IncludeVertexNormals = true,
			};

		public async Task<bool> Register(StereoApplication app, SpatialCoordinateSystem coordinateSystem, System.Numerics.Vector3 extents, int trianglesPerCubicMeter = 1000, bool onlyAdd = false, bool convertToLeftHanded = true)
		{
			this.currentHoloApp = app;
			this.trianglesPerCubicMeter = trianglesPerCubicMeter;
			this.currentCoordinateSystem = coordinateSystem;
			ConvertToLeftHanded = convertToLeftHanded;
			OnlyAdd = onlyAdd;

			var result = await SpatialSurfaceObserver.RequestAccessAsync();
			if (result != SpatialPerceptionAccessStatus.Allowed)
				return false;

			observer = new SpatialSurfaceObserver();
			observer.SetBoundingVolume(SpatialBoundingVolume.FromBox(coordinateSystem, new SpatialBoundingBox { Extents = extents }));

			foreach (var item in observer.GetObservedSurfaces())
			{
				lock (UpdateCache)
				{
					UpdateCache[item.Key] = item.Value.UpdateTime.ToUniversalTime();
				}
				ProcessSurface(item.Value);
			}
			observer.ObservedSurfacesChanged += Observer_ObservedSurfacesChanged;

			return true;
		}

		public void Stop()
		{
			if (observer != null)
				observer.ObservedSurfacesChanged -= Observer_ObservedSurfacesChanged;
			observer = null;
		}

		async void Observer_ObservedSurfacesChanged(SpatialSurfaceObserver sender, object args)
		{
			foreach (var item in sender.GetObservedSurfaces())
			{
				lock (UpdateCache)
				{
					DateTimeOffset updateTime;
					var time = item.Value.UpdateTime.ToUniversalTime();
					if (UpdateCache.TryGetValue(item.Key, out updateTime) && (updateTime >= time || OnlyAdd))
						continue;

					UpdateCache[item.Key] = time;
				}

				if (observer == null)
					return;

				await ProcessSurface(item.Value).ConfigureAwait(false);
			}
		}

		void RemoveSurfaceFromCache(Guid surfaceId)
		{
			lock (UpdateCache)
				UpdateCache.Remove(surfaceId);
		}

		async Task ProcessSurface(SpatialSurfaceInfo surface)
		{
			var mesh = await surface.TryComputeLatestMeshAsync(trianglesPerCubicMeter, options).AsTask().ConfigureAwait(false);
			if (observer == null || mesh == null)
			{
				RemoveSurfaceFromCache(surface.Id);
				return;
			}

			var bounds = mesh.SurfaceInfo.TryGetBounds(currentCoordinateSystem);
			if (bounds == null)
			{
				RemoveSurfaceFromCache(surface.Id);
				return;
			}

			var transform = mesh.CoordinateSystem.TryGetTransformTo(currentCoordinateSystem);
			if (transform == null)
			{
				RemoveSurfaceFromCache(surface.Id);
				return;
			}

			//1. TriangleIndices
			var trianglesBytes = mesh.TriangleIndices.Data.ToArray();
			var indeces = new short[mesh.TriangleIndices.ElementCount];
			int indexOffset = 0;
			for (int i = 0; i < mesh.TriangleIndices.ElementCount; i++)
			{
				//DirectXPixelFormat.R16UInt
				var index = BitConverter.ToInt16(trianglesBytes, indexOffset);
				indexOffset += 2;
				indeces[i] = index;
			}

			var vertexCount = mesh.VertexPositions.ElementCount;
			var vertexRawData = mesh.VertexPositions.Data.ToArray();
			var vertexScale = mesh.VertexPositionScale;
			var normalsRawData = mesh.VertexNormals.Data.ToArray();

			var vertexColor = DefaultColor.ToUInt();
			var vertexData = new SpatialVertex[vertexCount];

			var boundsRotation = new Quaternion(-bounds.Value.Orientation.X, -bounds.Value.Orientation.Y, bounds.Value.Orientation.Z, bounds.Value.Orientation.W);
			var boundsCenter = new Vector3(bounds.Value.Center.X, bounds.Value.Center.Y, -bounds.Value.Center.Z);
			var boundsExtents = new Vector3(bounds.Value.Extents.X, bounds.Value.Extents.Y, bounds.Value.Extents.Z);

			var transformValue = transform.Value;
			Matrix4 transformUrhoMatrix;
			unsafe { transformUrhoMatrix = *(Matrix4*)(void*)&transformValue; }

			//these values won't change, let's declare them as consts
			const int vertexStride = 16; // (int) mesh.VertexPositions.Stride;
			const int normalStride = 16; // (int) mesh.VertexNormals.Stride;

			//2,3 - VertexPositions and Normals
			for (int i = 0; i < vertexCount; i++)
			{
				var positionX = BitConverter.ToSingle(vertexRawData, i * vertexStride + 0); 
				var positionY = BitConverter.ToSingle(vertexRawData, i * vertexStride + 4); //4 per X,Y,Z,W (stride is 16)
				var positionZ = BitConverter.ToSingle(vertexRawData, i * vertexStride + 8); //also, we don't need the W component.

				var normalX = BitConverter.ToSingle(normalsRawData, i * normalStride + 0);
				var normalY = BitConverter.ToSingle(normalsRawData, i * normalStride + 4);
				var normalZ = BitConverter.ToSingle(normalsRawData, i * normalStride + 8);

				//merge vertex+normals for Urho3D (also, change RH to LH coordinate systems)
				vertexData[i].PositionX = positionX * vertexScale.X;
				vertexData[i].PositionY = positionY * vertexScale.Y;
				vertexData[i].PositionZ = - positionZ * vertexScale.Z;
				vertexData[i].NormalX = normalX;
				vertexData[i].NormalY = normalY;
				vertexData[i].NormalZ = - normalZ;

				//Vertex color (for VCol techniques)
				vertexData[i].ColorUint = vertexColor;
			}

			var surfaceInfo = new SharpReality.SpatialMeshInfo
			{
					SurfaceId = surface.Id.ToString(),
					Date = surface.UpdateTime,
					VertexData = vertexData,
					IndexData = indeces,
					BoundsCenter = boundsCenter,
					BoundsRotation = boundsRotation,
					Extents = boundsExtents, 
					Transform = transformUrhoMatrix,
				};
			currentHoloApp.HandleSurfaceUpdated(surfaceInfo);
		}
	}
}
