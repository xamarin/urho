using System;

namespace Urho {
	public partial class BillboardSet
	{
		public BillboardWrapper GetBillboardSafe (uint index)
		{
			Runtime.ValidateRefCounted(this);
			unsafe {
				Billboard* result = BillboardSet_GetBillboard (handle, index);
				if (result == null)
					return null;
				return new BillboardWrapper(this, result);
			}
		}
	}
}
