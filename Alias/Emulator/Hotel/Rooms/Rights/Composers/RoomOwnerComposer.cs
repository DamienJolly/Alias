using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Rights.Composers
{
	public class RoomOwnerComposer : IMessageComposer
	{
		public ServerMessage Compose()
		{
			return new ServerMessage(Outgoing.RoomOwnerMessageComposer);
		}
	}
}
