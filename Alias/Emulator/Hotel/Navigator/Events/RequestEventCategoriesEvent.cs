using System.Collections.Generic;
using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Hotel.Navigator.Views;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	class RequestEventCategoriesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!Alias.Server.NavigatorManager.TryGetCategories("roomads_view", out List<INavigatorCategory> categories))
			{
				return;
			}

			session.Send(new NavigatorEventCategoriesComposer(session.Habbo.Rank, categories));
		}
	}
}
