using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Items.Crafting.Composers;
using Alias.Emulator.Hotel.Players.Inventory;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Items.Crafting.Events
{
    class RequestCraftingRecipesAvailableEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int itemId = message.PopInt();
			RoomItem rItem = room.ItemManager.GetItem(itemId);
			if (rItem == null)
			{
				return;
			}

			if (!Alias.Server.ItemManager.Crafting.TryGetCraftingTable(rItem.ItemData.Id, out CraftingTable table))
			{
				return;
			}


			Dictionary<string, int> items = new Dictionary<string, int>();

			int count = message.PopInt();
			for (int i = 0; i < count; i++)
			{
				int iItemId = message.PopInt();
				if (session.Player.Inventory.TryGetItemById(iItemId, out InventoryItem iItem))
				{
					continue;
				}

				if (!items.ContainsKey(iItem.ItemData.Name))
				{
					items.Add(iItem.ItemData.Name, 0);
				}

				items[iItem.ItemData.Name]++;
			}

			Dictionary<CraftingRecipe, bool> recipes = table.GetRecipes(items);

			bool found = false;
			int c = recipes.Count();
			foreach (KeyValuePair<CraftingRecipe, bool> recipe in recipes)
			{
				if (session.Player.Crafting.Recipes.Contains(recipe.Key.Id))
				{
					c--;
					continue;
				}

				if (recipe.Value)
				{
					found = true;
					break;
				}
			}
			session.Send(new CraftingRecipesAvailableComposer(c, found));
		}
	}
}
