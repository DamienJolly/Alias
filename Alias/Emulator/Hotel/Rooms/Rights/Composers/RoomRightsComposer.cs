using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Rights.Composers
{
	public class RoomRightsComposer : MessageComposer
	{
		private int Id;

		public RoomRightsComposer(int id)
		{
			this.Id = id;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomRightsMessageComposer);
			result.Int(this.Id);
			return result;
		}
	}
}
