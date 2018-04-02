using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class UserHomeRoomComposer : IPacketComposer
	{
		private int HomeRoomId = 0;

		public UserHomeRoomComposer(int homeRoom)
		{
			this.HomeRoomId = homeRoom;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserHomeRoomMessageComposer);
			message.WriteInteger(this.HomeRoomId);
			message.WriteInteger(this.HomeRoomId);
			return message;
		}
	}
}
