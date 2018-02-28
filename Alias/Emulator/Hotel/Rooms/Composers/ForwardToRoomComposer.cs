using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class ForwardToRoomComposer : IMessageComposer
	{
		private int roomId;

		public ForwardToRoomComposer(int roomId)
		{
			this.roomId = roomId;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.ForwardToRoomMessageComposer);
			result.Int(this.roomId);
			return result;
		}
	}
}
