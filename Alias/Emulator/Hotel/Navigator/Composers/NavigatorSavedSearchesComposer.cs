using System.Collections.Generic;
using Alias.Emulator.Hotel.Players.Navigator;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	class NavigatorSavedSearchesComposer : IPacketComposer
	{
		private ICollection<NavigatorSearch> _searches;

		public NavigatorSavedSearchesComposer(ICollection<NavigatorSearch> searches)
		{
			_searches = searches;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.NavigatorSavedSearchesMessageComposer);
			message.WriteInteger(_searches.Count);
			foreach (NavigatorSearch search in _searches)
			{
				message.WriteInteger(search.Id);
				message.WriteString(search.Page);
				message.WriteString(search.Code);
				message.WriteString("");
			}
			return message;
		}
	}
}
