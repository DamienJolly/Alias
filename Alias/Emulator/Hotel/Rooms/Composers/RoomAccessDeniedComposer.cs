using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomAccessDeniedComposer : IPacketComposer
	{
		string Username;

		public RoomAccessDeniedComposer(string username)
		{
			this.Username = username;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomAccessDeniedMessageComposer);
			message.WriteString(this.Username);
			return message;
		}
	}
}
