using System;
using System.Collections.Generic;
using Alias.Emulator.Hotel.Players.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Inventory.Events
{
	class RequestInventoryItemsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int totalItems = session.Player.Inventory.Items.Count;
			int pages = (int)Math.Ceiling((double)totalItems / 1000.0);
			if (pages == 0)
			{
				pages = 1;
			}

			int count = 0;
			int page = 0;
			List<InventoryItem> items = new List<InventoryItem>();
			foreach (InventoryItem item in session.Player.Inventory.Items.Values)
			{
				if (count == 0)
				{
					page++;
				}

				items.Add(item);
				count++;

				if (count == 1000)
				{
					session.Send(new InventoryItemsComposer(page, pages, items));
					count = 0;
					items.Clear();
				}
			}

			session.Send(new InventoryItemsComposer(page, pages, items));
		}
	}
}
