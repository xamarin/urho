using System;

namespace Urho {
	public partial class BillboardSet {
		public Billboard? GetBillboardSafe (uint index)
		{
			unsafe {
                Billboard* result = BillboardSet_GetBillboard (handle, index);
				if (result == null)
					return null;
				return *result;
			}
		}
	}
}
