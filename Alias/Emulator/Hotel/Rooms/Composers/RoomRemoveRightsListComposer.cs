using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomRemoveRightsListComposer : IPacketComposer
	{
		private int RoomId;
		private int UserId;

		public RoomRemoveRightsListComposer(int roomId, int userId)
		{
			this.RoomId = roomId;
			this.UserId = userId;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomRemoveRightsListMessageComposer);
			result.Int(this.RoomId);
			result.Int(this.UserId);
			return result;
		}
	}
}
