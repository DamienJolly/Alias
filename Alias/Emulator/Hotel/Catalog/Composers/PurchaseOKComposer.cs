using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class PurchaseOKComposer : IMessageComposer
	{
		CatalogItem item;

		public PurchaseOKComposer(CatalogItem item)
		{
			this.item = item;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.PurchaseOKMessageComposer);
			message.Int(item.Id);
			message.String(item.Name);
			message.Boolean(false);
			message.Int(item.Credits);
			message.Int(item.Points);
			message.Int(item.PointsType);
			message.Boolean(item.CanGift);

			message.Int(item.GetItems().Count);
			item.GetItems().ForEach(data =>
			{
				message.String("s");
				if (data.Type.Equals("b"))
				{
					message.String(data.Name);
				}
				else
				{
					message.Int(data.Id);

					//TODO extradata
					message.String("");
					message.Int(item.GetItemAmount(data.Id));

					message.Boolean(item.IsLimited);
					if (item.IsLimited)
					{
						message.Int(item.LimitedStack);
						message.Int(item.LimitedStack - item.LimitedSells);
					}
				}
			});

			message.Int(item.ClubLevel);
			message.Boolean(item.HasOffer);
			message.Boolean(false);
			message.String(item.Name + ".png");
			return message;
		}
	}
}
