using System.Collections.Generic;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	class CatalogSearchResultComposer : IPacketComposer
	{
		private CatalogItem item;

		public CatalogSearchResultComposer(CatalogItem item)
		{
			this.item = item;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.CatalogSearchResultMessageComposer);
			message.WriteInteger(item.Id);
			message.WriteString(item.Name);
			message.WriteBoolean(false);
			message.WriteInteger(item.Credits);
			message.WriteInteger(item.Points);
			message.WriteInteger(item.PointsType);
			message.WriteBoolean(item.CanGift);

			message.WriteInteger(item.GetItems().Count);
			item.GetItems().ForEach(data =>
			{
				message.WriteString(data.Type);
				message.WriteInteger(data.SpriteId);
				message.WriteString(data.ExtraData);
				message.WriteInteger(item.GetItemAmount(data.Id));

				message.WriteBoolean(item.IsLimited);
				if (item.IsLimited)
				{
					message.WriteInteger(item.LimitedStack);
					message.WriteInteger(item.LimitedStack - item.LimitedSells);
				}
			});

			message.WriteInteger(item.ClubLevel);
			message.WriteBoolean(item.HasOffer);
			message.WriteBoolean(false);
			message.WriteString(item.Name + ".png");
			return message;
		}
	}
}
