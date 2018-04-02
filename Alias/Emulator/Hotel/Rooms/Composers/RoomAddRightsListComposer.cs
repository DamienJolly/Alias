using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomAddRightsListComposer : IPacketComposer
	{
		private int RoomId;
		private int UserId;
		private string Username;

		public RoomAddRightsListComposer(int roomId, int userId, string username)
		{
			this.RoomId = roomId;
			this.UserId = userId;
			this.Username = username;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomAddRightsListMessageComposer);
			message.WriteInteger(this.RoomId);
			message.WriteInteger(this.UserId);
			message.WriteString(this.Username);
			return message;
		}
	}
}
