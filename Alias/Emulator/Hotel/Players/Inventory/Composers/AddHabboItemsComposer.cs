using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Inventory.Composers
{
	public class AddPlayerItemsComposer : IPacketComposer
	{
		private List<InventoryItem> _items;

		public AddPlayerItemsComposer(InventoryItem item)
		{
			_items = new List<InventoryItem>
			{
				item
			};
		}

		public AddPlayerItemsComposer(List<InventoryItem> items)
		{
			_items = items;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AddPlayerItemsMessageComposer);
			message.WriteInteger(1);
			message.WriteInteger(1);
			message.WriteInteger(_items.Count);
			_items.ForEach(item =>
			{
				message.WriteInteger(item.Id);
			});
			return message;
		}
	}
}
