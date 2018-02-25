using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomOpenComposer : MessageComposer
	{
		public ServerMessage Compose()
		{
			return new ServerMessage(Outgoing.RoomOpenMessageComposer);
		}
	}
}
