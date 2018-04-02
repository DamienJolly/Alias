using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomSettingsUpdatedComposer : IPacketComposer
	{
		private int roomId;

		public RoomSettingsUpdatedComposer(int roomId)
		{
			this.roomId = roomId;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomSettingsUpdatedMessageComposer);
			message.WriteInteger(this.roomId);
			return message;
		}
	}
}
