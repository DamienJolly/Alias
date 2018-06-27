using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	class RemoveSavedSearchEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int id = message.PopInt();

			session.Habbo.NavigatorPreference.RemoveSearch(id);

			session.Send(new NavigatorSavedSearchesComposer(session.Habbo.NavigatorPreference.NavigatorSearches));
		}
	}
}
