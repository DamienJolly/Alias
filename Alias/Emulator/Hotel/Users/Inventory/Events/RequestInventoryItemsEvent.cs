using System;
using System.Collections.Generic;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Inventory.Events
{
	public class RequestInventoryItemsEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int totalItems = session.Habbo().Inventory().FloorItems().Count;
			int pages = (int)Math.Ceiling((double)totalItems / 1000.0);
			if (pages == 0)
			{
				pages = 1;
			}

			int count = 0;
			int page = 0;
			List<InventoryItem> items = new List<InventoryItem>();
			foreach (var item in session.Habbo().Inventory().FloorItems())
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
