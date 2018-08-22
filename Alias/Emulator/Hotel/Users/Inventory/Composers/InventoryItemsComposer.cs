using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class InventoryItemsComposer : IPacketComposer
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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.InventoryItemsMessageComposer);
			message.WriteInteger(this.pages);
			message.WriteInteger(this.page - 1);
			message.WriteInteger(this.items.Count);
			this.items.ForEach(item =>
			{
				message.WriteInteger(item.Id);
				message.WriteString(item.ItemData.Type.ToUpper());
				message.WriteInteger(item.Id);
				message.WriteInteger(item.ItemData.SpriteId);
				
				message.WriteInteger(1);
				message.WriteInteger(item.IsLimited ? 256 : 0);
				message.WriteString(item.ItemData.ExtraData);
				if (item.IsLimited)
				{
					message.WriteInteger(item.LimitedNumber);
					message.WriteInteger(item.LimitedStack);
				}

				message.WriteBoolean(false); //canRecyle
				message.WriteBoolean(true); //canTrade
				message.WriteBoolean(true); //canStack
				message.WriteBoolean(false); //canSell

				message.WriteInteger(-1); // item Rent time
				message.WriteBoolean(true); //??
				message.WriteInteger(-1); // roomId?? for rentables??

				if (item.ItemData.Type == "s")
				{
					message.WriteString(string.Empty); //??
					message.WriteInteger(0); //??
				}
			});
			return message;
		}
	}
}
