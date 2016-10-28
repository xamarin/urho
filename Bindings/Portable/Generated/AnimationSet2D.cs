// WARNING - AUTOGENERATED - DO NOT EDIT
// 
// Generated using `sharpie urho`
// 
// AnimationSet2D.cs
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

namespace Urho.Urho2D
{
	/// <summary>
	/// Spriter animation set, it includes one or more animations, for more information please refer to http://www.esotericsoftware.com and http://www.brashmonkey.com/spriter.htm.
	/// </summary>
	public unsafe partial class AnimationSet2D : Resource
	{
		public AnimationSet2D (IntPtr handle) : base (handle)
		{
		}

		protected AnimationSet2D (UrhoObjectFlag emptyFlag) : base (emptyFlag)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int AnimationSet2D_GetType (IntPtr handle);

		private StringHash UrhoGetType ()
		{
			Runtime.ValidateRefCounted (this);
			return new StringHash (AnimationSet2D_GetType (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimationSet2D_GetTypeName (IntPtr handle);

		private string GetTypeName ()
		{
			Runtime.ValidateRefCounted (this);
			return Marshal.PtrToStringAnsi (AnimationSet2D_GetTypeName (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int AnimationSet2D_GetTypeStatic ();

		private static StringHash GetTypeStatic ()
		{
			Runtime.Validate (typeof(AnimationSet2D));
			return new StringHash (AnimationSet2D_GetTypeStatic ());
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimationSet2D_GetTypeNameStatic ();

		private static string GetTypeNameStatic ()
		{
			Runtime.Validate (typeof(AnimationSet2D));
			return Marshal.PtrToStringAnsi (AnimationSet2D_GetTypeNameStatic ());
		}

		public AnimationSet2D () : this (Application.CurrentContext)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimationSet2D_AnimationSet2D (IntPtr context);

		public AnimationSet2D (Context context) : base (UrhoObjectFlag.Empty)
		{
			Runtime.Validate (typeof(AnimationSet2D));
			handle = AnimationSet2D_AnimationSet2D ((object)context == null ? IntPtr.Zero : context.Handle);
			Runtime.RegisterObject (this);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimationSet2D_RegisterObject (IntPtr context);

		/// <summary>
		/// Register object factory.
		/// </summary>
		public static void RegisterObject (Context context)
		{
			Runtime.Validate (typeof(AnimationSet2D));
			AnimationSet2D_RegisterObject ((object)context == null ? IntPtr.Zero : context.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool AnimationSet2D_BeginLoad (IntPtr handle, IntPtr source);

		/// <summary>
		/// Load resource from stream. May be called from a worker thread. Return true if successful.
		/// </summary>
		public override bool BeginLoad (File source)
		{
			Runtime.ValidateRefCounted (this);
			return AnimationSet2D_BeginLoad (handle, (object)source == null ? IntPtr.Zero : source.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool AnimationSet2D_EndLoad (IntPtr handle);

		/// <summary>
		/// Finish resource loading. Always called from the main thread. Return true if successful.
		/// </summary>
		public override bool EndLoad ()
		{
			Runtime.ValidateRefCounted (this);
			return AnimationSet2D_EndLoad (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint AnimationSet2D_GetNumAnimations (IntPtr handle);

		/// <summary>
		/// Get number of animations.
		/// </summary>
		private uint GetNumAnimations ()
		{
			Runtime.ValidateRefCounted (this);
			return AnimationSet2D_GetNumAnimations (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimationSet2D_GetAnimation (IntPtr handle, uint index);

		/// <summary>
		/// Return animation name.
		/// </summary>
		public string GetAnimation (uint index)
		{
			Runtime.ValidateRefCounted (this);
			return Marshal.PtrToStringAnsi (AnimationSet2D_GetAnimation (handle, index));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool AnimationSet2D_HasAnimation (IntPtr handle, string animation);

		/// <summary>
		/// Check has animation.
		/// </summary>
		public bool HasAnimation (string animation)
		{
			Runtime.ValidateRefCounted (this);
			return AnimationSet2D_HasAnimation (handle, animation);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimationSet2D_GetSprite (IntPtr handle);

		/// <summary>
		/// Return sprite.
		/// </summary>
		private Sprite2D GetSprite ()
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupObject<Sprite2D> (AnimationSet2D_GetSprite (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimationSet2D_GetSpriterFileSprite (IntPtr handle, int folderId, int fileId);

		/// <summary>
		/// Return spriter file sprite.
		/// </summary>
		public Sprite2D GetSpriterFileSprite (int folderId, int fileId)
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupObject<Sprite2D> (AnimationSet2D_GetSpriterFileSprite (handle, folderId, fileId));
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
		/// Get number of animations.
		/// </summary>
		public uint NumAnimations {
			get {
				return GetNumAnimations ();
			}
		}

		/// <summary>
		/// Return sprite.
		/// </summary>
		public Sprite2D Sprite {
			get {
				return GetSprite ();
			}
		}
	}
}
