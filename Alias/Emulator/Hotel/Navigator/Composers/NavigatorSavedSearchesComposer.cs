using System.Collections.Generic;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class NavigatorSavedSearchesComposer : MessageComposer
	{
		private List<NavigatorSearches> searches;

		public NavigatorSavedSearchesComposer(List<NavigatorSearches> searches)
		{
			this.searches = searches;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.NavigatorSavedSearchesMessageComposer);
			result.Int(searches.Count);

			int count = 0;
			searches.ForEach(search =>
			{
				count++;
				result.Int(count);
				result.String(search.Page);
				result.String(search.SearchCode);
				result.String("");
			});

			return result;
		}
	}
}