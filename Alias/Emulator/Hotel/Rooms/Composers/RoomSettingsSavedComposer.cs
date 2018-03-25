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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomSettingsSavedMessageComposer);
			result.Int(this.roomId);
			return result;
		}
	}
}
