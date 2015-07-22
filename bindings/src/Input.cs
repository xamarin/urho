using System;

namespace Urho {
	[Flags]
	public enum MouseButton {
		Left = 1 << 0,
		Middle = 1 << 1,
		Right = 1 << 2,
		X1 = 1 << 3,
		X2 = 1 << 4,
	}

	public partial class Input {
		public unsafe bool TryGetTouch (uint idx, out TouchState state)
		{
			if (idx > 0){
				var x = GetTouch (idx);
				if (x != null){
					state = *((TouchState *) x);
					return true;
				}
			}
			state = new TouchState ();
			return false;
		}

		public bool GetMouseButtonDown (MouseButton mb)
		{
			return Input_GetMouseButtonDown (handle, (int) mb);
		}

		public bool GetMouseButtonPress (MouseButton mb)
		{
			return Input_GetMouseButtonPress (handle, (int) mb);
		}
	}
}
