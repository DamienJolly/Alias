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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomSettingsUpdatedMessageComposer);
			result.Int(this.roomId);
			return result;
		}
	}
}
