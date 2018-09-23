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
    class CraftingCraftSecretEvent : IPacketEvent
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

			List<int> toRemove = new List<int>();
			Dictionary<string, int> items = new Dictionary<string, int>();

			int count = message.PopInt();
			for (int i = 0; i < count; i++)
			{
				int iItemId = message.PopInt();
				if (session.Player.Inventory.TryGetItemById(iItemId, out InventoryItem iItem))
				{
					session.Send(new CraftingResultComposer(null));
					return;
				}

				toRemove.Add(iItem.Id);
				if (!items.ContainsKey(iItem.ItemData.Name))
				{
					items.Add(iItem.ItemData.Name, 0);
				}

				items[iItem.ItemData.Name]++;
			}

			if (!table.TryGetRecipe(items, out CraftingRecipe recipe))
			{
				session.Send(new CraftingResultComposer(null));
				return;
			}
			
			session.Send(new CraftingResultComposer(recipe));
			await session.Player.Crafting.AddRecipe(recipe.Id);

			InventoryItem rewardItem = new InventoryItem(-1, 0, 0, recipe.Reward.Id, session.Player.Id, "");
			await session.Player.Inventory.AddItem(rewardItem);
			session.Send(new AddPlayerItemsComposer(rewardItem));

			foreach (int id in toRemove)
			{
				await session.Player.Inventory.RemoveItem(id);
				session.Send(new RemovePlayerItemComposer(id));
			}
			session.Send(new InventoryRefreshComposer());
		}
	}
}
