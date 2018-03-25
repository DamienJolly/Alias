using System.Linq;
using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	public class RequestRoomCategoriesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new RoomCategoriesComposer(session.Habbo.Rank, Alias.GetServer().GetNavigatorManager().GetCategories("hotel_view").Where(cat => cat.ExtraId > 0).ToList()));
		}
	}
}
