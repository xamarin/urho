using System;

namespace Urho.Portable
{
	[Preserve(AllMembers = true)]
	public class ScriptInstance : Component
	{
		public ScriptInstance(Context context) : base(context)
		{
			Runtime.Validate(typeof(ScriptInstance));
			handle = Component_Component((object)context == null ? IntPtr.Zero : context.Handle);
			Runtime.RegisterObject(this);
		}

		public ScriptInstance(IntPtr handle) : base(handle) { }

		protected ScriptInstance(UrhoObjectFlag emptyFlag) : base(emptyFlag) { }

		public ScriptInstance() : this (Application.CurrentContext) { }
	}
}
