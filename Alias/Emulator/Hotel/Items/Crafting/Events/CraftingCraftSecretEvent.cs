using System.Collections.Generic;
using Alias.Emulator.Hotel.Items.Crafting.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Items.Crafting.Events
{
    class CraftingCraftSecretEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
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

			List<InventoryItem> toRemove = new List<InventoryItem>();
			Dictionary<string, int> items = new Dictionary<string, int>();

			int count = message.PopInt();
			for (int i = 0; i < count; i++)
			{
				InventoryItem iItem = session.Habbo.Inventory.GetFloorItem(message.PopInt());
				if (iItem == null)
				{
					session.Send(new CraftingResultComposer(null));
					return;
				}

				toRemove.Add(iItem);

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

			InventoryItem rewardItem = new InventoryItem
			{
				Id = 0,
				LimitedNumber = 0,
				LimitedStack = 0,
				ItemData = Alias.Server.ItemManager.GetItemData(recipe.Reward.Id),
				Mode = 0,
				ExtraData = ""
			};

			session.Send(new CraftingResultComposer(recipe));
			session.Habbo.Inventory.AddItem(rewardItem);
			session.Send(new AddHabboItemsComposer(rewardItem));
			toRemove.ForEach(item =>
			{
				session.Habbo.Inventory.RemoveItem(item);
				session.Send(new RemoveHabboItemComposer(item.Id));
			});
			session.Send(new InventoryRefreshComposer());
		}
	}
}
