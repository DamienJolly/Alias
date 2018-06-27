using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	class AddSavedSearchEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			string page = message.PopString();
			string searchCode = message.PopString();

			if (session.Habbo.NavigatorPreference.HasPage(page, searchCode))
				return;

			session.Habbo.NavigatorPreference.AddSearch(page, searchCode);

			session.Send(new NavigatorSavedSearchesComposer(session.Habbo.NavigatorPreference.NavigatorSearches));
		}
	}
}
