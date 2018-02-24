using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Landing.Events
{
	public class HotelViewEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			//todo: leave room
		}
	}
}
