using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Landing.Events
{
	class RequestNewsListEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new HotelViewDataComposer("2013-05-08 13:0", "gamesmaker"));
			session.Send(new HallOfFameComposer()); //todo:
			session.Send(new NewsListComposer()); //todo:
		}
	}
}
