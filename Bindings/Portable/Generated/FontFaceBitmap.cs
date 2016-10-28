// WARNING - AUTOGENERATED - DO NOT EDIT
// 
// Generated using `sharpie urho`
// 
// FontFaceBitmap.cs
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

namespace Urho.Gui
{
	/// <summary>
	/// Bitmap font face description.
	/// </summary>
	public unsafe partial class FontFaceBitmap : FontFace
	{
		public FontFaceBitmap (IntPtr handle) : base (handle)
		{
		}

		protected FontFaceBitmap (UrhoObjectFlag emptyFlag) : base (emptyFlag)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr FontFaceBitmap_FontFaceBitmap (IntPtr font);

		public FontFaceBitmap (Font font) : base (UrhoObjectFlag.Empty)
		{
			Runtime.Validate (typeof(FontFaceBitmap));
			handle = FontFaceBitmap_FontFaceBitmap ((object)font == null ? IntPtr.Zero : font.Handle);
			Runtime.RegisterObject (this);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool FontFaceBitmap_Load (IntPtr handle, byte* fontData, uint fontDataSize, int pointSize);

		/// <summary>
		/// Load font face.
		/// </summary>
		public override bool Load (byte* fontData, uint fontDataSize, int pointSize)
		{
			Runtime.ValidateRefCounted (this);
			return FontFaceBitmap_Load (handle, fontData, fontDataSize, pointSize);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool FontFaceBitmap_Load0 (IntPtr handle, IntPtr fontFace, bool usedGlyphs);

		/// <summary>
		/// Load from existed font face, pack used glyphs into smallest texture size and smallest number of texture.
		/// </summary>
		public bool Load (FontFace fontFace, bool usedGlyphs)
		{
			Runtime.ValidateRefCounted (this);
			return FontFaceBitmap_Load0 (handle, (object)fontFace == null ? IntPtr.Zero : fontFace.Handle, usedGlyphs);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool FontFaceBitmap_Save (IntPtr handle, IntPtr dest, int pointSize, string indentation);

		/// <summary>
		/// Save as a new bitmap font type in XML format. Return true if successful.
		/// </summary>
		public bool Save (File dest, int pointSize, string indentation)
		{
			Runtime.ValidateRefCounted (this);
			return FontFaceBitmap_Save (handle, (object)dest == null ? IntPtr.Zero : dest.Handle, pointSize, indentation);
		}
	}
}
