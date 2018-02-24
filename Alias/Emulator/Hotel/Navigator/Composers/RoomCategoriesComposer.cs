using System.Linq;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class RoomCategoriesComposer : MessageComposer
	{
		int Rank;

		public RoomCategoriesComposer(int rank)
		{
			this.Rank = rank;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.RoomCategoriesMessageComposer);
			message.Int(Navigator.GetCategories("hotel_view").Where(cat => cat.ExtraId > 0).Count());
			Navigator.GetCategories("hotel_view").Where(cat => cat.ExtraId > 0).ToList().ForEach(category =>
			{
				message.Int(category.ExtraId);
				message.String(category.Title);
				message.Boolean(category.MinRank <= this.Rank);
				message.Boolean(false);
				message.String(category.Title);

				if (category.Title.StartsWith("${"))
				{
					message.String("");
				}
				else
				{
					message.String(category.Title);
				}

				message.Boolean(false);
			});
			return message;
		}
	}
}
