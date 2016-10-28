// WARNING - AUTOGENERATED - DO NOT EDIT
// 
// Generated using `sharpie urho`
// 
// SoundSource.cs
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

namespace Urho.Audio
{
	/// <summary>
	/// %Sound source component with stereo position. A sound source needs to be created to a node to be considered "enabled" and be able to play, however that node does not need to belong to a scene.
	/// </summary>
	public unsafe partial class SoundSource : Component
	{
		public SoundSource (IntPtr handle) : base (handle)
		{
		}

		protected SoundSource (UrhoObjectFlag emptyFlag) : base (emptyFlag)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int SoundSource_GetType (IntPtr handle);

		private StringHash UrhoGetType ()
		{
			Runtime.ValidateRefCounted (this);
			return new StringHash (SoundSource_GetType (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SoundSource_GetTypeName (IntPtr handle);

		private string GetTypeName ()
		{
			Runtime.ValidateRefCounted (this);
			return Marshal.PtrToStringAnsi (SoundSource_GetTypeName (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int SoundSource_GetTypeStatic ();

		private static StringHash GetTypeStatic ()
		{
			Runtime.Validate (typeof(SoundSource));
			return new StringHash (SoundSource_GetTypeStatic ());
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SoundSource_GetTypeNameStatic ();

		private static string GetTypeNameStatic ()
		{
			Runtime.Validate (typeof(SoundSource));
			return Marshal.PtrToStringAnsi (SoundSource_GetTypeNameStatic ());
		}

		public SoundSource () : this (Application.CurrentContext)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SoundSource_SoundSource (IntPtr context);

		public SoundSource (Context context) : base (UrhoObjectFlag.Empty)
		{
			Runtime.Validate (typeof(SoundSource));
			handle = SoundSource_SoundSource ((object)context == null ? IntPtr.Zero : context.Handle);
			Runtime.RegisterObject (this);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_RegisterObject (IntPtr context);

		/// <summary>
		/// Register object factory.
		/// </summary>
		public new static void RegisterObject (Context context)
		{
			Runtime.Validate (typeof(SoundSource));
			SoundSource_RegisterObject ((object)context == null ? IntPtr.Zero : context.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_Play (IntPtr handle, IntPtr sound);

		/// <summary>
		/// Play a sound.
		/// </summary>
		public void Play (Sound sound)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_Play (handle, (object)sound == null ? IntPtr.Zero : sound.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_Play0 (IntPtr handle, IntPtr sound, float frequency);

		/// <summary>
		/// Play a sound with specified frequency.
		/// </summary>
		public void Play (Sound sound, float frequency)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_Play0 (handle, (object)sound == null ? IntPtr.Zero : sound.Handle, frequency);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_Play1 (IntPtr handle, IntPtr sound, float frequency, float gain);

		/// <summary>
		/// Play a sound with specified frequency and gain.
		/// </summary>
		public void Play (Sound sound, float frequency, float gain)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_Play1 (handle, (object)sound == null ? IntPtr.Zero : sound.Handle, frequency, gain);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_Play2 (IntPtr handle, IntPtr sound, float frequency, float gain, float panning);

		/// <summary>
		/// Play a sound with specified frequency, gain and panning.
		/// </summary>
		public void Play (Sound sound, float frequency, float gain, float panning)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_Play2 (handle, (object)sound == null ? IntPtr.Zero : sound.Handle, frequency, gain, panning);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_Play3 (IntPtr handle, IntPtr stream);

		/// <summary>
		/// Start playing a sound stream.
		/// </summary>
		public void Play (SoundStream stream)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_Play3 (handle, (object)stream == null ? IntPtr.Zero : stream.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_Stop (IntPtr handle);

		/// <summary>
		/// Stop playback.
		/// </summary>
		public void Stop ()
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_Stop (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_SetSoundType (IntPtr handle, string type);

		/// <summary>
		/// Set sound type, determines the master gain group.
		/// </summary>
		public void SetSoundType (string type)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_SetSoundType (handle, type);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_SetFrequency (IntPtr handle, float frequency);

		/// <summary>
		/// Set frequency.
		/// </summary>
		private void SetFrequency (float frequency)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_SetFrequency (handle, frequency);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_SetGain (IntPtr handle, float gain);

		/// <summary>
		/// Set gain. 0.0 is silence, 1.0 is full volume.
		/// </summary>
		private void SetGain (float gain)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_SetGain (handle, gain);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_SetAttenuation (IntPtr handle, float attenuation);

		/// <summary>
		/// Set attenuation. 1.0 is unaltered. Used for distance attenuated playback.
		/// </summary>
		private void SetAttenuation (float attenuation)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_SetAttenuation (handle, attenuation);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_SetPanning (IntPtr handle, float panning);

		/// <summary>
		/// Set stereo panning. -1.0 is full left and 1.0 is full right.
		/// </summary>
		private void SetPanning (float panning)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_SetPanning (handle, panning);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_SetAutoRemoveMode (IntPtr handle, AutoRemoveMode mode);

		/// <summary>
		/// / Set to remove either the sound source component or its owner node from the scene automatically on sound playback completion. Disabled by default.
		/// </summary>
		private void SetAutoRemoveMode (AutoRemoveMode mode)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_SetAutoRemoveMode (handle, mode);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_SetPlayPosition (IntPtr handle, sbyte* pos);

		/// <summary>
		/// Set new playback position.
		/// </summary>
		public void SetPlayPosition (sbyte* pos)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_SetPlayPosition (handle, pos);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SoundSource_GetSound (IntPtr handle);

		/// <summary>
		/// Return sound.
		/// </summary>
		private Sound GetSound ()
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupObject<Sound> (SoundSource_GetSound (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern sbyte* SoundSource_GetPlayPosition (IntPtr handle);

		/// <summary>
		/// Return playback position.
		/// </summary>
		private sbyte* GetPlayPosition ()
		{
			Runtime.ValidateRefCounted (this);
			return SoundSource_GetPlayPosition (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SoundSource_GetSoundType (IntPtr handle);

		/// <summary>
		/// Return sound type, determines the master gain group.
		/// </summary>
		private string GetSoundType ()
		{
			Runtime.ValidateRefCounted (this);
			return Marshal.PtrToStringAnsi (SoundSource_GetSoundType (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern float SoundSource_GetTimePosition (IntPtr handle);

		/// <summary>
		/// Return playback time position.
		/// </summary>
		private float GetTimePosition ()
		{
			Runtime.ValidateRefCounted (this);
			return SoundSource_GetTimePosition (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern float SoundSource_GetFrequency (IntPtr handle);

		/// <summary>
		/// Return frequency.
		/// </summary>
		private float GetFrequency ()
		{
			Runtime.ValidateRefCounted (this);
			return SoundSource_GetFrequency (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern float SoundSource_GetGain (IntPtr handle);

		/// <summary>
		/// Return gain.
		/// </summary>
		private float GetGain ()
		{
			Runtime.ValidateRefCounted (this);
			return SoundSource_GetGain (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern float SoundSource_GetAttenuation (IntPtr handle);

		/// <summary>
		/// Return attenuation.
		/// </summary>
		private float GetAttenuation ()
		{
			Runtime.ValidateRefCounted (this);
			return SoundSource_GetAttenuation (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern float SoundSource_GetPanning (IntPtr handle);

		/// <summary>
		/// Return stereo panning.
		/// </summary>
		private float GetPanning ()
		{
			Runtime.ValidateRefCounted (this);
			return SoundSource_GetPanning (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern AutoRemoveMode SoundSource_GetAutoRemoveMode (IntPtr handle);

		/// <summary>
		/// Return automatic removal mode on sound playback completion.
		/// </summary>
		private AutoRemoveMode GetAutoRemoveMode ()
		{
			Runtime.ValidateRefCounted (this);
			return SoundSource_GetAutoRemoveMode (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool SoundSource_IsPlaying (IntPtr handle);

		/// <summary>
		/// Return whether is playing.
		/// </summary>
		private bool IsPlaying ()
		{
			Runtime.ValidateRefCounted (this);
			return SoundSource_IsPlaying (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_Update (IntPtr handle, float timeStep);

		/// <summary>
		/// Update the sound source. Perform subclass specific operations. Called by Audio.
		/// </summary>
		public virtual void Update (float timeStep)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_Update (handle, timeStep);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_Mix (IntPtr handle, int* dest, uint samples, int mixRate, bool stereo, bool interpolation);

		/// <summary>
		/// Mix sound source output to a 32-bit clipping buffer. Called by Audio.
		/// </summary>
		public void Mix (int* dest, uint samples, int mixRate, bool stereo, bool interpolation)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_Mix (handle, dest, samples, mixRate, stereo, interpolation);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_UpdateMasterGain (IntPtr handle);

		/// <summary>
		/// Update the effective master gain. Called internally and by Audio when the master gain changes.
		/// </summary>
		public void UpdateMasterGain ()
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_UpdateMasterGain (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_SetPositionAttr (IntPtr handle, int value);

		/// <summary>
		/// Set sound position attribute.
		/// </summary>
		private void SetPositionAttr (int value)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_SetPositionAttr (handle, value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern ResourceRef SoundSource_GetSoundAttr (IntPtr handle);

		/// <summary>
		/// Return sound attribute.
		/// </summary>
		private ResourceRef GetSoundAttr ()
		{
			Runtime.ValidateRefCounted (this);
			return SoundSource_GetSoundAttr (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SoundSource_SetPlayingAttr (IntPtr handle, bool value);

		/// <summary>
		/// Set sound playing attribute
		/// </summary>
		public void SetPlayingAttr (bool value)
		{
			Runtime.ValidateRefCounted (this);
			SoundSource_SetPlayingAttr (handle, value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int SoundSource_GetPositionAttr (IntPtr handle);

		/// <summary>
		/// Return sound position attribute.
		/// </summary>
		private int GetPositionAttr ()
		{
			Runtime.ValidateRefCounted (this);
			return SoundSource_GetPositionAttr (handle);
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
		/// Return sound type, determines the master gain group.
		/// </summary>
		public string SoundType {
			get {
				return GetSoundType ();
			}
		}

		/// <summary>
		/// Return frequency.
		/// Or
		/// Set frequency.
		/// </summary>
		public float Frequency {
			get {
				return GetFrequency ();
			}
			set {
				SetFrequency (value);
			}
		}

		/// <summary>
		/// Return gain.
		/// Or
		/// Set gain. 0.0 is silence, 1.0 is full volume.
		/// </summary>
		public float Gain {
			get {
				return GetGain ();
			}
			set {
				SetGain (value);
			}
		}

		/// <summary>
		/// Return attenuation.
		/// Or
		/// Set attenuation. 1.0 is unaltered. Used for distance attenuated playback.
		/// </summary>
		public float Attenuation {
			get {
				return GetAttenuation ();
			}
			set {
				SetAttenuation (value);
			}
		}

		/// <summary>
		/// Return stereo panning.
		/// Or
		/// Set stereo panning. -1.0 is full left and 1.0 is full right.
		/// </summary>
		public float Panning {
			get {
				return GetPanning ();
			}
			set {
				SetPanning (value);
			}
		}

		/// <summary>
		/// Return automatic removal mode on sound playback completion.
		/// Or
		/// / Set to remove either the sound source component or its owner node from the scene automatically on sound playback completion. Disabled by default.
		/// </summary>
		public AutoRemoveMode AutoRemoveMode {
			get {
				return GetAutoRemoveMode ();
			}
			set {
				SetAutoRemoveMode (value);
			}
		}

		/// <summary>
		/// Return playback position.
		/// </summary>
		public sbyte* PlayPosition {
			get {
				return GetPlayPosition ();
			}
		}

		/// <summary>
		/// Return sound.
		/// </summary>
		public Sound Sound {
			get {
				return GetSound ();
			}
		}

		/// <summary>
		/// Return playback time position.
		/// </summary>
		public float TimePosition {
			get {
				return GetTimePosition ();
			}
		}

		/// <summary>
		/// Return whether is playing.
		/// </summary>
		public bool Playing {
			get {
				return IsPlaying ();
			}
		}

		/// <summary>
		/// Return sound attribute.
		/// </summary>
		public ResourceRef SoundAttr {
			get {
				return GetSoundAttr ();
			}
		}

		/// <summary>
		/// Return sound position attribute.
		/// Or
		/// Set sound position attribute.
		/// </summary>
		public int PositionAttr {
			get {
				return GetPositionAttr ();
			}
			set {
				SetPositionAttr (value);
			}
		}
	}
}
