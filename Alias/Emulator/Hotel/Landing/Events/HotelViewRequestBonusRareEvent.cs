using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Landing.Events
{
	public class HotelViewRequestBonusRareEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new BonusRareComposer());
		}
	}
}
