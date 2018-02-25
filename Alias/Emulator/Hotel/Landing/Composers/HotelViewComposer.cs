using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	public class HotelViewComposer : MessageComposer
	{
		public ServerMessage Compose()
		{
			return new ServerMessage(Outgoing.HotelViewMessageComposer);
		}
	}
}
