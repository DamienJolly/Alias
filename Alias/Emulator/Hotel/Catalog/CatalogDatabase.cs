/*
 * Deprecated!
using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Utilities;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Catalog
{
	class CatalogDatabase
	{
		public static List<CatalogPage> ReadPages()
		{
			List<CatalogPage> pages = new List<CatalogPage>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `catalog_pages` ORDER BY `order_id` ASC, `caption` ASC"))
				{
					while (Reader.Read())
					{
						CatalogPage item = new CatalogPage
						{
							Id           = Reader.GetInt32("id"),
							ParentId     = Reader.GetInt32("parent_id"),
							Name         = Reader.GetString("name"),
							Caption      = Reader.GetString("caption"),
							Icon         = Reader.GetInt32("icon"),
							Rank         = Reader.GetInt32("rank"),
							Order        = Reader.GetInt32("order_id"),
							HeaderImage  = Reader.GetString("header_image"),
							TeaserImage  = Reader.GetString("teaser_image"),
							SpecialImage = Reader.GetString("special_image"),
							TextOne      = Reader.GetString("text_one"),
							TextTwo      = Reader.GetString("text_two"),
							TextDetails  = Reader.GetString("text_details"),
							TextTeaser   = Reader.GetString("text_teaser"),
							Layout       = CatalogLayouts.GetLayoutFromString(Reader.GetString("layout")),
							Enabled      = Reader.GetBoolean("enabled"),
							Visible      = Reader.GetBoolean("visible")
						};
						pages.Add(item);
					}
				}
			}
			return pages;
		}

		public static List<CatalogBots> ReadBots()
		{
			List<CatalogBots> bots = new List<CatalogBots>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `catalog_bot_items`"))
				{
					while (Reader.Read())
					{
						CatalogBots bot = new CatalogBots
						{
							ItemId = Reader.GetInt32("item_id"),
							Name   = Reader.GetString("name"),
							Look   = Reader.GetString("look"),
							Motto  = Reader.GetString("motto"),
							Gender = Reader.GetString("gender")
						};
						bots.Add(bot);
					}
				}
			}
			return bots;
		}

		public static void ReadItems(CatalogManager catalog)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `catalog_items`"))
				{
					while (Reader.Read())
					{
						CatalogPage page = catalog.GetCatalogPage(Reader.GetInt32("page_Id"));
						if (page == null)
						{
							continue;
						}

						CatalogItem item = new CatalogItem
						{
							Id           = Reader.GetInt32("id"),
							PageId       = Reader.GetInt32("page_Id"),
							ItemIds      = Reader.GetString("item_ids"),
							Name         = Reader.GetString("catalog_name"),
							Credits      = Reader.GetInt32("cost_credits"),
							Points       = Reader.GetInt32("cost_points"),
							PointsType   = Reader.GetInt32("points_type"),
							Amount       = Reader.GetInt32("amount"),
							LimitedStack = Reader.GetInt32("limited_stack"),
							ClubLevel    = Reader.GetInt32("club_level"),
							CanGift      = Reader.GetBoolean("can_gift"),
							HasOffer     = Reader.GetBoolean("have_offer"),
							OfferId      = Reader.GetInt32("offer_id")
						};

						if (item.LimitedStack > 0)
						{
							item.LimitedNumbers = ReadLimited(item.Id, item.LimitedStack);
							item.LimitedNumbers.Shuffle();
						}

						if (item.OfferId != -1)
						{
							page.AddOfferId(item.OfferId);
						}

						page.Items.Add(item);
					}
				}
			}
		}

		public static List<int> ReadLimited(int itemId, int size)
		{
			List<int> availableNumbers = new List<int>();
			List<int> takenNumbers = new List<int>();

			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("itemId", itemId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `catalog_limited_items` WHERE `item_id` = @itemId"))
				{
					while (Reader.Read())
					{
						takenNumbers.Add(Reader.GetInt32("number"));
					}
				}
			}

			for (int i = 1; i <= size; i++)
			{
				if (!takenNumbers.Contains(i))
				{
					availableNumbers.Add(i);
				}
			}
			return availableNumbers;
		}

		public static void AddLimited(int itemId, int number)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("itemId", itemId);
				dbClient.AddParameter("number", number);
				dbClient.Query("INSERT INTO	`catalog_limited_items` (`item_id`, `number`) VALUES (@itemId, @number)");
			}
		}

		public static List<CatalogFeatured> ReadFeatured()
		{
			List<CatalogFeatured> featured = new List<CatalogFeatured>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `catalog_featured`"))
				{
					while (Reader.Read())
					{
						CatalogFeatured item = new CatalogFeatured
						{
							SlotId      = Reader.GetInt32("slot_id"),
							Caption     = Reader.GetString("caption"),
							Image       = Reader.GetString("image"),
							Type        = Reader.GetInt32("type"),
							PageName    = Reader.GetString("page_name"),
							PageId      = Reader.GetInt32("page_id"),
							ProductName = Reader.GetString("product_name")
						};
						featured.Add(item);
					}
				}
			}
			return featured;
		}
	}
}
*/
