using System.Collections.Generic;
using Alias.Emulator.Hotel.Navigator.Views;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	class RoomCategoriesComposer : IPacketComposer
	{
		private int rank;
		private List<INavigatorCategory> categories;

		public RoomCategoriesComposer(int rank, List<INavigatorCategory> categories)
		{
			this.rank = rank;
			this.categories = categories;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomCategoriesMessageComposer);
			message.WriteInteger(this.categories.Count);
			this.categories.ForEach(category =>
			{
				message.WriteInteger(category.Id);
				message.WriteString(category.Title);
				message.WriteBoolean(category.MinRank <= this.rank);
				message.WriteBoolean(false);
				message.WriteString(category.Title);

				if (category.Title.StartsWith("${"))
				{
					message.WriteString("");
				}
				else
				{
					message.WriteString(category.Title);
				}

				message.WriteBoolean(false);
			});
			return message;
		}
	}
}
