// WARNING - AUTOGENERATED - DO NOT EDIT
// 
// Generated using `sharpie urho`
// 
// ValueAnimationInfo.cs
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
	/// Base class for a value animation instance, which includes animation runtime information and updates the target object's value automatically.
	/// </summary>
	public unsafe partial class ValueAnimationInfo : RefCounted
	{
		public ValueAnimationInfo (IntPtr handle) : base (handle)
		{
		}

		protected ValueAnimationInfo (UrhoObjectFlag emptyFlag) : base (emptyFlag)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr ValueAnimationInfo_ValueAnimationInfo (IntPtr animation, WrapMode wrapMode, float speed);

		public ValueAnimationInfo (ValueAnimation animation, WrapMode wrapMode, float speed) : base (UrhoObjectFlag.Empty)
		{
			Runtime.Validate (typeof(ValueAnimationInfo));
			handle = ValueAnimationInfo_ValueAnimationInfo ((object)animation == null ? IntPtr.Zero : animation.Handle, wrapMode, speed);
			Runtime.RegisterObject (this);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr ValueAnimationInfo_ValueAnimationInfo0 (IntPtr target, IntPtr animation, WrapMode wrapMode, float speed);

		public ValueAnimationInfo (Urho.UrhoObject target, ValueAnimation animation, WrapMode wrapMode, float speed) : base (UrhoObjectFlag.Empty)
		{
			Runtime.Validate (typeof(ValueAnimationInfo));
			handle = ValueAnimationInfo_ValueAnimationInfo0 ((object)target == null ? IntPtr.Zero : target.Handle, (object)animation == null ? IntPtr.Zero : animation.Handle, wrapMode, speed);
			Runtime.RegisterObject (this);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool ValueAnimationInfo_Update (IntPtr handle, float timeStep);

		/// <summary>
		/// Advance time position and apply. Return true when the animation is finished. No-op when the target object is not defined.
		/// </summary>
		public bool Update (float timeStep)
		{
			Runtime.ValidateRefCounted (this);
			return ValueAnimationInfo_Update (handle, timeStep);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool ValueAnimationInfo_SetTime (IntPtr handle, float time);

		/// <summary>
		/// Set time position and apply. Return true when the animation is finished. No-op when the target object is not defined.
		/// </summary>
		public bool SetTime (float time)
		{
			Runtime.ValidateRefCounted (this);
			return ValueAnimationInfo_SetTime (handle, time);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void ValueAnimationInfo_SetWrapMode (IntPtr handle, WrapMode wrapMode);

		/// <summary>
		/// Set wrap mode.
		/// </summary>
		private void SetWrapMode (WrapMode wrapMode)
		{
			Runtime.ValidateRefCounted (this);
			ValueAnimationInfo_SetWrapMode (handle, wrapMode);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void ValueAnimationInfo_SetSpeed (IntPtr handle, float speed);

		/// <summary>
		/// Set speed.
		/// </summary>
		private void SetSpeed (float speed)
		{
			Runtime.ValidateRefCounted (this);
			ValueAnimationInfo_SetSpeed (handle, speed);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr ValueAnimationInfo_GetTarget (IntPtr handle);

		/// <summary>
		/// Return target object.
		/// </summary>
		private Urho.UrhoObject GetTarget ()
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupRefCounted<Urho.UrhoObject> (ValueAnimationInfo_GetTarget (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr ValueAnimationInfo_GetAnimation (IntPtr handle);

		/// <summary>
		/// Return animation.
		/// </summary>
		private ValueAnimation GetAnimation ()
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupObject<ValueAnimation> (ValueAnimationInfo_GetAnimation (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern WrapMode ValueAnimationInfo_GetWrapMode (IntPtr handle);

		/// <summary>
		/// Return wrap mode.
		/// </summary>
		private WrapMode GetWrapMode ()
		{
			Runtime.ValidateRefCounted (this);
			return ValueAnimationInfo_GetWrapMode (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern float ValueAnimationInfo_GetTime (IntPtr handle);

		/// <summary>
		/// Return time position.
		/// </summary>
		private float GetTime ()
		{
			Runtime.ValidateRefCounted (this);
			return ValueAnimationInfo_GetTime (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern float ValueAnimationInfo_GetSpeed (IntPtr handle);

		/// <summary>
		/// Return speed.
		/// </summary>
		private float GetSpeed ()
		{
			Runtime.ValidateRefCounted (this);
			return ValueAnimationInfo_GetSpeed (handle);
		}

		/// <summary>
		/// Return wrap mode.
		/// Or
		/// Set wrap mode.
		/// </summary>
		public WrapMode WrapMode {
			get {
				return GetWrapMode ();
			}
			set {
				SetWrapMode (value);
			}
		}

		/// <summary>
		/// Return speed.
		/// Or
		/// Set speed.
		/// </summary>
		public float Speed {
			get {
				return GetSpeed ();
			}
			set {
				SetSpeed (value);
			}
		}

		/// <summary>
		/// Return target object.
		/// </summary>
		public Urho.UrhoObject Target {
			get {
				return GetTarget ();
			}
		}

		/// <summary>
		/// Return animation.
		/// </summary>
		public ValueAnimation Animation {
			get {
				return GetAnimation ();
			}
		}

		/// <summary>
		/// Return time position.
		/// </summary>
		public float Time {
			get {
				return GetTime ();
			}
		}
	}
}
