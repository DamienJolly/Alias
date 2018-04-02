using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class NavigatorSavedSearchesComposer : IPacketComposer
	{
		private List<NavigatorSearches> searches;

		public NavigatorSavedSearchesComposer(List<NavigatorSearches> searches)
		{
			this.searches = searches;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.NavigatorSavedSearchesMessageComposer);
			message.WriteInteger(searches.Count);

			int count = 0;
			searches.ForEach(search =>
			{
				count++;
				message.WriteInteger(count);
				message.WriteString(search.Page);
				message.WriteString(search.SearchCode);
				message.WriteString("");
			});

			return message;
		}
	}
}
