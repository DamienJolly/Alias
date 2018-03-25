using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomChatSettingsComposer : IPacketComposer
	{
		RoomData roomData;

		public RoomChatSettingsComposer(RoomData roomData)
		{
			this.roomData = roomData;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomChatSettingsMessageComposer);
			result.Int(this.roomData.Settings.ChatMode);
			result.Int(this.roomData.Settings.ChatSize);
			result.Int(this.roomData.Settings.ChatSpeed);
			result.Int(this.roomData.Settings.ChatDistance);
			result.Int(this.roomData.Settings.ChatFlood);
			return result;
		}
	}
}
