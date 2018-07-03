using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	class RoomChatSettingsComposer : IPacketComposer
	{
		RoomData roomData;

		public RoomChatSettingsComposer(RoomData roomData)
		{
			this.roomData = roomData;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomChatSettingsMessageComposer);
			message.WriteInteger(this.roomData.Settings.ChatMode);
			message.WriteInteger(this.roomData.Settings.ChatSize);
			message.WriteInteger(this.roomData.Settings.ChatSpeed);
			message.WriteInteger(this.roomData.Settings.ChatDistance);
			message.WriteInteger(this.roomData.Settings.ChatFlood);
			return message;
		}
	}
}
