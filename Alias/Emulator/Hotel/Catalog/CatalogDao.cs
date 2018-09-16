using Alias.Emulator.Database;
using Alias.Emulator.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alias.Emulator.Hotel.Catalog
{
	internal class CatalogDao : AbstractDao
    {
		internal async Task<IList<CatalogPage>> ReadPagesAsync()
		{
			IList<CatalogPage> pages = new List<CatalogPage>();

			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					CatalogPage item = new CatalogPage
					{
						Id = (int)reader.ReadData<uint>("rank"),
						ParentId = (int)reader.ReadData<uint>("rank"),
						Name = reader.ReadData<string>("name"),
						Caption = reader.ReadData<string>("caption"),
						Icon = (int)reader.ReadData<uint>("rank"),
						Rank = (int)reader.ReadData<uint>("rank"),
						Order = (int)reader.ReadData<uint>("rank"),
						HeaderImage = reader.ReadData<string>("header_image"),
						TeaserImage = reader.ReadData<string>("teaser_image"),
						SpecialImage = reader.ReadData<string>("special_image"),
						TextOne = reader.ReadData<string>("text_one"),
						TextTwo = reader.ReadData<string>("text_two"),
						TextDetails = reader.ReadData<string>("text_details"),
						TextTeaser = reader.ReadData<string>("text_teaser"),
						Layout = CatalogLayouts.GetLayoutFromString(reader.ReadData<string>("layout")),
						Enabled = reader.ReadBool("enabled"),
						Visible = reader.ReadBool("visible")
					};

					pages.Add(item);
				}
			}, "SELECT * FROM `catalog_pages` ORDER BY `order_id` ASC, `caption` ASC");

			return pages;
		}

		internal async Task<IList<CatalogBots>> ReadBotsAsync()
		{
			IList<CatalogBots> bots = new List<CatalogBots>();

			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					CatalogBots bot = new CatalogBots
					{
						ItemId = reader.ReadData<int>("item_id"),
						Name = reader.ReadData<string>("name"),
						Look = reader.ReadData<string>("look"),
						Motto = reader.ReadData<string>("motto"),
						Gender = reader.ReadData<string>("gender")
					};
					bots.Add(bot);
				}
			}, "SELECT * FROM `catalog_bot_items`");

			return bots;
		}

		internal async Task ReadItemsAsync(CatalogManager catalog)
		{
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					CatalogPage page = catalog.GetCatalogPage(reader.ReadData<int>("page_Id"));
					if (page == null)
					{
						continue;
					}

					CatalogItem item = new CatalogItem
					{
						Id = reader.ReadData<int>("id"),
						PageId = reader.ReadData<int>("page_Id"),
						ItemIds = reader.ReadData<string>("item_ids"),
						Name = reader.ReadData<string>("catalog_name"),
						Credits = reader.ReadData<int>("cost_credits"),
						Points = reader.ReadData<int>("cost_points"),
						PointsType = reader.ReadData<int>("points_type"),
						Amount = reader.ReadData<int>("amount"),
						LimitedStack = reader.ReadData<int>("limited_stack"),
						ClubLevel = reader.ReadData<int>("club_level"),
						CanGift = reader.ReadData<bool>("can_gift"),
						HasOffer = reader.ReadData<bool>("have_offer"),
						OfferId = reader.ReadData<int>("offer_id")
					};

					if (item.LimitedStack > 0)
					{
						item.LimitedNumbers = await ReadLimitedAsync(item.Id, item.LimitedStack);
						item.LimitedNumbers.Shuffle();
					}

					if (item.OfferId != -1)
					{
						page.AddOfferId(item.OfferId);
					}

					page.Items.Add(item);
				}
			}, "SELECT * FROM `catalog_items`");
		}

		internal async Task<IList<int>> ReadLimitedAsync(int itemId, int size)
		{
			List<int> availableNumbers = new List<int>();
			List<int> takenNumbers = new List<int>();

			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					takenNumbers.Add(reader.ReadData<int>("number"));
				}
			}, "SELECT * FROM `catalog_limited_items` WHERE `item_id` = @0", itemId);

			for (int i = 1; i <= size; i++)
			{
				if (!takenNumbers.Contains(i))
				{
					availableNumbers.Add(i);
				}
			}
			return availableNumbers;
		}

		internal async Task AddLimitedAsync(int itemId, int number)
		{
			await InsertAsync("INSERT INTO `catalog_limited_items`(`item_id`, `number`) VALUES(@0, @1)", itemId, number);
		}

		internal async Task<IList<CatalogFeatured>> ReadFeaturedAsync()
		{
			IList<CatalogFeatured> featured = new List<CatalogFeatured>();

			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					CatalogFeatured item = new CatalogFeatured
					{
						SlotId = reader.ReadData<int>("slot_id"),
						Caption = reader.ReadData<string>("caption"),
						Image = reader.ReadData<string>("image"),
						Type = int.Parse(reader.ReadData<string>("type")),
						PageName = reader.ReadData<string>("page_name"),
						PageId = reader.ReadData<int>("page_id"),
						ProductName = reader.ReadData<string>("product_name")
					};
					featured.Add(item);
				}
			}, "SELECT * FROM `catalog_featured`");

			return featured;
		}
	}
}
