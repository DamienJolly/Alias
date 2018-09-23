using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	class RemoveSavedSearchEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			int id = message.PopInt();
			await session.Player.Navigator.RemoveSearch(id);

			session.Send(new NavigatorSavedSearchesComposer(session.Player.Navigator.Searches.Values));
		}
	}
}
