using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class CatalogPageComposer : MessageComposer
	{
		CatalogPage page;
		Habbo habbo;
		string mode;

		public CatalogPageComposer(CatalogPage page, Habbo habbo, string mode)
		{
			this.page = page;
			this.habbo = habbo;
			this.mode = mode;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.CatalogPageMessageComposer);
			message.Int(this.page.Id);
			message.String(this.mode);
			this.page.GetLayout().Serialize(message, this.page);

			if (this.page.Layout == CatalogLayout.RECENT_PURCHASES)
			{
				message.Int(0);
				//todo:
			}
			else
			{
				message.Int(this.page.Items.Count);
				this.page.Items.ForEach(item =>
				{
					message.Int(item.Id);
					message.String(item.Name);
					message.Boolean(false); //rentable
					message.Int(item.Credits);
					message.Int(item.Points);
					message.Int(item.PointsType);
					message.Boolean(true); //can gift

					message.Int(item.GetItems().Count);
					item.GetItems().ForEach(data =>
					{
						message.String("s"); //todo:
						//if (data.Type.Equals("b"))
						{
							//message.String(data.name);
						}
						//else
						{
							message.Int(data.Id);

							//todo: extradata
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
					message.Boolean(false); //dunno
					message.String(item.Name + ".png");
				});
			}

			message.Int(0);
			message.Boolean(false);

			if (this.page.Layout == CatalogLayout.FRONTPAGE || this.page.Layout == CatalogLayout.FRONTPAGE_FEATURED)
			{
				message.Int(CatalogManager.GetFeaturedPages().Count);
				CatalogManager.GetFeaturedPages().ForEach(feature =>
				{
					message.Int(feature.SlotId);
					message.String(feature.Caption);
					message.String(feature.Image);
					message.Int(feature.Type);
					switch (feature.Type)
					{
						case 0:
						default:
							message.String(feature.PageName); break;
						case 1:
							message.Int(feature.PageId); break;
						case 2:
							message.String(feature.ProductName); break;
					}
					message.Int(-1);
				});

			}
			return message;
		}
	}
}
