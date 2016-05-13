using System;
using System.Runtime.InteropServices;
using Urho.Network;

namespace Urho {
	public partial class Controls {
		// If null, we own the unmanaged value
		// This is not-null when this is created from a pointer to another object, so we use this to keep the other object alive.
		Connection connection;
		internal IntPtr handle;

		internal Controls (Connection connection, IntPtr handle)
		{
			this.connection = connection;
			this.handle = handle;
		}

		public Controls ()
		{
			Runtime.ValidateObject(this);
			handle = Controls_Create ();
			connection = null;
		}

		static void ReleaseControl (IntPtr h)
		{
			Controls_Destroy (h);
		}
		
		~Controls ()
		{
			if (connection == null){
				ReleaseControl (handle);
				handle = IntPtr.Zero;
			}
		}
		
		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static void Controls_Destroy (IntPtr handle);
		
		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr Controls_Create ();

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static uint Controls_GetButtons (IntPtr handle);

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static void Controls_SetButtons (IntPtr handle, uint value);

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static float Controls_GetYaw (IntPtr handle);

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static void Controls_SetYaw (IntPtr handle, float yaw);

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static float Controls_GetPitch (IntPtr handle);

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static void Controls_SetPitch (IntPtr handle, float yaw);
		
		public uint Buttons {
			get { return Controls_GetButtons (handle); }
			set { Controls_SetButtons (handle, value); }
		}

		public float Yaw {
			get { return Controls_GetYaw (handle); }
			set { Controls_SetYaw (handle, value); }
		}

		public float Pitch {
			get { return Controls_GetPitch (handle); }
			set { Controls_SetPitch (handle, value); }
		}

		public bool IsPressed (uint button, ref Controls previousControls)
		{
			Runtime.ValidateObject(this);
			return ((Buttons & button) != 0) && ((previousControls.Buttons & button) != 0);
		}

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		internal static extern void Controls_Reset (IntPtr handle);

		public void Reset ()
		{
			Runtime.ValidateObject(this);
			Controls_Reset (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		internal static extern void Controls_Set (IntPtr handle, uint buttons, bool down);

		public void Set (uint buttons, bool down)
		{
			Runtime.ValidateObject(this);
			Controls_Set (handle, buttons, down);
		}

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		internal static extern bool Controls_IsDown (IntPtr handle, uint button);

		public bool IsDown (uint button)
		{
			Runtime.ValidateObject(this);
			return Controls_IsDown (handle, button);
		}
	}
}
