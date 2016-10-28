// WARNING - AUTOGENERATED - DO NOT EDIT
// 
// Generated using `sharpie urho`
// 
// TextureCube.cs
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
	/// Cube texture resource.
	/// </summary>
	public unsafe partial class TextureCube : Texture
	{
		public TextureCube (IntPtr handle) : base (handle)
		{
		}

		protected TextureCube (UrhoObjectFlag emptyFlag) : base (emptyFlag)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int TextureCube_GetType (IntPtr handle);

		private StringHash UrhoGetType ()
		{
			Runtime.ValidateRefCounted (this);
			return new StringHash (TextureCube_GetType (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr TextureCube_GetTypeName (IntPtr handle);

		private string GetTypeName ()
		{
			Runtime.ValidateRefCounted (this);
			return Marshal.PtrToStringAnsi (TextureCube_GetTypeName (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int TextureCube_GetTypeStatic ();

		private static StringHash GetTypeStatic ()
		{
			Runtime.Validate (typeof(TextureCube));
			return new StringHash (TextureCube_GetTypeStatic ());
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr TextureCube_GetTypeNameStatic ();

		private static string GetTypeNameStatic ()
		{
			Runtime.Validate (typeof(TextureCube));
			return Marshal.PtrToStringAnsi (TextureCube_GetTypeNameStatic ());
		}

		public TextureCube () : this (Application.CurrentContext)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr TextureCube_TextureCube (IntPtr context);

		public TextureCube (Context context) : base (UrhoObjectFlag.Empty)
		{
			Runtime.Validate (typeof(TextureCube));
			handle = TextureCube_TextureCube ((object)context == null ? IntPtr.Zero : context.Handle);
			Runtime.RegisterObject (this);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void TextureCube_RegisterObject (IntPtr context);

		/// <summary>
		/// Register object factory.
		/// </summary>
		public static void RegisterObject (Context context)
		{
			Runtime.Validate (typeof(TextureCube));
			TextureCube_RegisterObject ((object)context == null ? IntPtr.Zero : context.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool TextureCube_BeginLoad_File (IntPtr handle, IntPtr source);

		/// <summary>
		/// Load resource from stream. May be called from a worker thread. Return true if successful.
		/// </summary>
		public override bool BeginLoad (File source)
		{
			Runtime.ValidateRefCounted (this);
			return TextureCube_BeginLoad_File (handle, (object)source == null ? IntPtr.Zero : source.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool TextureCube_BeginLoad_MemoryBuffer (IntPtr handle, IntPtr source);

		/// <summary>
		/// Load resource from stream. May be called from a worker thread. Return true if successful.
		/// </summary>
		public override bool BeginLoad (MemoryBuffer source)
		{
			Runtime.ValidateRefCounted (this);
			return TextureCube_BeginLoad_MemoryBuffer (handle, (object)source == null ? IntPtr.Zero : source.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool TextureCube_EndLoad (IntPtr handle);

		/// <summary>
		/// Finish resource loading. Always called from the main thread. Return true if successful.
		/// </summary>
		public override bool EndLoad ()
		{
			Runtime.ValidateRefCounted (this);
			return TextureCube_EndLoad (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void TextureCube_Release (IntPtr handle);

		/// <summary>
		/// Release the texture.
		/// </summary>
		public void Release ()
		{
			Runtime.ValidateRefCounted (this);
			TextureCube_Release (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool TextureCube_SetSize (IntPtr handle, int size, uint format, TextureUsage usage, int multiSample);

		/// <summary>
		/// Set size, format, usage and multisampling parameter for rendertargets. Note that cube textures always use autoresolve when multisampled due to lacking support (on all APIs) to multisample them in a shader. Return true if successful.
		/// </summary>
		public bool SetSize (int size, uint format, TextureUsage usage, int multiSample)
		{
			Runtime.ValidateRefCounted (this);
			return TextureCube_SetSize (handle, size, format, usage, multiSample);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool TextureCube_SetData (IntPtr handle, CubeMapFace face, uint level, int x, int y, int width, int height, void* data);

		/// <summary>
		/// Set data either partially or fully on a face's mip level. Return true if successful.
		/// </summary>
		public bool SetData (CubeMapFace face, uint level, int x, int y, int width, int height, void* data)
		{
			Runtime.ValidateRefCounted (this);
			return TextureCube_SetData (handle, face, level, x, y, width, height, data);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool TextureCube_SetData0_File (IntPtr handle, CubeMapFace face, IntPtr source);

		/// <summary>
		/// Set data of one face from a stream. Return true if successful.
		/// </summary>
		public bool SetData (CubeMapFace face, File source)
		{
			Runtime.ValidateRefCounted (this);
			return TextureCube_SetData0_File (handle, face, (object)source == null ? IntPtr.Zero : source.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool TextureCube_SetData0_MemoryBuffer (IntPtr handle, CubeMapFace face, IntPtr source);

		/// <summary>
		/// Set data of one face from a stream. Return true if successful.
		/// </summary>
		public bool SetData (CubeMapFace face, MemoryBuffer source)
		{
			Runtime.ValidateRefCounted (this);
			return TextureCube_SetData0_MemoryBuffer (handle, face, (object)source == null ? IntPtr.Zero : source.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool TextureCube_SetData1 (IntPtr handle, CubeMapFace face, IntPtr image, bool useAlpha);

		/// <summary>
		/// Set data of one face from an image. Return true if successful. Optionally make a single channel image alpha-only.
		/// </summary>
		public bool SetData (CubeMapFace face, Image image, bool useAlpha)
		{
			Runtime.ValidateRefCounted (this);
			return TextureCube_SetData1 (handle, face, (object)image == null ? IntPtr.Zero : image.Handle, useAlpha);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool TextureCube_GetData (IntPtr handle, CubeMapFace face, uint level, IntPtr dest);

		/// <summary>
		/// Get data from a face's mip level. The destination buffer must be big enough. Return true if successful.
		/// </summary>
		public bool GetData (CubeMapFace face, uint level, IntPtr dest)
		{
			Runtime.ValidateRefCounted (this);
			return TextureCube_GetData (handle, face, level, dest);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr TextureCube_GetImage (IntPtr handle, CubeMapFace face);

		/// <summary>
		/// Get image data from a face's zero mip level. Only RGB and RGBA textures are supported.
		/// </summary>
		public Image GetImage (CubeMapFace face)
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupRefCounted<Image> (TextureCube_GetImage (handle, face));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr TextureCube_GetRenderSurface (IntPtr handle, CubeMapFace face);

		/// <summary>
		/// Return render surface for one face.
		/// </summary>
		public RenderSurface GetRenderSurface (CubeMapFace face)
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupRefCounted<RenderSurface> (TextureCube_GetRenderSurface (handle, face));
		}

		public override StringHash Type {
			get {
				return UrhoGetType ();
			}
		}

		public override string TypeName {
			get {
				return GetTypeName ();
			}
		}

		public new static StringHash TypeStatic {
			get {
				return GetTypeStatic ();
			}
		}

		public new static string TypeNameStatic {
			get {
				return GetTypeNameStatic ();
			}
		}
	}
}