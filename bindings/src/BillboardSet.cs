using System;

namespace Urho {
	public partial class BillboardSet
    {
#warning It doesn't work. see https://github.com/xamarin/urho/issues/48 
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
