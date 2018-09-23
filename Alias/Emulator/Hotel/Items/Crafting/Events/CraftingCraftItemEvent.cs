using System.Collections.Generic;
using Alias.Emulator.Hotel.Items.Crafting.Composers;
using Alias.Emulator.Hotel.Players.Inventory;
using Alias.Emulator.Hotel.Players.Inventory.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Items.Crafting.Events
{
    class CraftingCraftItemEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
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
				session.Send(new CraftingResultComposer(null));
				return;
			}

			if (!Alias.Server.ItemManager.Crafting.TryGetCraftingTable(rItem.ItemData.Id, out CraftingTable table))
			{
				session.Send(new CraftingResultComposer(null));
				return;
			}

			string recipeName = message.PopString();
			if (!table.TryGetRecipe(recipeName, out CraftingRecipe recipe))
			{
				session.Send(new CraftingResultComposer(null));
				return;
			}

			List<InventoryItem> toRemove = new List<InventoryItem>();
			foreach (KeyValuePair<string, int> ingredient in recipe.Ingredients)
			{
				for (int i = 0; i < ingredient.Value; i++)
				{
					if(!session.Player.Inventory.TryGetItemByName(ingredient.Key, out InventoryItem iItem))
					{
						session.Send(new CraftingResultComposer(null));
						return;
					}

					toRemove.Add(iItem);
				}
			}
			
			session.Send(new CraftingResultComposer(recipe));
			await session.Player.Crafting.AddRecipe(recipe.Id);

			InventoryItem habboItem = new InventoryItem(-1, 0, 0, recipe.Reward.Id, session.Player.Id, "");
			await session.Player.Inventory.AddItem(habboItem);
			session.Send(new AddPlayerItemsComposer(habboItem));

			foreach (InventoryItem item in toRemove)
			{
				await session.Player.Inventory.RemoveItem(item.Id);
				session.Send(new RemovePlayerItemComposer(item.Id));
			}

			session.Send(new InventoryRefreshComposer());
		}
	}
}
