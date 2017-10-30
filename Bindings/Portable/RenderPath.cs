namespace Urho
{
	unsafe partial class RenderPath
	{
#if !WINDOWS_UWP && !UWP_HOLO
		public RenderPathCommand*[] Commands
		{
			get
			{
				var count = NumCommands;
				var result = new RenderPathCommand*[count];
				for (uint i = 0; i < count; i++)
				{
					result[i] = GetCommand(i);
				}
				return result;
			}
		}
#endif
	}
}
