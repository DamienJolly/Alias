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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomAccessDeniedMessageComposer);
			result.String(this.Username);
			return result;
		}
	}
}
