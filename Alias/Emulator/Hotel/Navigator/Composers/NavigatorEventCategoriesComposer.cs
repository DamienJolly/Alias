using System.Linq;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class NavigatorEventCategoriesComposer : IPacketComposer
	{
		int Rank;

		public NavigatorEventCategoriesComposer(int rank)
		{
			this.Rank = rank;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.NavigatorEventCategoriesMessageComposer);
			message.WriteInteger(Alias.Server.NavigatorManager.GetCategories("roomads_view").Where(cat => cat.ExtraId > 0).Count());
			Alias.Server.NavigatorManager.GetCategories("roomads_view").Where(cat => cat.ExtraId > 0).ToList().ForEach(category =>
			{
				message.WriteInteger(category.ExtraId);
				message.WriteString(category.Title);
				message.WriteBoolean(category.MinRank <= this.Rank);
			});
			return message;
		}
	}
}
