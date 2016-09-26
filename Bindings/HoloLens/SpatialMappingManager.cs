using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.DirectX;
using Windows.Perception.Spatial;
using Windows.Perception.Spatial.Surfaces;
using Urho.HoloLens;

namespace Urho
{
	public class SpatialMappingManager
	{
		SpatialSurfaceObserver observer;
		SpatialCoordinateSystem currentCoordinateSystem;
		static Dictionary<Guid, DateTimeOffset> updateCache = new Dictionary<Guid, DateTimeOffset>();
		HoloApplication currentHoloApp;
		int trianglesPerCubicMeter;

		public Color DefaultColor { get; set; } = Color.Transparent;
		public bool ConvertToLeftHanded { get; set; }
		public bool OnlyAdd { get; set; }

		SpatialSurfaceMeshOptions options = new SpatialSurfaceMeshOptions
			{
				//TODO: check if supported?
				VertexPositionFormat = DirectXPixelFormat.R16G16B16A16IntNormalized,
				VertexNormalFormat = DirectXPixelFormat.R8G8B8A8IntNormalized,
				TriangleIndexFormat = DirectXPixelFormat.R16UInt,
				IncludeVertexNormals = true,
			};

		public async Task<bool> Register(HoloApplication app, SpatialCoordinateSystem coordinateSystem, System.Numerics.Vector3 extents, int trianglesPerCubicMeter = 1000, bool onlyAdd = false, bool convertToLeftHanded = true)
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
				lock (updateCache)
				{
					updateCache[item.Key] = item.Value.UpdateTime.ToUniversalTime();
				}
				ProcessSurface(item.Value, ConvertToLeftHanded);
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
				lock (updateCache)
				{
					DateTimeOffset updateTime;
					var time = item.Value.UpdateTime.ToUniversalTime();
					if (updateCache.TryGetValue(item.Key, out updateTime) && (updateTime >= time || OnlyAdd))
						continue;

					updateCache[item.Key] = time;
				}

				if (observer == null)
					return;

				await ProcessSurface(item.Value, ConvertToLeftHanded).ConfigureAwait(false);
			}
		}

		void RemoveSurfaceFromCache(Guid surfaceId)
		{
			lock (updateCache)
				updateCache.Remove(surfaceId);
		}

		async Task ProcessSurface(SpatialSurfaceInfo surface, bool convertToLH)
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

			// convert from RH to LH coordinate system
			int rhToLh = convertToLH ? -1 : 1;

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

			//2,3 - VertexPositions and Normals
			for (int i = 0; i < vertexCount; i++)
			{
				//VertexPositions: DirectXPixelFormat.R16G16B16A16IntNormalized
				short x = BitConverter.ToInt16(vertexRawData, i * 8 + 0);
				short y = BitConverter.ToInt16(vertexRawData, i * 8 + 2);
				short z = BitConverter.ToInt16(vertexRawData, i * 8 + 4);

				//short to float:
				float xx = (x == -32768) ? -1.0f : x * 1.0f / (32767.0f);
				float yy = (y == -32768) ? -1.0f : y * 1.0f / (32767.0f);
				float zz = (z == -32768) ? -1.0f : z * 1.0f / (32767.0f);

				//Normals: DirectXPixelFormat.R8G8B8A8IntNormalized
				var normalX = normalsRawData[i * 4 + 0];
				var normalY = normalsRawData[i * 4 + 1];
				var normalZ = normalsRawData[i * 4 + 2];

				//merge vertex+normals for Urho3D (also, change RH to LH coordinate systems)
				vertexData[i].PositionX =  xx * vertexScale.X;
				vertexData[i].PositionY =  yy * vertexScale.Y;
				vertexData[i].PositionZ = rhToLh * zz * vertexScale.Z;
				vertexData[i].NormalX = normalX == 0 ? 0 :  255 / normalX;
				vertexData[i].NormalY = normalY == 0 ? 0 :  255 / normalY;
				vertexData[i].NormalZ = normalZ == 0 ? 0 : rhToLh * 255 / normalZ;

				//Vertex color
				vertexData[i].ColorUint = vertexColor;
			}

			var boundsRotation = new Quaternion(rhToLh * bounds.Value.Orientation.X, rhToLh * bounds.Value.Orientation.Y, bounds.Value.Orientation.Z, bounds.Value.Orientation.W);
			var boundsCenter = new Vector3(bounds.Value.Center.X, bounds.Value.Center.Y, rhToLh * bounds.Value.Center.Z);
			var boundsExtents = new Vector3(bounds.Value.Extents.X, bounds.Value.Extents.Y, bounds.Value.Extents.Z);

			var transformValue = transform.Value;
			Matrix4 transformUrhoMatrix;
			unsafe { transformUrhoMatrix = *(Matrix4*)(void*)&transformValue; }

			var surfaceInfo = new HoloLens.SpatialMeshInfo
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
