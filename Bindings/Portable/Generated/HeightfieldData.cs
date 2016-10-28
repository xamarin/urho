// WARNING - AUTOGENERATED - DO NOT EDIT
// 
// Generated using `sharpie urho`
// 
// HeightfieldData.cs
// 
// Copyright 2015 Xamarin Inc. All rights reserved.

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
	/// <summary>
	/// Heightfield geometry data.
	/// </summary>
	[StructLayout (LayoutKind.Sequential)]
	public unsafe partial struct HeightfieldData
	{
		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr HeightfieldData_HeightfieldData (IntPtr terrain, uint lodLevel);

		public HeightfieldData (Terrain terrain, uint lodLevel)
		{
			Runtime.Validate (typeof(HeightfieldData));
		}
	}
}
