using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Wardrobe
{
    internal class WardrobeDao : AbstractDao
    {
		internal async Task<Dictionary<int, WardrobeItem>> ReadPlayerWardrobeItemsAsync(int id)
		{
			Dictionary<int, WardrobeItem> wardrobeItems = new Dictionary<int, WardrobeItem>();
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					WardrobeItem wardrobeItem = new WardrobeItem(reader);
					if (wardrobeItems.ContainsKey(wardrobeItem.SlotId))
					{
						wardrobeItems.Add(wardrobeItem.SlotId, wardrobeItem);
					}
				}
			}, "SELECT `slot_id`, `figure`, `gender` FROM `wardrobe` WHERE `user_id` = @0", id);
			return wardrobeItems;
		}

		internal async Task AddPlayerWardrobeItemAsync(WardrobeItem item, int userId)
		{
			await InsertAsync("INSERT INTO `wardrobe` (`slot_id`, `figure`, `gender`, `user_id`) VALUES (@0, @1, @2, @3);", item.SlotId, item.Figure, item.Gender, userId);
		}

		internal async Task UpdatePlayerWardrobeItemAsync(WardrobeItem item, int userId)
		{
			await InsertAsync("UPDATE `wardrobe` SET `figure` = @0, `gender` = @1 WHERE `slot_id` = @2 AND `user_id` = @3;", item.Figure, item.Gender, item.SlotId, userId);
		}
	}
}
