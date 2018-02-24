using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Landing.Events
{
	public class RequestNewsListEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new HotelViewDataComposer("2013-05-08 13:0", "gamesmaker"));
			session.Send(new HallOfFameComposer()); //todo:
			session.Send(new NewsListComposer()); //todo:
		}
	}
}
