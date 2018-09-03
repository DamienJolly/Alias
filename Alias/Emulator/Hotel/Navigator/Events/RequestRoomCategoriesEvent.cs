using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	class RequestRoomCategoriesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (Alias.Server.NavigatorManager.TryGetCategory("hotel_view", out NavigatorCategory category))
			{
				session.Send(new RoomCategoriesComposer(session.Habbo.Rank, category.Categories));
			}
		}
	}
}
