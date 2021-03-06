using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	class CatalogPageComposer : IPacketComposer
	{
		CatalogPage page;
		Player habbo;
		string mode;

		public CatalogPageComposer(CatalogPage page, Player habbo, string mode)
		{
			this.page = page;
			this.habbo = habbo;
			this.mode = mode;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.CatalogPageMessageComposer);
			message.WriteInteger(this.page.Id);
			message.WriteString(this.mode);
			this.page.GetLayout().Serialize(message, this.page);

			List<CatalogItem> items = this.page.Items;
			if (this.page.Layout == CatalogLayout.RECENT_PURCHASES)
			{
				items = this.habbo.RecentPurchases.Reverse().ToList();
			}

			message.WriteInteger(items.Count);
			items.ForEach(item =>
			{
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
			});

			message.WriteInteger(0);
			message.WriteBoolean(false);

			if (this.page.Layout == CatalogLayout.FRONTPAGE || this.page.Layout == CatalogLayout.FRONTPAGE_FEATURED)
			{
				message.WriteInteger(Alias.Server.CatalogManager.GetFeaturedPages().Count);
				IList<CatalogFeatured> featuredPages = Alias.Server.CatalogManager.GetFeaturedPages();
				foreach (CatalogFeatured feature in featuredPages)
				{
					message.WriteInteger(feature.SlotId);
					message.WriteString(feature.Caption);
					message.WriteString(feature.Image);
					message.WriteInteger(feature.Type);
					switch (feature.Type)
					{
						case 0:
						default:
							message.WriteString(feature.PageName); break;
						case 1:
							message.WriteInteger(feature.PageId); break;
						case 2:
							message.WriteString(feature.ProductName); break;
					}
					message.WriteInteger(-1);
				}
			}
			return message;
		}
	}
}
