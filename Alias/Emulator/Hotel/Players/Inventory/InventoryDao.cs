using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Inventory
{
    class InventoryDao : AbstractDao
	{
		internal async Task<Dictionary<int, InventoryBot>> ReadPlayerBotsAsync(int id)
		{
			Dictionary<int, InventoryBot> bots = new Dictionary<int, InventoryBot>();
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					InventoryBot bot = new InventoryBot(reader);
					if (!bots.ContainsKey(bot.Id))
					{
						bots.Add(bot.Id, bot);
					}
				}
			}, "SELECT * FROM `bots` WHERE `user_id` = @0 AND `room_id` = 0", id);
			return bots;
		}

		internal async Task<Dictionary<int, InventoryPet>> ReadPlayerPetsAsync(int id)
		{
			Dictionary<int, InventoryPet> pets = new Dictionary<int, InventoryPet>();
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					InventoryPet pet = new InventoryPet(reader);
					if (!pets.ContainsKey(pet.Id))
					{
						pets.Add(pet.Id, pet);
					}
				}
			}, "SELECT * FROM `pets` WHERE `user_id` = @0 AND `room_id` = 0", id);
			return pets;
		}

		internal async Task<Dictionary<int, InventoryItem>> ReadPlayerItemsAsync(int id)
		{
			Dictionary<int, InventoryItem> items = new Dictionary<int, InventoryItem>();
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					InventoryItem item = new InventoryItem(reader);
					if (!items.ContainsKey(item.Id))
					{
						items.Add(item.Id, item);
					}
				}
			}, "SELECT * FROM `items` WHERE `user_id` = @0 AND `room_id` = 0", id);
			return items;
		}

		internal async Task AddPlayerBotAsync(InventoryBot bot, int userId)
		{
			bot.Id = await InsertAsync("INSERT INTO `bots` (`name`, `motto`, `look`, `gender`, `user_id`) VALUES (@0, @1, @2, @3, @4);", bot.Name, bot.Motto, bot.Look, bot.Gender, userId);
		}

		internal async Task UpdatePlayerBotAsync(InventoryBot bot)
		{
			await InsertAsync("UPDATE `bots` SET `room_id` = @0 WHERE `id` = @1;", bot.RoomId, bot.Id);
		}

		internal async Task AddPlayerPetAsync(InventoryPet pet, int userId)
		{
			pet.Id = await InsertAsync("INSERT INTO `pets` (`name`, `type`, `race`, `colour`, `user_id`) VALUES (@0, @1, @2, @3, @4);", pet.Name, pet.Type, pet.Race, pet.Colour, userId);
		}

		internal async Task UpdatePlayerPetAsync(InventoryPet pet)
		{
			await InsertAsync("UPDATE `pets` SET `room_id` = @0 WHERE `id` = @1;", pet.RoomId, pet.Id);
		}

		internal async Task AddPlayerItemAsync(InventoryItem item, int userId)
		{
			item.Id = await InsertAsync("INSERT INTO `items` (`base_id`, `limited_stack`, `limited_number`, `extradata`, `user_id`) VALUES (@0, @1, @2, @3, @4);", item.ItemData.Id, item.LimitedStack, item.LimitedNumber, item.ExtraData, userId);
		}

		internal async Task UpdatePlayerItemAsync(InventoryItem item, int userId)
		{
			await InsertAsync("UPDATE `items` SET `base_id` = @1, `extradata` = @2, `room_id` = @3, `user_id` = @4, WHERE `id` = @0;", item.Id, item.ItemData.Id, item.ExtraData, item.RoomId, userId);
		}

		internal async Task RemovePlayerItemAsync(int id)
		{
			await InsertAsync("DELETE FROM `items` WHERE `id` = @0;", id);
		}
	}
}
