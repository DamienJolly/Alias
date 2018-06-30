using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class PurchaseOKComposer : IPacketComposer
	{
		CatalogItem item;

		public PurchaseOKComposer(CatalogItem item)
		{
			this.item = item;
		}

		public PurchaseOKComposer()
		{
			this.item = null;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.PurchaseOKMessageComposer);
			if (this.item != null)
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
					message.WriteString("s");
					if (data.Type.Equals("b"))
					{
						message.WriteString(data.Name);
					}
					else
					{
						message.WriteInteger(data.Id);

					//TODO extradata
					message.WriteString("");
						message.WriteInteger(item.GetItemAmount(data.Id));

						message.WriteBoolean(item.IsLimited);
						if (item.IsLimited)
						{
							message.WriteInteger(item.LimitedStack);
							message.WriteInteger(item.LimitedStack - item.LimitedSells);
						}
					}
				});

				message.WriteInteger(item.ClubLevel);
				message.WriteBoolean(item.HasOffer);
				message.WriteBoolean(false);
				message.WriteString(item.Name + ".png");
			}
			else
			{
				message.WriteInteger(0);
				message.WriteString("");
				message.WriteBoolean(false);
				message.WriteInteger(0);
				message.WriteInteger(0);
				message.WriteInteger(0);
				message.WriteBoolean(true);
				message.WriteInteger(1);
				message.WriteString("s");
				message.WriteInteger(0);
				message.WriteString("");
				message.WriteInteger(1);
				message.WriteInteger(0);
				message.WriteString("");
				message.WriteInteger(1);
			}
			return message;
		}
	}
}
