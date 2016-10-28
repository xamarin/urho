// WARNING - AUTOGENERATED - DO NOT EDIT
// 
// Generated using `sharpie urho`
// 
// AnimatedModel.cs
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
	/// Animated model component.
	/// </summary>
	public unsafe partial class AnimatedModel : StaticModel
	{
		public AnimatedModel (IntPtr handle) : base (handle)
		{
		}

		protected AnimatedModel (UrhoObjectFlag emptyFlag) : base (emptyFlag)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int AnimatedModel_GetType (IntPtr handle);

		private StringHash UrhoGetType ()
		{
			Runtime.ValidateRefCounted (this);
			return new StringHash (AnimatedModel_GetType (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimatedModel_GetTypeName (IntPtr handle);

		private string GetTypeName ()
		{
			Runtime.ValidateRefCounted (this);
			return Marshal.PtrToStringAnsi (AnimatedModel_GetTypeName (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int AnimatedModel_GetTypeStatic ();

		private static StringHash GetTypeStatic ()
		{
			Runtime.Validate (typeof(AnimatedModel));
			return new StringHash (AnimatedModel_GetTypeStatic ());
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimatedModel_GetTypeNameStatic ();

		private static string GetTypeNameStatic ()
		{
			Runtime.Validate (typeof(AnimatedModel));
			return Marshal.PtrToStringAnsi (AnimatedModel_GetTypeNameStatic ());
		}

		public AnimatedModel () : this (Application.CurrentContext)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimatedModel_AnimatedModel (IntPtr context);

		public AnimatedModel (Context context) : base (UrhoObjectFlag.Empty)
		{
			Runtime.Validate (typeof(AnimatedModel));
			handle = AnimatedModel_AnimatedModel ((object)context == null ? IntPtr.Zero : context.Handle);
			Runtime.RegisterObject (this);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_RegisterObject (IntPtr context);

		/// <summary>
		/// Register object factory. Drawable must be registered first.
		/// </summary>
		public new static void RegisterObject (Context context)
		{
			Runtime.Validate (typeof(AnimatedModel));
			AnimatedModel_RegisterObject ((object)context == null ? IntPtr.Zero : context.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool AnimatedModel_Load_File (IntPtr handle, IntPtr source, bool setInstanceDefault);

		/// <summary>
		/// Load from binary data. Return true if successful.
		/// </summary>
		public override bool Load (File source, bool setInstanceDefault)
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_Load_File (handle, (object)source == null ? IntPtr.Zero : source.Handle, setInstanceDefault);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool AnimatedModel_Load_MemoryBuffer (IntPtr handle, IntPtr source, bool setInstanceDefault);

		/// <summary>
		/// Load from binary data. Return true if successful.
		/// </summary>
		public override bool Load (MemoryBuffer source, bool setInstanceDefault)
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_Load_MemoryBuffer (handle, (object)source == null ? IntPtr.Zero : source.Handle, setInstanceDefault);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool AnimatedModel_LoadXML (IntPtr handle, ref Urho.Resources.XmlElement source, bool setInstanceDefault);

		/// <summary>
		/// Load from XML data. Return true if successful.
		/// </summary>
		public override bool LoadXml (Urho.Resources.XmlElement source, bool setInstanceDefault)
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_LoadXML (handle, ref source, setInstanceDefault);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_ApplyAttributes (IntPtr handle);

		/// <summary>
		/// Apply attribute changes that can not be applied immediately. Called after scene load or a network update.
		/// </summary>
		public override void ApplyAttributes ()
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_ApplyAttributes (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern UpdateGeometryType AnimatedModel_GetUpdateGeometryType (IntPtr handle);

		/// <summary>
		/// Return whether a geometry update is necessary, and if it can happen in a worker thread.
		/// </summary>
		private UpdateGeometryType GetUpdateGeometryType ()
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_GetUpdateGeometryType (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_DrawDebugGeometry (IntPtr handle, IntPtr debug, bool depthTest);

		/// <summary>
		/// Visualize the component as debug geometry.
		/// </summary>
		public override void DrawDebugGeometry (DebugRenderer debug, bool depthTest)
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_DrawDebugGeometry (handle, (object)debug == null ? IntPtr.Zero : debug.Handle, depthTest);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_SetModel (IntPtr handle, IntPtr model, bool createBones);

		/// <summary>
		/// Set model.
		/// </summary>
		public void SetModel (Model model, bool createBones)
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_SetModel (handle, (object)model == null ? IntPtr.Zero : model.Handle, createBones);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimatedModel_AddAnimationState (IntPtr handle, IntPtr animation);

		/// <summary>
		/// Add an animation.
		/// </summary>
		public AnimationState AddAnimationState (Animation animation)
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupRefCounted<AnimationState> (AnimatedModel_AddAnimationState (handle, (object)animation == null ? IntPtr.Zero : animation.Handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_RemoveAnimationState (IntPtr handle, IntPtr animation);

		/// <summary>
		/// Remove an animation by animation pointer.
		/// </summary>
		public void RemoveAnimationState (Animation animation)
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_RemoveAnimationState (handle, (object)animation == null ? IntPtr.Zero : animation.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_RemoveAnimationState0 (IntPtr handle, string animationName);

		/// <summary>
		/// Remove an animation by animation name.
		/// </summary>
		public void RemoveAnimationState (string animationName)
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_RemoveAnimationState0 (handle, animationName);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_RemoveAnimationState1 (IntPtr handle, int animationNameHash);

		/// <summary>
		/// Remove an animation by animation name hash.
		/// </summary>
		public void RemoveAnimationState (StringHash animationNameHash)
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_RemoveAnimationState1 (handle, animationNameHash.Code);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_RemoveAnimationState2 (IntPtr handle, IntPtr state);

		/// <summary>
		/// Remove an animation by AnimationState pointer.
		/// </summary>
		public void RemoveAnimationState (AnimationState state)
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_RemoveAnimationState2 (handle, (object)state == null ? IntPtr.Zero : state.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_RemoveAnimationState3 (IntPtr handle, uint index);

		/// <summary>
		/// Remove an animation by index.
		/// </summary>
		public void RemoveAnimationState (uint index)
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_RemoveAnimationState3 (handle, index);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_RemoveAllAnimationStates (IntPtr handle);

		/// <summary>
		/// Remove all animations.
		/// </summary>
		public void RemoveAllAnimationStates ()
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_RemoveAllAnimationStates (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_SetAnimationLodBias (IntPtr handle, float bias);

		/// <summary>
		/// Set animation LOD bias.
		/// </summary>
		private void SetAnimationLodBias (float bias)
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_SetAnimationLodBias (handle, bias);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_SetUpdateInvisible (IntPtr handle, bool enable);

		/// <summary>
		/// Set whether to update animation and the bounding box when not visible. Recommended to enable for physically controlled models like ragdolls.
		/// </summary>
		private void SetUpdateInvisible (bool enable)
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_SetUpdateInvisible (handle, enable);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_SetMorphWeight (IntPtr handle, uint index, float weight);

		/// <summary>
		/// Set vertex morph weight by index.
		/// </summary>
		public void SetMorphWeight (uint index, float weight)
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_SetMorphWeight (handle, index, weight);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_SetMorphWeight4 (IntPtr handle, string name, float weight);

		/// <summary>
		/// Set vertex morph weight by name.
		/// </summary>
		public void SetMorphWeight (string name, float weight)
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_SetMorphWeight4 (handle, name, weight);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_SetMorphWeight5 (IntPtr handle, int nameHash, float weight);

		/// <summary>
		/// Set vertex morph weight by name hash.
		/// </summary>
		public void SetMorphWeight (StringHash nameHash, float weight)
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_SetMorphWeight5 (handle, nameHash.Code, weight);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_ResetMorphWeights (IntPtr handle);

		/// <summary>
		/// Reset all vertex morphs to zero.
		/// </summary>
		public void ResetMorphWeights ()
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_ResetMorphWeights (handle);
		}

		private IReadOnlyList<AnimationState> _GetAnimationStates_cache;

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimatedModel_GetAnimationStates (IntPtr handle);

		/// <summary>
		/// Return all animation states.
		/// </summary>
		private IReadOnlyList<AnimationState> GetAnimationStates ()
		{
			Runtime.ValidateRefCounted (this);
			return _GetAnimationStates_cache != null ? _GetAnimationStates_cache : _GetAnimationStates_cache = Runtime.CreateVectorSharedPtrRefcountedProxy<AnimationState> (AnimatedModel_GetAnimationStates (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint AnimatedModel_GetNumAnimationStates (IntPtr handle);

		/// <summary>
		/// Return number of animation states.
		/// </summary>
		private uint GetNumAnimationStates ()
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_GetNumAnimationStates (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimatedModel_GetAnimationState (IntPtr handle, IntPtr animation);

		/// <summary>
		/// Return animation state by animation pointer.
		/// </summary>
		public AnimationState GetAnimationState (Animation animation)
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupRefCounted<AnimationState> (AnimatedModel_GetAnimationState (handle, (object)animation == null ? IntPtr.Zero : animation.Handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimatedModel_GetAnimationState6 (IntPtr handle, string animationName);

		/// <summary>
		/// Return animation state by animation name.
		/// </summary>
		public AnimationState GetAnimationState (string animationName)
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupRefCounted<AnimationState> (AnimatedModel_GetAnimationState6 (handle, animationName));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimatedModel_GetAnimationState7 (IntPtr handle, int animationNameHash);

		/// <summary>
		/// Return animation state by animation name hash.
		/// </summary>
		public AnimationState GetAnimationState (StringHash animationNameHash)
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupRefCounted<AnimationState> (AnimatedModel_GetAnimationState7 (handle, animationNameHash.Code));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimatedModel_GetAnimationState8 (IntPtr handle, uint index);

		/// <summary>
		/// Return animation state by index.
		/// </summary>
		public AnimationState GetAnimationState (uint index)
		{
			Runtime.ValidateRefCounted (this);
			return Runtime.LookupRefCounted<AnimationState> (AnimatedModel_GetAnimationState8 (handle, index));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern float AnimatedModel_GetAnimationLodBias (IntPtr handle);

		/// <summary>
		/// Return animation LOD bias.
		/// </summary>
		private float GetAnimationLodBias ()
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_GetAnimationLodBias (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool AnimatedModel_GetUpdateInvisible (IntPtr handle);

		/// <summary>
		/// Return whether to update animation when not visible.
		/// </summary>
		private bool GetUpdateInvisible ()
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_GetUpdateInvisible (handle);
		}

		private IReadOnlyList<VertexBuffer> _GetMorphVertexBuffers_cache;

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr AnimatedModel_GetMorphVertexBuffers (IntPtr handle);

		/// <summary>
		/// Return all morph vertex buffers.
		/// </summary>
		private IReadOnlyList<VertexBuffer> GetMorphVertexBuffers ()
		{
			Runtime.ValidateRefCounted (this);
			return _GetMorphVertexBuffers_cache != null ? _GetMorphVertexBuffers_cache : _GetMorphVertexBuffers_cache = Runtime.CreateVectorSharedPtrProxy<VertexBuffer> (AnimatedModel_GetMorphVertexBuffers (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint AnimatedModel_GetNumMorphs (IntPtr handle);

		/// <summary>
		/// Return number of vertex morphs.
		/// </summary>
		private uint GetNumMorphs ()
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_GetNumMorphs (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern float AnimatedModel_GetMorphWeight (IntPtr handle, uint index);

		/// <summary>
		/// Return vertex morph weight by index.
		/// </summary>
		public float GetMorphWeight (uint index)
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_GetMorphWeight (handle, index);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern float AnimatedModel_GetMorphWeight9 (IntPtr handle, string name);

		/// <summary>
		/// Return vertex morph weight by name.
		/// </summary>
		public float GetMorphWeight (string name)
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_GetMorphWeight9 (handle, name);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern float AnimatedModel_GetMorphWeight10 (IntPtr handle, int nameHash);

		/// <summary>
		/// Return vertex morph weight by name hash.
		/// </summary>
		public float GetMorphWeight (StringHash nameHash)
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_GetMorphWeight10 (handle, nameHash.Code);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool AnimatedModel_IsMaster (IntPtr handle);

		/// <summary>
		/// Return whether is the master (first) animated model.
		/// </summary>
		private bool IsMaster ()
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_IsMaster (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern ResourceRef AnimatedModel_GetModelAttr (IntPtr handle);

		/// <summary>
		/// Return model attribute.
		/// </summary>
		private ResourceRef GetModelAttr ()
		{
			Runtime.ValidateRefCounted (this);
			return AnimatedModel_GetModelAttr (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void AnimatedModel_UpdateBoneBoundingBox (IntPtr handle);

		/// <summary>
		/// Recalculate the bone bounding box. Normally called internally, but can also be manually called if up-to-date information before rendering is necessary.
		/// </summary>
		public void UpdateBoneBoundingBox ()
		{
			Runtime.ValidateRefCounted (this);
			AnimatedModel_UpdateBoneBoundingBox (handle);
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
		/// Return whether a geometry update is necessary, and if it can happen in a worker thread.
		/// </summary>
		public override UpdateGeometryType UpdateGeometryType {
			get {
				return GetUpdateGeometryType ();
			}
		}

		/// <summary>
		/// Return animation LOD bias.
		/// Or
		/// Set animation LOD bias.
		/// </summary>
		public float AnimationLodBias {
			get {
				return GetAnimationLodBias ();
			}
			set {
				SetAnimationLodBias (value);
			}
		}

		/// <summary>
		/// Return whether to update animation when not visible.
		/// Or
		/// Set whether to update animation and the bounding box when not visible. Recommended to enable for physically controlled models like ragdolls.
		/// </summary>
		public bool UpdateInvisible {
			get {
				return GetUpdateInvisible ();
			}
			set {
				SetUpdateInvisible (value);
			}
		}

		/// <summary>
		/// Return all animation states.
		/// </summary>
		public IReadOnlyList<AnimationState> AnimationStates {
			get {
				return GetAnimationStates ();
			}
		}

		/// <summary>
		/// Return number of animation states.
		/// </summary>
		public uint NumAnimationStates {
			get {
				return GetNumAnimationStates ();
			}
		}

		/// <summary>
		/// Return all morph vertex buffers.
		/// </summary>
		public IReadOnlyList<VertexBuffer> MorphVertexBuffers {
			get {
				return GetMorphVertexBuffers ();
			}
		}

		/// <summary>
		/// Return number of vertex morphs.
		/// </summary>
		public uint NumMorphs {
			get {
				return GetNumMorphs ();
			}
		}

		/// <summary>
		/// Return whether is the master (first) animated model.
		/// </summary>
		public bool Master {
			get {
				return IsMaster ();
			}
		}

		/// <summary>
		/// Return model attribute.
		/// </summary>
		public override ResourceRef ModelAttr {
			get {
				return GetModelAttr ();
			}
		}
	}
}