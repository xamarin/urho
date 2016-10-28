// WARNING - AUTOGENERATED - DO NOT EDIT
// 
// Generated using `sharpie urho`
// 
// Texture2DArray.cs
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
	/// 2D texture array resource.
	/// </summary>
	public unsafe partial class Texture2DArray : Texture
	{
		public Texture2DArray (IntPtr handle) : base (handle)
		{
		}

		protected Texture2DArray (UrhoObjectFlag emptyFlag) : base (emptyFlag)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int Texture2DArray_GetType (IntPtr handle);

		private StringHash UrhoGetType ()
		{
			Runtime.ValidateRefCounted (this);
			return new StringHash (Texture2DArray_GetType (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr Texture2DArray_GetTypeName (IntPtr handle);

		private string GetTypeName ()
		{
			Runtime.ValidateRefCounted (this);
			return Marshal.PtrToStringAnsi (Texture2DArray_GetTypeName (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int Texture2DArray_GetTypeStatic ();

		private static StringHash GetTypeStatic ()
		{
			Runtime.Validate (typeof(Texture2DArray));
			return new StringHash (Texture2DArray_GetTypeStatic ());
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr Texture2DArray_GetTypeNameStatic ();

		private static string GetTypeNameStatic ()
		{
			Runtime.Validate (typeof(Texture2DArray));
			return Marshal.PtrToStringAnsi (Texture2DArray_GetTypeNameStatic ());
		}

		public Texture2DArray () : this (Application.CurrentContext)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr Texture2DArray_Texture2DArray (IntPtr context);

		public Texture2DArray (Context context) : base (UrhoObjectFlag.Empty)
		{
			Runtime.Validate (typeof(Texture2DArray));
			handle = Texture2DArray_Texture2DArray ((object)context == null ? IntPtr.Zero : context.Handle);
			Runtime.RegisterObject (this);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Texture2DArray_RegisterObject (IntPtr context);

		/// <summary>
		/// Register object factory.
		/// </summary>
		public static void RegisterObject (Context context)
		{
			Runtime.Validate (typeof(Texture2DArray));
			Texture2DArray_RegisterObject ((object)context == null ? IntPtr.Zero : context.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Texture2DArray_BeginLoad_File (IntPtr handle, IntPtr source);

		/// <summary>
		/// Load resource from stream. May be called from a worker thread. Return true if successful.
		/// </summary>
		public override bool BeginLoad (File source)
		{
			Runtime.ValidateRefCounted (this);
			return Texture2DArray_BeginLoad_File (handle, (object)source == null ? IntPtr.Zero : source.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Texture2DArray_BeginLoad_MemoryBuffer (IntPtr handle, IntPtr source);

		/// <summary>
		/// Load resource from stream. May be called from a worker thread. Return true if successful.
		/// </summary>
		public override bool BeginLoad (MemoryBuffer source)
		{
			Runtime.ValidateRefCounted (this);
			return Texture2DArray_BeginLoad_MemoryBuffer (handle, (object)source == null ? IntPtr.Zero : source.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Texture2DArray_EndLoad (IntPtr handle);

		/// <summary>
		/// Finish resource loading. Always called from the main thread. Return true if successful.
		/// </summary>
		public override bool EndLoad ()
		{
			Runtime.ValidateRefCounted (this);
			return Texture2DArray_EndLoad (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Texture2DArray_OnDeviceLost (IntPtr handle);

		/// <summary>
		/// Mark the GPU resource destroyed on context destruction.
		/// </summary>
		public void OnDeviceLost ()
		{
			Runtime.ValidateRefCounted (this);
			Texture2DArray_OnDeviceLost (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Texture2DArray_OnDeviceReset (IntPtr handle);

		/// <summary>
		/// Recreate the GPU resource and restore data if applicable.
		/// </summary>
		public void OnDeviceReset ()
		{
			Runtime.ValidateRefCounted (this);
			Texture2DArray_OnDeviceReset (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Texture2DArray_Release (IntPtr handle);

		/// <summary>
		/// Release the texture.
		/// </summary>
		public void Release ()
		{
			Runtime.ValidateRefCounted (this);
			Texture2DArray_Release (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Texture2DArray_SetLayers (IntPtr handle, uint layers);

		/// <summary>
		/// Set the number of layers in the texture. To be used before SetData.
		/// </summary>
		private void SetLayers (uint layers)
		{
			Runtime.ValidateRefCounted (this);
			Texture2DArray_SetLayers (handle, layers);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Texture2DArray_SetSize (IntPtr handle, uint layers, int width, int height, uint format, TextureUsage usage);

		/// <summary>
		/// Set layers, size, format and usage. Set layers to zero to leave them unchanged. Return true if successful.
		/// </summary>
		public bool SetSize (uint layers, int width, int height, uint format, TextureUsage usage)
		{
			Runtime.ValidateRefCounted (this);
			return Texture2DArray_SetSize (handle, layers, width, height, format, usage);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Texture2DArray_SetData (IntPtr handle, uint layer, uint level, int x, int y, int width, int height, void* data);

		/// <summary>
		/// Set data either partially or fully on a layer's mip level. Return true if successful.
		/// </summary>
		public bool SetData (uint layer, uint level, int x, int y, int width, int height, void* data)
		{
			Runtime.ValidateRefCounted (this);
			return Texture2DArray_SetData (handle, layer, level, x, y, width, height, data);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Texture2DArray_SetData0_File (IntPtr handle, uint layer, IntPtr source);

		/// <summary>
		/// Set data of one layer from a stream. Return true if successful.
		/// </summary>
		public bool SetData (uint layer, File source)
		{
			Runtime.ValidateRefCounted (this);
			return Texture2DArray_SetData0_File (handle, layer, (object)source == null ? IntPtr.Zero : source.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Texture2DArray_SetData0_MemoryBuffer (IntPtr handle, uint layer, IntPtr source);

		/// <summary>
		/// Set data of one layer from a stream. Return true if successful.
		/// </summary>
		public bool SetData (uint layer, MemoryBuffer source)
		{
			Runtime.ValidateRefCounted (this);
			return Texture2DArray_SetData0_MemoryBuffer (handle, layer, (object)source == null ? IntPtr.Zero : source.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Texture2DArray_SetData1 (IntPtr handle, uint layer, IntPtr image, bool useAlpha);

		/// <summary>
		/// Set data of one layer from an image. Return true if successful. Optionally make a single channel image alpha-only.
		/// </summary>
		public bool SetData (uint layer, Image image, bool useAlpha)
		{
			Runtime.ValidateRefCounted (this);
			return Texture2DArray_SetData1 (handle, layer, (object)image == null ? IntPtr.Zero : image.Handle, useAlpha);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint Texture2DArray_GetLayers (IntPtr handle);

		/// <summary>
		/// Return number of layers in the texture.
		/// </summary>
		private uint GetLayers ()
		{
			Runtime.ValidateRefCounted (this);
			return Texture2DArray_GetLayers (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Texture2DArray_GetData (IntPtr handle, uint layer, uint level, IntPtr dest);

		/// <summary>
		/// Get data from a mip level. The destination buffer must be big enough. Return true if successful.
		/// </summary>
		public bool GetData (uint layer, uint level, IntPtr dest)
		{
			Runtime.ValidateRefCounted (this);
			return Texture2DArray_GetData (handle, layer, level, dest);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr Texture2DArray_GetRenderSurface (IntPtr handle);

		/// <summary>
		/// Return render surface.
		/// </summary>
		private RenderSurface GetRenderSurface ()
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupRefCounted<RenderSurface> (Texture2DArray_GetRenderSurface (handle));
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

		/// <summary>
		/// Return number of layers in the texture.
		/// Or
		/// Set the number of layers in the texture. To be used before SetData.
		/// </summary>
		public uint Layers {
			get {
				return GetLayers ();
			}
			set {
				SetLayers (value);
			}
		}

		/// <summary>
		/// Return render surface.
		/// </summary>
		public RenderSurface RenderSurface {
			get {
				return GetRenderSurface ();
			}
		}
	}
}