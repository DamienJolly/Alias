using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Hotel.Players.Navigator;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	class AddSavedSearchEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			string page = message.PopString();
			string code = message.PopString();

			NavigatorSearch search = new NavigatorSearch(page, code);
			await session.Player.Navigator.AddSearch(search);

			session.Send(new NavigatorSavedSearchesComposer(session.Player.Navigator.Searches.Values));
		}
	}
}
