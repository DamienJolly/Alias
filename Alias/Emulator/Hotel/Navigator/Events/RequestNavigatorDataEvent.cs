using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	class RequestNavigatorDataEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new NavigatorMetaDataComposer());
			session.Send(new NavigatorLiftedRoomsComposer());
			session.Send(new NavigatorCollapsedCategoriesComposer());
			session.Send(new NavigatorSettingsComposer(session.Player.Navigator.Settings));
			session.Send(new NavigatorSavedSearchesComposer(session.Player.Navigator.Searches.Values));
		}
	}
}
