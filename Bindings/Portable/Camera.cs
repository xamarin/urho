using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Urho.Urho2D;
using Urho.Gui;
using Urho.Resources;
using Urho.IO;
using Urho.Navigation;
using Urho.Network;

namespace Urho
{
	partial class Camera
	{
		public Ray GetScreenRayForMouse()
		{
			var app = Application.Current;
			var cursorPos = app.UI.CursorPosition;
			return GetScreenRay((float)cursorPos.X / app.Graphics.Width, (float)cursorPos.Y / app.Graphics.Height);
		}
	}
}
