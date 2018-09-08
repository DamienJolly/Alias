using System.Collections.Generic;
using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Hotel.Navigator.Views;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	class SearchRoomsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			string categoryName = message.PopString();
			if (!Alias.Server.NavigatorManager.TryGetCategories(categoryName, out List<INavigatorCategory> categories))
			{
				return;
			}

			string searchParam = message.PopString();
			session.Send(new NavigatorSearchResultsComposer(categoryName, categories, searchParam, session));
		}
	}
}
