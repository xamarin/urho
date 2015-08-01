using System;
using System.Runtime.InteropServices;

namespace Urho {
	public partial class Controls {
		Connection connection;
		IntPtr handle;

		internal Controls (Connection connection, IntPtr handle)
		{
			this.connection = connection;
			this.handle = handle;
		}
		
		[DllImport ("mono-urho")]
		extern static uint Controls_GetButtons (IntPtr handle);

		[DllImport ("mono-urho")]
		extern static void Controls_SetButtons (IntPtr handle, uint value);

		[DllImport ("mono-urho")]
		extern static float Controls_GetYaw (IntPtr handle);

		[DllImport ("mono-urho")]
		extern static void Controls_SetYaw (IntPtr handle, float yaw);

		[DllImport ("mono-urho")]
		extern static float Controls_GetPitch (IntPtr handle);

		[DllImport ("mono-urho")]
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
			return ((Buttons & button) != 0) && ((previousControls.Buttons & button) != 0);
		}

		[DllImport ("mono-urho")]
		internal static extern void Controls_Reset (IntPtr handle);

		public void Reset ()
		{
			Controls_Reset (handle);
		}

		[DllImport ("mono-urho")]
		internal static extern void Controls_Set (IntPtr handle, uint buttons, bool down);

		public void Set (uint buttons, bool down)
		{
			Controls_Set (handle, buttons, down);
		}

		[DllImport ("mono-urho")]
		internal static extern bool Controls_IsDown (IntPtr handle, uint button);

		public bool IsDown (uint button)
		{
			return Controls_IsDown (handle, button);
		}
	}
}
