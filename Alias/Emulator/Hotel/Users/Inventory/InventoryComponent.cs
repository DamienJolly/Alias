using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Users.Inventory
{
	sealed class InventoryComponent
	{
		private List<InventoryItem> floorItems;
		private List<InventoryBots> bots;
		public List<InventoryPets> Pets { get; set; }

		private Habbo habbo;

		public InventoryComponent(Habbo h)
		{
			this.floorItems = InventoryDatabase.ReadFloorItems(h.Id);
			this.bots = InventoryDatabase.ReadBots(h.Id);
			this.Pets = InventoryDatabase.ReadPets(h.Id);

			this.habbo = h;
		}

		public void AddItem(InventoryItem item)
		{
			InventoryDatabase.AddFurni(item);
			FloorItems.Add(item);
		}

		public void AddBot(InventoryBots bot)
		{
			bot.Id = InventoryDatabase.AddBot(bot, habbo.Id);
			bots.Add(bot);
		}

		public void UpdateBot(InventoryBots bot)
		{
			InventoryDatabase.UpdateBot(bot);
			if (bot.RoomId != 0)
			{
				bots.Remove(bot);
			}
			else
			{
				if (!bots.Contains(bot))
				{
					bots.Add(bot);
				}
			}
		}

		public void UpdateItem(InventoryItem item)
		{
			InventoryDatabase.UpdateFurni(item);
			if (item.RoomId != 0)
			{
				floorItems.Remove(item);
			}
			else
			{
				if (!floorItems.Contains(item))
				{
					floorItems.Add(item);
				}
			}
		}

		public void RemoveItem(InventoryItem item)
		{
			InventoryDatabase.RemoveFurni(item.Id);
			floorItems.Remove(item);
		}

		public Habbo Habbo()
		{
			return this.habbo;
		}

		public InventoryItem GetFloorItem(int itemId)
		{
			return this.floorItems.Where(item => item.Id == itemId).FirstOrDefault();
		}

		public InventoryBots GetBot(int botId)
		{
			return this.bots.Where(bot => bot.Id == botId).FirstOrDefault();
		}

		public List<InventoryItem> FloorItems
		{
			get
			{
				return this.floorItems;
			}
		}

		public List<InventoryBots> GetBots
		{
			get
			{
				return this.bots;
			}
		}
	}
}
