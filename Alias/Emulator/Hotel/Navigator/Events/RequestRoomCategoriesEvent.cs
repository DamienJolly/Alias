using System.Collections.Generic;
using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Hotel.Navigator.Views;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	class RequestRoomCategoriesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!Alias.Server.NavigatorManager.TryGetCategories("hotel_view", out List<INavigatorCategory> categories))
			{
				return;
			}

			session.Send(new RoomCategoriesComposer(session.Player.Rank, categories));
		}
	}
}
