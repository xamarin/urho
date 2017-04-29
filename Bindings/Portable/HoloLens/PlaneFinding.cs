using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Urho.SharpReality.HoloToolkit
{
	[StructLayout(LayoutKind.Sequential)]
	public struct OrientedBoundingBox
	{
		public Vector3 Center;
		public Vector3 Extents;
		public Quaternion Rotation;
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct BoundedPlane
	{
		public Plane Plane;
		public OrientedBoundingBox Bounds;
		public float Area;
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3
	{
		public float x;
		public float y;
		public float z;

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public float Length()
		{
			return (float)Math.Sqrt(x * x + y * y + z * z);
		}

		public override string ToString()
		{
			return string.Format("{{{0}, {1}, {2}}}", x, y, z);
		}

		public string ToString(string format)
		{
			return string.Format("{{{0}, {1}, {2}}}", x.ToString(format), y.ToString(format), z.ToString(format));
		}

		public static readonly Vector3 zero = new Vector3(0, 0, 0);
		public static readonly Vector3 one = new Vector3(1, 1, 1);
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Quaternion
	{
		public float x;
		public float y;
		public float z;
		public float w;

		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public static readonly Quaternion identity = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix4x4
	{
		public float m00; public float m10; public float m20; public float m30;
		public float m01; public float m11; public float m21; public float m31;
		public float m02; public float m12; public float m22; public float m32;
		public float m03; public float m13; public float m23; public float m33;

		public Matrix4x4(
			float m00, float m01, float m02, float m03,
			float m10, float m11, float m12, float m13,
			float m20, float m21, float m22, float m23,
			float m30, float m31, float m32, float m33)
		{
			this.m00 = m00; this.m01 = m01; this.m02 = m02; this.m03 = m03;
			this.m10 = m10; this.m11 = m11; this.m12 = m12; this.m13 = m13;
			this.m20 = m20; this.m21 = m21; this.m22 = m22; this.m23 = m23;
			this.m30 = m30; this.m31 = m31; this.m32 = m32; this.m33 = m33;
		}

		public Vector3 TransformDirection(Vector3 v)
		{
			return new Vector3(
				v.x * m00 + v.y * m01 + v.z * m02,
				v.x * m10 + v.y * m11 + v.z * m12,
				v.x * m20 + v.y * m21 + v.z * m22);
		}

		public Vector3 TransformPoint(Vector3 v)
		{
			return TransformDirection(v) + new Vector3(m03, m13, m23);
		}

		public static readonly Matrix4x4 identity = new Matrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Plane
	{
		public Vector3 normal;
		public float distance;
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct Transform
	{
		public Matrix4x4 localToWorldMatrix;
		public Quaternion rotation;
		public Vector3 TransformPoint(Vector3 point)
		{
			return localToWorldMatrix.TransformPoint(point);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Bounds
	{
		public Vector3 center;
		public Vector3 extents;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Mesh
	{
		public Bounds bounds;
		public Vector3[] vertices;
		public Vector3[] normals;
		public int[] triangles;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MeshFilter
	{
		public Transform transform;
		public Mesh sharedMesh;
	}

	public class PlaneFinding
	{
		#region Public APIs

		/// <summary>
		/// PlaneFinding is an expensive task that should not be run from Unity's main thread as it
		/// will stall the thread and cause a framerate dip.  Instead, the PlaneFinding APIs should be
		/// exclusively called from background threads.  Unfortunately, Unity's built-in data types
		/// (such as MeshFilter) are not thread safe and cannot be accessed from background threads.
		/// The MeshData struct exists to work-around this limitation.  When you want to find planes
		/// in a collection of MeshFilter objects, start by constructing a list of MeshData structs
		/// from those MeshFilters. You can then take the resulting list of MeshData structs, and
		/// safely pass it to the FindPlanes() API from a background thread.
		/// </summary>
		public struct MeshData
		{
			public Matrix4x4 Transform;
			public Vector3[] Verts;
			public Vector3[] Normals;
			public Int32[] Indices;

			public MeshData(MeshFilter meshFilter)
			{
				Transform = meshFilter.transform.localToWorldMatrix;
				Verts = meshFilter.sharedMesh.vertices;
				Normals = meshFilter.sharedMesh.normals;
				Indices = meshFilter.sharedMesh.triangles;
			}
		}

		/// <summary>
		/// Finds small planar patches that are contained within individual meshes.  The output of this
		/// API can then be passed to MergeSubPlanes() in order to find larger planar surfaces that
		/// potentially span across multiple meshes.
		/// </summary>
		/// <param name="meshes">
		/// List of meshes to run the plane finding algorithm on.
		/// </param>
		/// <param name="snapToGravityThreshold">
		/// Planes whose normal vectors are within this threshold (in degrees) from vertical/horizontal
		/// will be snapped to be perfectly gravity aligned.  When set to something other than zero, the
		/// bounding boxes for each plane will be gravity aligned as well, rather than rotated for an
		/// optimally tight fit. Pass 0.0 for this parameter to completely disable the gravity alignment
		/// logic.
		/// </param>
		public static BoundedPlane[] FindSubPlanes(List<MeshData> meshes, float snapToGravityThreshold = 0.0f)
		{
			StartPlaneFinding();

			try
			{
				int planeCount;
				IntPtr planesPtr;
				IntPtr pinnedMeshData = PinMeshDataForMarshalling(meshes);
				DLLImports.FindSubPlanes(meshes.Count, pinnedMeshData, snapToGravityThreshold, out planeCount, out planesPtr);
				return MarshalBoundedPlanesFromIntPtr(planesPtr, planeCount);
			}
			finally
			{
				FinishPlaneFinding();
			}
		}

		/// <summary>
		/// Takes the subplanes returned by one or more previous calls to FindSubPlanes() and merges
		/// them together into larger planes that can potentially span across multiple meshes.
		/// Overlapping subplanes that have similar plane equations will be merged together to form
		/// larger planes.
		/// </summary>
		/// <param name="subPlanes">
		/// The output from one or more previous calls to FindSubPlanes().
		/// </param>
		/// <param name="snapToGravityThreshold">
		/// Planes whose normal vectors are within this threshold (in degrees) from vertical/horizontal
		/// will be snapped to be perfectly gravity aligned.  When set to something other than zero, the
		/// bounding boxes for each plane will be gravity aligned as well, rather than rotated for an
		/// optimally tight fit. Pass 0.0 for this parameter to completely disable the gravity alignment
		/// logic.
		/// </param>
		/// <param name="minArea">
		/// While merging subplanes together, any candidate merged plane whose constituent mesh
		/// triangles have a total area less than this threshold are ignored.
		/// </param>
		public static BoundedPlane[] MergeSubPlanes(BoundedPlane[] subPlanes, float snapToGravityThreshold = 0.0f, float minArea = 0.0f)
		{
			StartPlaneFinding();

			try
			{
				int planeCount;
				IntPtr planesPtr;
				DLLImports.MergeSubPlanes(subPlanes.Length, PinObject(subPlanes), minArea, snapToGravityThreshold, out planeCount, out planesPtr);
				return MarshalBoundedPlanesFromIntPtr(planesPtr, planeCount);
			}
			finally
			{
				FinishPlaneFinding();
			}
		}

		/// <summary>
		/// Convenience wrapper that executes FindSubPlanes followed by MergeSubPlanes via a single
		/// call into native code (which improves performance by avoiding a bunch of unnecessary data
		/// marshalling and a managed-to-native transition).
		/// </summary>
		/// <param name="meshes">
		/// List of meshes to run the plane finding algorithm on.
		/// </param>
		/// <param name="snapToGravityThreshold">
		/// Planes whose normal vectors are within this threshold (in degrees) from vertical/horizontal
		/// will be snapped to be perfectly gravity aligned.  When set to something other than zero, the
		/// bounding boxes for each plane will be gravity aligned as well, rather than rotated for an
		/// optimally tight fit. Pass 0.0 for this parameter to completely disable the gravity alignment
		/// logic.
		/// </param>
		/// <param name="minArea">
		/// While merging subplanes together, any candidate merged plane whose constituent mesh
		/// triangles have a total area less than this threshold are ignored.
		/// </param>
		public static BoundedPlane[] FindPlanes(List<MeshData> meshes, float snapToGravityThreshold = 0.0f, float minArea = 0.0f)
		{
			StartPlaneFinding();

			try
			{
				int planeCount;
				IntPtr planesPtr;
				IntPtr pinnedMeshData = PinMeshDataForMarshalling(meshes);
				DLLImports.FindPlanes(meshes.Count, pinnedMeshData, minArea, snapToGravityThreshold, out planeCount, out planesPtr);
				return MarshalBoundedPlanesFromIntPtr(planesPtr, planeCount);
			}
			finally
			{
				FinishPlaneFinding();
			}
		}

		#endregion

		#region Internal

		private static bool findPlanesRunning = false;
		private static System.Object findPlanesLock = new System.Object();
		private static DLLImports.MeshData[] reusedMeshesForMarshalling = null;
		private static List<GCHandle> reusedPinnedMemoryHandles = new List<GCHandle>();

		/// <summary>
		/// Validate that no other PlaneFinding API call is currently in progress. As a performance
		/// optimization to avoid unnecessarily thrashing the garbage collector, each call into the
		/// PlaneFinding DLL reuses a couple of static data stuctures. As a result, we can't handle
		/// multiple concurrent calls into these APIs.
		/// </summary>
		private static void StartPlaneFinding()
		{
			lock (findPlanesLock)
			{
				if (findPlanesRunning)
				{
					throw new Exception("PlaneFinding is already running. You can not call these APIs from multiple threads.");
				}
				findPlanesRunning = true;
			}
		}

		/// <summary>
		/// Cleanup after finishing a PlaneFinding API call by unpinning any memory that was pinned
		/// for the call into the driver, and then reset the findPlanesRunning bool.
		/// </summary>
		private static void FinishPlaneFinding()
		{
			UnpinAllObjects();
			findPlanesRunning = false;
		}

		/// <summary>
		/// Pins the specified object so that the backing memory can not be relocated, adds the pinned
		/// memory handle to the tracking list, and then returns that address of the pinned memory so
		/// that it can be passed into the DLL to be access directly from native code.
		/// </summary>
		private static IntPtr PinObject(System.Object obj)
		{
			GCHandle h = GCHandle.Alloc(obj, GCHandleType.Pinned);
			reusedPinnedMemoryHandles.Add(h);
			return h.AddrOfPinnedObject();
		}

		/// <summary>
		/// Unpins all of the memory previously pinned by calls to PinObject().
		/// </summary>
		private static void UnpinAllObjects()
		{
			for (int i = 0; i < reusedPinnedMemoryHandles.Count; ++i)
			{
				reusedPinnedMemoryHandles[i].Free();
			}
			reusedPinnedMemoryHandles.Clear();
		}

		/// <summary>
		/// Copies the supplied mesh data into the reusedMeshesForMarhsalling array. All managed arrays
		/// are pinned so that the marshalling only needs to pass a pointer and the native code can
		/// reference the memory in place without needing the marshaller to create a complete copy of
		/// the data.
		/// </summary>
		private static IntPtr PinMeshDataForMarshalling(List<MeshData> meshes)
		{
			// if we have a big enough array reuse it, otherwise create new
			if (reusedMeshesForMarshalling == null || reusedMeshesForMarshalling.Length < meshes.Count)
			{
				reusedMeshesForMarshalling = new DLLImports.MeshData[meshes.Count];
			}

			for (int i = 0; i < meshes.Count; ++i)
			{
				reusedMeshesForMarshalling[i] = new DLLImports.MeshData()
				{
					transform = meshes[i].Transform,
					vertCount = meshes[i].Verts.Length,
					indexCount = meshes[i].Indices.Length,
					verts = PinObject(meshes[i].Verts),
					normals = PinObject(meshes[i].Normals),
					indices = PinObject(meshes[i].Indices),
				};
			}

			return PinObject(reusedMeshesForMarshalling);
		}

		/// <summary>
		/// Marshals BoundedPlane data returned from a DLL API call into a managed BoundedPlane array
		/// and then frees the memory that was allocated within the DLL.
		/// </summary>
		/// <remarks>Disabling warning 618 when calling Marshal.SizeOf(), because
		/// Unity does not support .Net 4.5.1+ for using the preferred Marshal.SizeOf(T) method."/>, </remarks>
		private static BoundedPlane[] MarshalBoundedPlanesFromIntPtr(IntPtr outArray, int size)
		{
			BoundedPlane[] resArray = new BoundedPlane[size];
#pragma warning disable 618
			int structsize = Marshal.SizeOf(typeof(BoundedPlane));
#pragma warning restore 618
			IntPtr current = outArray;
			for (int i = 0; i < size; i++)
			{
#pragma warning disable 618
				resArray[i] = (BoundedPlane)Marshal.PtrToStructure(current, typeof(BoundedPlane));
#pragma warning restore 618
				current = (IntPtr)((long)current + structsize);
			}
			Marshal.FreeCoTaskMem(outArray);
			return resArray;
		}

		/// <summary>
		/// Raw PlaneFinding.dll imports
		/// </summary>
		private class DLLImports
		{
			[StructLayout(LayoutKind.Sequential)]
			public struct MeshData
			{
				public Matrix4x4 transform;
				public Int32 vertCount;
				public Int32 indexCount;
				public IntPtr verts;
				public IntPtr normals;
				public IntPtr indices;
			};

			[DllImport(Consts.NativeImport)]
			public static extern void FindPlanes(
				[In] int meshCount,
				[In] IntPtr meshes,
				[In] float minArea,
				[In] float snapToGravityThreshold,
				[Out] out int planeCount,
				[Out] out IntPtr planesPtr);

			[DllImport(Consts.NativeImport)]
			public static extern void FindSubPlanes(
				[In] int meshCount,
				[In] IntPtr meshes,
				[In] float snapToGravityThreshold,
				[Out] out int planeCount,
				[Out] out IntPtr planesPtr);

			[DllImport(Consts.NativeImport)]
			public static extern void MergeSubPlanes(
				[In] int subPlaneCount,
				[In] IntPtr subPlanes,
				[In] float minArea,
				[In] float snapToGravityThreshold,
				[Out] out int planeCount,
				[Out] out IntPtr planesPtr);
		}

		#endregion
	}
}