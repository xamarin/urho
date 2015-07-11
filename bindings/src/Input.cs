namespace Urho {
	public partial class Input {
		public unsafe bool TryGetTouch (uint idx, out TouchState state)
		{
			if (idx > 0){
				var x = GetTouch (idx);
				if (x != null){
					state = *((TouchState *) x);
					return true;
				}
			}
			state = new TouchState ();
			return false;
		}
	}
}
