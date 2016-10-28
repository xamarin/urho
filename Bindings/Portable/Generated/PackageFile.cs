// WARNING - AUTOGENERATED - DO NOT EDIT
// 
// Generated using `sharpie urho`
// 
// PackageFile.cs
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

namespace Urho.IO
{
	/// <summary>
	/// Stores files of a directory tree sequentially for convenient access.
	/// </summary>
	public unsafe partial class PackageFile : UrhoObject
	{
		public PackageFile (IntPtr handle) : base (handle)
		{
		}

		protected PackageFile (UrhoObjectFlag emptyFlag) : base (emptyFlag)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int PackageFile_GetType (IntPtr handle);

		private StringHash UrhoGetType ()
		{
			Runtime.ValidateRefCounted (this);
			return new StringHash (PackageFile_GetType (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr PackageFile_GetTypeName (IntPtr handle);

		private string GetTypeName ()
		{
			Runtime.ValidateRefCounted (this);
			return Marshal.PtrToStringAnsi (PackageFile_GetTypeName (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int PackageFile_GetTypeStatic ();

		private static StringHash GetTypeStatic ()
		{
			Runtime.Validate (typeof(PackageFile));
			return new StringHash (PackageFile_GetTypeStatic ());
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr PackageFile_GetTypeNameStatic ();

		private static string GetTypeNameStatic ()
		{
			Runtime.Validate (typeof(PackageFile));
			return Marshal.PtrToStringAnsi (PackageFile_GetTypeNameStatic ());
		}

		public PackageFile () : this (Application.CurrentContext)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr PackageFile_PackageFile (IntPtr context);

		public PackageFile (Context context) : base (UrhoObjectFlag.Empty)
		{
			Runtime.Validate (typeof(PackageFile));
			handle = PackageFile_PackageFile ((object)context == null ? IntPtr.Zero : context.Handle);
			Runtime.RegisterObject (this);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr PackageFile_PackageFile0 (IntPtr context, string fileName, uint startOffset);

		public PackageFile (Context context, string fileName, uint startOffset) : base (UrhoObjectFlag.Empty)
		{
			Runtime.Validate (typeof(PackageFile));
			handle = PackageFile_PackageFile0 ((object)context == null ? IntPtr.Zero : context.Handle, fileName, startOffset);
			Runtime.RegisterObject (this);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool PackageFile_Open (IntPtr handle, string fileName, uint startOffset);

		/// <summary>
		/// Open the package file. Return true if successful.
		/// </summary>
		public bool Open (string fileName, uint startOffset)
		{
			Runtime.ValidateRefCounted (this);
			return PackageFile_Open (handle, fileName, startOffset);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool PackageFile_Exists (IntPtr handle, string fileName);

		/// <summary>
		/// Check if a file exists within the package file. This will be case-insensitive on Windows and case-sensitive on other platforms.
		/// </summary>
		public bool Exists (string fileName)
		{
			Runtime.ValidateRefCounted (this);
			return PackageFile_Exists (handle, fileName);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern PackageEntry* PackageFile_GetEntry (IntPtr handle, string fileName);

		/// <summary>
		/// Return the file entry corresponding to the name, or null if not found. This will be case-insensitive on Windows and case-sensitive on other platforms.
		/// </summary>
		public PackageEntry* GetEntry (string fileName)
		{
			Runtime.ValidateRefCounted (this);
			return PackageFile_GetEntry (handle, fileName);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr PackageFile_GetName (IntPtr handle);

		/// <summary>
		/// Return the package file name.
		/// </summary>
		private string GetName ()
		{
			Runtime.ValidateRefCounted (this);
			return Marshal.PtrToStringAnsi (PackageFile_GetName (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int PackageFile_GetNameHash (IntPtr handle);

		/// <summary>
		/// Return hash of the package file name.
		/// </summary>
		private StringHash GetNameHash ()
		{
			Runtime.ValidateRefCounted (this);
			return new StringHash (PackageFile_GetNameHash (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint PackageFile_GetNumFiles (IntPtr handle);

		/// <summary>
		/// Return number of files.
		/// </summary>
		private uint GetNumFiles ()
		{
			Runtime.ValidateRefCounted (this);
			return PackageFile_GetNumFiles (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint PackageFile_GetTotalSize (IntPtr handle);

		/// <summary>
		/// Return total size of the package file.
		/// </summary>
		private uint GetTotalSize ()
		{
			Runtime.ValidateRefCounted (this);
			return PackageFile_GetTotalSize (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint PackageFile_GetTotalDataSize (IntPtr handle);

		/// <summary>
		/// Return total data size from all the file entries in the package file.
		/// </summary>
		private uint GetTotalDataSize ()
		{
			Runtime.ValidateRefCounted (this);
			return PackageFile_GetTotalDataSize (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint PackageFile_GetChecksum (IntPtr handle);

		/// <summary>
		/// Return checksum of the package file contents.
		/// </summary>
		private uint GetChecksum ()
		{
			Runtime.ValidateRefCounted (this);
			return PackageFile_GetChecksum (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool PackageFile_IsCompressed (IntPtr handle);

		/// <summary>
		/// Return whether the files are compressed.
		/// </summary>
		private bool IsCompressed ()
		{
			Runtime.ValidateRefCounted (this);
			return PackageFile_IsCompressed (handle);
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

		public static StringHash TypeStatic {
			get {
				return GetTypeStatic ();
			}
		}

		public static string TypeNameStatic {
			get {
				return GetTypeNameStatic ();
			}
		}

		/// <summary>
		/// Return the package file name.
		/// </summary>
		public string Name {
			get {
				return GetName ();
			}
		}

		/// <summary>
		/// Return hash of the package file name.
		/// </summary>
		public StringHash NameHash {
			get {
				return GetNameHash ();
			}
		}

		/// <summary>
		/// Return number of files.
		/// </summary>
		public uint NumFiles {
			get {
				return GetNumFiles ();
			}
		}

		/// <summary>
		/// Return total size of the package file.
		/// </summary>
		public uint TotalSize {
			get {
				return GetTotalSize ();
			}
		}

		/// <summary>
		/// Return total data size from all the file entries in the package file.
		/// </summary>
		public uint TotalDataSize {
			get {
				return GetTotalDataSize ();
			}
		}

		/// <summary>
		/// Return checksum of the package file contents.
		/// </summary>
		public uint Checksum {
			get {
				return GetChecksum ();
			}
		}

		/// <summary>
		/// Return whether the files are compressed.
		/// </summary>
		public bool Compressed {
			get {
				return IsCompressed ();
			}
		}
	}
}
