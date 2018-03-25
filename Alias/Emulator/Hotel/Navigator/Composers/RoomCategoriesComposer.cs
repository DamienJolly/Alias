using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Navigator.Views;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class RoomCategoriesComposer : IPacketComposer
	{
		private int rank;
		private List<INavigatorCategory> categories;

		public RoomCategoriesComposer(int rank, List<INavigatorCategory> categories)
		{
			this.rank = rank;
			this.categories = categories;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.RoomCategoriesMessageComposer);
			message.Int(this.categories.Count);
			this.categories.ForEach(category =>
			{
				message.Int(category.ExtraId);
				message.String(category.Title);
				message.Boolean(category.MinRank <= this.rank);
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
