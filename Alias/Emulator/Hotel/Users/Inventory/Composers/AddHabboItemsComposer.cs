using System.Collections.Generic;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class AddHabboItemsComposer : IMessageComposer
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

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.AddHabboItemsMessageComposer);
			message.Int(1);
			message.Int(1);
			message.Int(this.items.Count);
			this.items.ForEach(item =>
			{
				message.Int(item.Id);
			});
			return message;
		}
	}
}
