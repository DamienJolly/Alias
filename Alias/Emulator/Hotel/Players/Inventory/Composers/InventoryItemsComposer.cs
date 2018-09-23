using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Inventory.Composers
{
	public class InventoryItemsComposer : IPacketComposer
	{
		private int _page;
		private int _pages;
		private List<InventoryItem> _items;

		public InventoryItemsComposer(int page, int pages, List<InventoryItem> items)
		{
			_page = page;
			_pages = pages;
			_items = items;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.InventoryItemsMessageComposer);
			message.WriteInteger(_pages);
			message.WriteInteger(_page - 1);
			message.WriteInteger(_items.Count);
			_items.ForEach(item =>
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
				message.WriteBoolean(item.ItemData.CanStack && !item.IsLimited);
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
