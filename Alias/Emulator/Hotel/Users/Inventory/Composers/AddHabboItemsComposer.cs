using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class AddHabboItemsComposer : IPacketComposer
	{
		List<InventoryItem> items;

		public AddHabboItemsComposer(InventoryItem item)
		{
			this.items = new List<InventoryItem>();
			this.items.Add(item);
		}

		public AddHabboItemsComposer(List<InventoryItem> items)
		{
			this.items = items;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AddHabboItemsMessageComposer);
			message.WriteInteger(1);
			message.WriteInteger(1);
			message.WriteInteger(this.items.Count);
			this.items.ForEach(item =>
			{
				message.WriteInteger(item.Id);
			});
			return message;
		}
	}
}
