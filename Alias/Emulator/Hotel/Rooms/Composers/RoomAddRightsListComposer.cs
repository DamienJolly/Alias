using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomAddRightsListComposer : MessageComposer
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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomAddRightsListMessageComposer);
			result.Int(this.RoomId);
			result.Int(this.UserId);
			result.String(this.Username);
			return result;
		}
	}
}
