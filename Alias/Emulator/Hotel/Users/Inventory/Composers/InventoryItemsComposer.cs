using System.Collections.Generic;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class InventoryItemsComposer : IMessageComposer
	{
		private int page;
		private int pages;
		private List<InventoryItem> items;

		public InventoryItemsComposer(int page, int pages, List<InventoryItem> items)
		{
			this.page = page;
			this.pages = pages;
			this.items = items;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.InventoryItemsMessageComposer);
			message.Int(this.pages);
			message.Int(this.page - 1);
			message.Int(this.items.Count);
			this.items.ForEach(item =>
			{
				message.Int(item.Id);
				message.String("S"); //type
				message.Int(item.Id);
				message.Int(item.ItemData.Id);

				// ExtraData shit!!!
				message.Int(1);
				message.Int(item.IsLimited ? 256 : 0);
				message.String(""); //item Extradata
				if (item.IsLimited)
				{
					message.Int(item.LimitedNumber);
					message.Int(item.LimitedStack);
				}

				message.Boolean(false); //canRecyle
				message.Boolean(true); //canTrade
				message.Boolean(true); //canStack
				message.Boolean(false); //canSell

				message.Int(-1); // item Rent time
				message.Boolean(true); //??
				message.Int(-1); // roomId?? for rentables??

				//if (item.IsWallItem)
				{
					message.String(string.Empty); //??
					message.Int(0); //??
				}
			});
			return message;
		}
	}
}
