// WARNING - AUTOGENERATED - DO NOT EDIT
// 
// Generated using `sharpie urho`
// 
// Serializable.cs
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
	/// Base class for objects with automatic serialization through attributes.
	/// </summary>
	public unsafe partial class Serializable : UrhoObject
	{
		public Serializable (IntPtr handle) : base (handle)
		{
		}

		protected Serializable (UrhoObjectFlag emptyFlag) : base (emptyFlag)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int Serializable_GetType (IntPtr handle);

		private StringHash UrhoGetType ()
		{
			Runtime.ValidateRefCounted (this);
			return new StringHash (Serializable_GetType (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr Serializable_GetTypeName (IntPtr handle);

		private string GetTypeName ()
		{
			Runtime.ValidateRefCounted (this);
			return Marshal.PtrToStringAnsi (Serializable_GetTypeName (handle));
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int Serializable_GetTypeStatic ();

		private static StringHash GetTypeStatic ()
		{
			Runtime.Validate (typeof(Serializable));
			return new StringHash (Serializable_GetTypeStatic ());
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr Serializable_GetTypeNameStatic ();

		private static string GetTypeNameStatic ()
		{
			Runtime.Validate (typeof(Serializable));
			return Marshal.PtrToStringAnsi (Serializable_GetTypeNameStatic ());
		}

		public Serializable () : this (Application.CurrentContext)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr Serializable_Serializable (IntPtr context);

		public Serializable (Context context) : base (UrhoObjectFlag.Empty)
		{
			Runtime.Validate (typeof(Serializable));
			handle = Serializable_Serializable ((object)context == null ? IntPtr.Zero : context.Handle);
			Runtime.RegisterObject (this);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_Load (IntPtr handle, IntPtr source, bool setInstanceDefault);

		/// <summary>
		/// Load from binary data. When setInstanceDefault is set to true, after setting the attribute value, store the value as instance's default value. Return true if successful.
		/// </summary>
		public virtual bool Load (File source, bool setInstanceDefault)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_Load (handle, (object)source == null ? IntPtr.Zero : source.Handle, setInstanceDefault);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_Save (IntPtr handle, IntPtr dest);

		/// <summary>
		/// Save as binary data. Return true if successful.
		/// </summary>
		public virtual bool Save (File dest)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_Save (handle, (object)dest == null ? IntPtr.Zero : dest.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_LoadXML (IntPtr handle, ref Urho.Resources.XmlElement source, bool setInstanceDefault);

		/// <summary>
		/// Load from XML data. When setInstanceDefault is set to true, after setting the attribute value, store the value as instance's default value. Return true if successful.
		/// </summary>
		public virtual bool LoadXml (Urho.Resources.XmlElement source, bool setInstanceDefault)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_LoadXML (handle, ref source, setInstanceDefault);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SaveXML (IntPtr handle, ref Urho.Resources.XmlElement dest);

		/// <summary>
		/// Save as XML data. Return true if successful.
		/// </summary>
		public virtual bool SaveXml (Urho.Resources.XmlElement dest)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SaveXML (handle, ref dest);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Serializable_ApplyAttributes (IntPtr handle);

		/// <summary>
		/// Apply attribute changes that can not be applied immediately. Called after scene load or a network update.
		/// </summary>
		public virtual void ApplyAttributes ()
		{
			Runtime.ValidateRefCounted (this);
			Serializable_ApplyAttributes (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SaveDefaultAttributes (IntPtr handle);

		/// <summary>
		/// Return whether should save default-valued attributes into XML. Default false.
		/// </summary>
		public virtual bool SaveDefaultAttributes ()
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SaveDefaultAttributes (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Serializable_MarkNetworkUpdate (IntPtr handle);

		/// <summary>
		/// Mark for attribute check on the next network update.
		/// </summary>
		public virtual void MarkNetworkUpdate ()
		{
			Runtime.ValidateRefCounted (this);
			Serializable_MarkNetworkUpdate (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute0 (IntPtr handle, uint index, ref Vector3 value);

		/// <summary>
		/// Set attribute by index. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (uint index, Vector3 value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute0 (handle, index, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute1 (IntPtr handle, uint index, ref IntRect value);

		/// <summary>
		/// Set attribute by index. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (uint index, IntRect value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute1 (handle, index, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute2 (IntPtr handle, uint index, ref Color value);

		/// <summary>
		/// Set attribute by index. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (uint index, Color value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute2 (handle, index, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute3 (IntPtr handle, uint index, ref Vector2 value);

		/// <summary>
		/// Set attribute by index. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (uint index, Vector2 value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute3 (handle, index, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute4 (IntPtr handle, uint index, ref Vector4 value);

		/// <summary>
		/// Set attribute by index. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (uint index, Vector4 value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute4 (handle, index, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute5 (IntPtr handle, uint index, ref IntVector2 value);

		/// <summary>
		/// Set attribute by index. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (uint index, IntVector2 value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute5 (handle, index, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute6 (IntPtr handle, uint index, ref Quaternion value);

		/// <summary>
		/// Set attribute by index. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (uint index, Quaternion value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute6 (handle, index, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute7 (IntPtr handle, uint index, ref Matrix4 value);

		/// <summary>
		/// Set attribute by index. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (uint index, Matrix4 value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute7 (handle, index, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute8 (IntPtr handle, uint index, ref Matrix3x4 value);

		/// <summary>
		/// Set attribute by index. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (uint index, Matrix3x4 value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute8 (handle, index, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute9 (IntPtr handle, uint index, int value);

		/// <summary>
		/// Set attribute by index. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (uint index, int value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute9 (handle, index, value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute10 (IntPtr handle, uint index, float value);

		/// <summary>
		/// Set attribute by index. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (uint index, float value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute10 (handle, index, value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute11 (IntPtr handle, uint index, string value);

		/// <summary>
		/// Set attribute by index. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (uint index, string value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute11 (handle, index, value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute00 (IntPtr handle, string name, ref Vector3 value);

		/// <summary>
		/// Set attribute by name. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (string name, Vector3 value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute00 (handle, name, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute01 (IntPtr handle, string name, ref IntRect value);

		/// <summary>
		/// Set attribute by name. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (string name, IntRect value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute01 (handle, name, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute02 (IntPtr handle, string name, ref Color value);

		/// <summary>
		/// Set attribute by name. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (string name, Color value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute02 (handle, name, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute03 (IntPtr handle, string name, ref Vector2 value);

		/// <summary>
		/// Set attribute by name. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (string name, Vector2 value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute03 (handle, name, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute04 (IntPtr handle, string name, ref Vector4 value);

		/// <summary>
		/// Set attribute by name. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (string name, Vector4 value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute04 (handle, name, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute05 (IntPtr handle, string name, ref IntVector2 value);

		/// <summary>
		/// Set attribute by name. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (string name, IntVector2 value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute05 (handle, name, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute06 (IntPtr handle, string name, ref Quaternion value);

		/// <summary>
		/// Set attribute by name. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (string name, Quaternion value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute06 (handle, name, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute07 (IntPtr handle, string name, ref Matrix4 value);

		/// <summary>
		/// Set attribute by name. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (string name, Matrix4 value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute07 (handle, name, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute08 (IntPtr handle, string name, ref Matrix3x4 value);

		/// <summary>
		/// Set attribute by name. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (string name, Matrix3x4 value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute08 (handle, name, ref value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute09 (IntPtr handle, string name, int value);

		/// <summary>
		/// Set attribute by name. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (string name, int value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute09 (handle, name, value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute010 (IntPtr handle, string name, float value);

		/// <summary>
		/// Set attribute by name. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (string name, float value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute010 (handle, name, value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_SetAttribute011 (IntPtr handle, string name, string value);

		/// <summary>
		/// Set attribute by name. Return true if successfully set.
		/// </summary>
		public bool SetAttribute (string name, string value)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_SetAttribute011 (handle, name, value);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Serializable_ResetToDefault (IntPtr handle);

		/// <summary>
		/// Reset all editable attributes to their default values.
		/// </summary>
		public void ResetToDefault ()
		{
			Runtime.ValidateRefCounted (this);
			Serializable_ResetToDefault (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Serializable_RemoveInstanceDefault (IntPtr handle);

		/// <summary>
		/// Remove instance's default values if they are set previously.
		/// </summary>
		public void RemoveInstanceDefault ()
		{
			Runtime.ValidateRefCounted (this);
			Serializable_RemoveInstanceDefault (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Serializable_SetTemporary (IntPtr handle, bool enable);

		/// <summary>
		/// Set temporary flag. Temporary objects will not be saved.
		/// </summary>
		private void SetTemporary (bool enable)
		{
			Runtime.ValidateRefCounted (this);
			Serializable_SetTemporary (handle, enable);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Serializable_SetInterceptNetworkUpdate (IntPtr handle, string attributeName, bool enable);

		/// <summary>
		/// Enable interception of an attribute from network updates. Intercepted attributes are sent as events instead of applying directly. This can be used to implement client side prediction.
		/// </summary>
		public void SetInterceptNetworkUpdate (string attributeName, bool enable)
		{
			Runtime.ValidateRefCounted (this);
			Serializable_SetInterceptNetworkUpdate (handle, attributeName, enable);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Serializable_AllocateNetworkState (IntPtr handle);

		/// <summary>
		/// Allocate network attribute state.
		/// </summary>
		public void AllocateNetworkState ()
		{
			Runtime.ValidateRefCounted (this);
			Serializable_AllocateNetworkState (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Serializable_WriteInitialDeltaUpdate (IntPtr handle, IntPtr dest, byte timeStamp);

		/// <summary>
		/// Write initial delta network update.
		/// </summary>
		public void WriteInitialDeltaUpdate (File dest, byte timeStamp)
		{
			Runtime.ValidateRefCounted (this);
			Serializable_WriteInitialDeltaUpdate (handle, (object)dest == null ? IntPtr.Zero : dest.Handle, timeStamp);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Serializable_WriteLatestDataUpdate (IntPtr handle, IntPtr dest, byte timeStamp);

		/// <summary>
		/// Write a latest data network update.
		/// </summary>
		public void WriteLatestDataUpdate (File dest, byte timeStamp)
		{
			Runtime.ValidateRefCounted (this);
			Serializable_WriteLatestDataUpdate (handle, (object)dest == null ? IntPtr.Zero : dest.Handle, timeStamp);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_ReadDeltaUpdate (IntPtr handle, IntPtr source);

		/// <summary>
		/// Read and apply a network delta update. Return true if attributes were changed.
		/// </summary>
		public bool ReadDeltaUpdate (File source)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_ReadDeltaUpdate (handle, (object)source == null ? IntPtr.Zero : source.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_ReadLatestDataUpdate (IntPtr handle, IntPtr source);

		/// <summary>
		/// Read and apply a network latest data update. Return true if attributes were changed.
		/// </summary>
		public bool ReadLatestDataUpdate (File source)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_ReadLatestDataUpdate (handle, (object)source == null ? IntPtr.Zero : source.Handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern Variant Serializable_GetAttribute (IntPtr handle, uint index);

		/// <summary>
		/// Return attribute value by index. Return empty if illegal index.
		/// </summary>
		public Variant GetAttribute (uint index)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_GetAttribute (handle, index);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern Variant Serializable_GetAttribute1 (IntPtr handle, string name);

		/// <summary>
		/// Return attribute value by name. Return empty if not found.
		/// </summary>
		public Variant GetAttribute (string name)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_GetAttribute1 (handle, name);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern Variant Serializable_GetAttributeDefault (IntPtr handle, uint index);

		/// <summary>
		/// Return attribute default value by index. Return empty if illegal index.
		/// </summary>
		public Variant GetAttributeDefault (uint index)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_GetAttributeDefault (handle, index);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern Variant Serializable_GetAttributeDefault2 (IntPtr handle, string name);

		/// <summary>
		/// Return attribute default value by name. Return empty if not found.
		/// </summary>
		public Variant GetAttributeDefault (string name)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_GetAttributeDefault2 (handle, name);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint Serializable_GetNumAttributes (IntPtr handle);

		/// <summary>
		/// Return number of attributes.
		/// </summary>
		private uint GetNumAttributes ()
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_GetNumAttributes (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint Serializable_GetNumNetworkAttributes (IntPtr handle);

		/// <summary>
		/// Return number of network replication attributes.
		/// </summary>
		private uint GetNumNetworkAttributes ()
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_GetNumNetworkAttributes (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_IsTemporary (IntPtr handle);

		/// <summary>
		/// Return whether is temporary.
		/// </summary>
		private bool IsTemporary ()
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_IsTemporary (handle);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Serializable_GetInterceptNetworkUpdate (IntPtr handle, string attributeName);

		/// <summary>
		/// Return whether an attribute's network updates are being intercepted.
		/// </summary>
		public bool GetInterceptNetworkUpdate (string attributeName)
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_GetInterceptNetworkUpdate (handle, attributeName);
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern NetworkState* Serializable_GetNetworkState (IntPtr handle);

		/// <summary>
		/// Return the network attribute state, if allocated.
		/// </summary>
		private NetworkState* GetNetworkState ()
		{
			Runtime.ValidateRefCounted (this);
			return Serializable_GetNetworkState (handle);
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
		/// Return whether is temporary.
		/// Or
		/// Set temporary flag. Temporary objects will not be saved.
		/// </summary>
		public bool Temporary {
			get {
				return IsTemporary ();
			}
			set {
				SetTemporary (value);
			}
		}

		/// <summary>
		/// Return number of attributes.
		/// </summary>
		public uint NumAttributes {
			get {
				return GetNumAttributes ();
			}
		}

		/// <summary>
		/// Return number of network replication attributes.
		/// </summary>
		public uint NumNetworkAttributes {
			get {
				return GetNumNetworkAttributes ();
			}
		}

		/// <summary>
		/// Return the network attribute state, if allocated.
		/// </summary>
		public NetworkState* NetworkState {
			get {
				return GetNetworkState ();
			}
		}
	}
}
