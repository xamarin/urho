namespace Urho
{
	unsafe partial class RenderPath
	{
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
	}
}
