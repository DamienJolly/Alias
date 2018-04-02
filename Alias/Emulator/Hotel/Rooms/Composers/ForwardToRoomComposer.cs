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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ForwardToRoomMessageComposer);
			message.WriteInteger(this.roomId);
			return message;
		}
	}
}
