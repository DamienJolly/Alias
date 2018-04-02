using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomSettingsSavedComposer : IPacketComposer
	{
		private int roomId;

		public RoomSettingsSavedComposer(int roomId)
		{
			this.roomId = roomId;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomSettingsSavedMessageComposer);
			message.WriteInteger(this.roomId);
			return message;
		}
	}
}
