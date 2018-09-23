using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	public class UserHomeRoomComposer : IPacketComposer
	{
		private int _homeRoomId = 0;

		public UserHomeRoomComposer(int homeRoom)
		{
			_homeRoomId = homeRoom;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserHomeRoomMessageComposer);
			message.WriteInteger(_homeRoomId);
			message.WriteInteger(_homeRoomId);
			return message;
		}
	}
}
