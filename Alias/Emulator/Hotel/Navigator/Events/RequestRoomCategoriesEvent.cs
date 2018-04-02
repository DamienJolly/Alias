using System.Linq;
using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	public class RequestRoomCategoriesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new RoomCategoriesComposer(session.Habbo.Rank, Alias.Server.NavigatorManager.GetCategories("hotel_view").Where(cat => cat.ExtraId > 0).ToList()));
		}
	}
}
