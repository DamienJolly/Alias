using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	public class RoomUserRemoveComposer : IMessageComposer
	{
		private int VirtualId;

		public RoomUserRemoveComposer(int virtualId)
		{
			this.VirtualId = virtualId;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomUserRemoveMessageComposer);
			result.String(this.VirtualId.ToString());
			return result;
		}
	}
}
