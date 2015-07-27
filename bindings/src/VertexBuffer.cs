using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Urho
{	
	[Flags]
	public enum ElementMask : uint {
		 MASK_NONE = 0x0,
		 MASK_POSITION = 0x1,
		 MASK_NORMAL = 0x2,
		 MASK_COLOR = 0x4,
		 MASK_TEXCOORD1 = 0x8,
		 MASK_TEXCOORD2 = 0x10,
		 MASK_CUBETEXCOORD1 = 0x20,
		 MASK_CUBETEXCOORD2 = 0x40,
		 MASK_TANGENT = 0x80,
		 MASK_BLENDWEIGHTS = 0x100,
		 MASK_BLENDINDICES = 0x200,
		 MASK_INSTANCEMATRIX1 = 0x400,
		 MASK_INSTANCEMATRIX2 = 0x800,
		 MASK_INSTANCEMATRIX3 = 0x1000,
		 MASK_DEFAULT = 0xffffffff,
		 NO_ELEMENT = 0xffffffff
	}
	
	public partial class VertexBuffer
	{
		public bool SetSize (uint vertexCount, ElementMask elementMask, bool dynamic)
		{
			return VertexBuffer_SetSize (handle, vertexCount, (uint)elementMask, dynamic);
		}

		public static uint GetVertexSize (ElementMask elementMask)
		{
			return VertexBuffer_GetVertexSize0 ((uint)elementMask);
		}

		public static uint GetElementOffset (ElementMask elementMask, VertexElement element)
		{
			return VertexBuffer_GetElementOffset1 ((uint)elementMask, element);
		}

		public uint ElementMask {
			get {
				return GetElementMask ();
			}
		}
		
		public ElementMask ElementMaskEnum {
			get {
				return (ElementMask)GetElementMask ();
			}
		}
	}
}
