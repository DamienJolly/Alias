using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alias.Emulator.Hotel.Players.Inventory
{
    class InventoryComponent
    {
		private readonly InventoryDao _dao;
		private readonly Player _player;

		public IDictionary<int, InventoryItem> Items { get; set; }
		public IDictionary<int, InventoryBot> Bots { get; set; }
		public IDictionary<int, InventoryPet> Pets { get; set; }

		internal InventoryComponent(InventoryDao dao, Player player)
		{
			_dao = dao;
			_player = player;
			Bots = new Dictionary<int, InventoryBot>();
			Pets = new Dictionary<int, InventoryPet>();
			Items = new Dictionary<int, InventoryItem>();
		}

		public async Task Initialize()
		{
			Bots = await _dao.ReadPlayerBotsAsync(_player.Id);
			Pets = await _dao.ReadPlayerPetsAsync(_player.Id);
			Items = await _dao.ReadPlayerItemsAsync(_player.Id);
		}

		public async Task AddBot(InventoryBot bot)
		{
			await _dao.AddPlayerBotAsync(bot, _player.Id);
			Bots.Add(bot.Id, bot);
		}

		public async Task UpdateBot(InventoryBot bot)
		{
			await _dao.UpdatePlayerBotAsync(bot);
			if (bot.RoomId != 0)
			{
				Bots.Remove(bot.Id);
			}
			else
			{
				if (!Bots.ContainsKey(bot.Id))
				{
					Bots.Add(bot.Id, bot);
				}
			}
		}

		public async Task AddPet(InventoryPet pet)
		{
			await _dao.AddPlayerPetAsync(pet, _player.Id);
			Pets.Add(pet.Id, pet);
		}

		public async Task UpdatePet(InventoryPet pet)
		{
			await _dao.UpdatePlayerPetAsync(pet);
			if (pet.RoomId != 0)
			{
				Pets.Remove(pet.Id);
			}
			else
			{
				if (!Pets.ContainsKey(pet.Id))
				{
					Pets.Add(pet.Id, pet);
				}
			}
		}

		public async Task AddItem(InventoryItem item)
		{
			await _dao.AddPlayerItemAsync(item, _player.Id);
			Items.Add(item.Id, item);
		}

		public async Task UpdateItem(InventoryItem item)
		{
			await _dao.UpdatePlayerItemAsync(item, _player.Id);
			if (item.RoomId != 0)
			{
				Items.Remove(item.Id);
			}
			else
			{
				if (!Items.ContainsKey(item.Id))
				{
					Items.Add(item.Id, item);
				}
			}
		}

		public async Task RemoveItem(int id)
		{
			await _dao.RemovePlayerItemAsync(id);
			Items.Remove(id);
		}

		public bool TryGetItemById(int id, out InventoryItem item) => Items.TryGetValue(id, out item);

		public bool TryGetItemByName(string name, out InventoryItem item)
		{
			foreach (var i in Items.Values)
			{
				if (i.ItemData.Name == name)
				{
					item = i;
					return true;
				}
			}
			item = null;
			return false;
		}

		public bool TryGetBot(int id, out InventoryBot bot) => Bots.TryGetValue(id, out bot);

		public bool TryGetPet(int id, out InventoryPet pet) => Pets.TryGetValue(id, out pet);
	}
}
