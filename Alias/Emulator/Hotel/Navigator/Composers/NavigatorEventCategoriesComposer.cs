using System.Collections.Generic;
using Alias.Emulator.Hotel.Navigator.Views;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	class NavigatorEventCategoriesComposer : IPacketComposer
	{
		private int rank;
		private List<INavigatorCategory> categories;

		public NavigatorEventCategoriesComposer(int rank, List<INavigatorCategory> categories)
		{
			this.rank = rank;
			this.categories = categories;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.NavigatorEventCategoriesMessageComposer);
			message.WriteInteger(this.categories.Count);
			this.categories.ForEach(category =>
			{
				message.WriteInteger(category.Id);
				message.WriteString(category.Title);
				message.WriteBoolean(category.MinRank <= this.rank);
			});
			return message;
		}
	}
}
