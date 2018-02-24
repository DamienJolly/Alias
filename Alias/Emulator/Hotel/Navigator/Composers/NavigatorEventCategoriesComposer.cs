using System.Linq;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class NavigatorEventCategoriesComposer : MessageComposer
	{
		int Rank;

		public NavigatorEventCategoriesComposer(int rank)
		{
			this.Rank = rank;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.NavigatorEventCategoriesMessageComposer);
			message.Int(Navigator.GetCategories("roomads_view").Where(cat => cat.ExtraId > 0).Count());
			Navigator.GetCategories("roomads_view").Where(cat => cat.ExtraId > 0).ToList().ForEach(category =>
			{
				message.Int(category.ExtraId);
				message.String(category.Title);
				message.Boolean(category.MinRank <= this.Rank);
			});
			return message;
		}
	}
}
