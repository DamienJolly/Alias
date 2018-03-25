using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class ForwardToRoomComposer : IPacketComposer
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
