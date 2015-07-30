namespace Urho {
	public partial class Connection {
		public void SendMessage (int msgId, bool reliable, bool inOrder, byte [] buffer, uint contentId = 0)
		{
			unsafe {
				fixed (byte *p = &buffer[0])
					Connection_SendMessage (handle, msgId, reliable, inOrder, p, (uint) buffer.Length, contentId);
			}
		}
	}
}
