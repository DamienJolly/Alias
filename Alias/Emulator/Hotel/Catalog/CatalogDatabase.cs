using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Catalog
{
	public class CatalogDatabase
	{
		public static List<CatalogPage> ReadPages()
		{
			List<CatalogPage> pages = new List<CatalogPage>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `catalog_pages` ORDER BY `order_id` ASC, `caption` ASC").Rows)
				{
					CatalogPage item = new CatalogPage();
					item.Id = (int)row["id"];
					item.ParentId = (int)row["parent_id"];
					item.Name = (string)row["name"];
					item.Caption = (string)row["caption"];
					item.Icon = (int)row["icon"];
					item.Rank = (int)row["rank"];
					item.Order = (int)row["order_id"];
					item.HeaderImage = (string)row["header_image"];
					item.TeaserImage = (string)row["teaser_image"];
					item.SpecialImage = (string)row["special_image"];
					item.TextOne = (string)row["text_one"];
					item.TextTwo = (string)row["text_two"];
					item.TextDetails = (string)row["text_details"];
					item.TextTeaser = (string)row["text_teaser"];
					item.Layout = CatalogLayouts.GetLayoutFromString((string)row["layout"]);
					item.Enabled = AliasEnvironment.ToBool((string)row["enabled"]);
					item.Visible = AliasEnvironment.ToBool((string)row["visible"]);
					item.Items = ReadItems(item);
					pages.Add(item);
					row.Delete();
				}
			}
			return pages;
		}
		
		public static List<CatalogItem> ReadItems(CatalogPage page)
		{
			List<CatalogItem> items = new List<CatalogItem>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("pageId", page.Id);
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `catalog_items` WHERE `page_id` = @pageId").Rows)
				{
					CatalogItem item = new CatalogItem();
					item.Id = (int)row["id"];
					item.PageId = (int)row["page_Id"];
					item.ItemIds = (string)row["item_ids"];
					item.Name = (string)row["catalog_name"];
					item.Credits = (int)row["cost_credits"];
					item.Points = (int)row["cost_points"];
					item.PointsType = (int)row["points_type"];
					item.Amount = (int)row["amount"];
					item.LimitedStack = (int)row["limited_stack"];
					item.ClubLevel = (int)row["club_level"];
					item.CanGift = AliasEnvironment.ToBool((string)row["can_gift"]);
					item.HasOffer = AliasEnvironment.ToBool((string)row["have_offer"]);
					item.OfferId = (int)row["offer_id"];
					item.LimitedNumbers = ReadLimited(item.Id, item.LimitedStack);
					item.LimitedNumbers.Shuffle();

					if (item.OfferId != -1)
						page.AddOfferId(item.OfferId);

					items.Add(item);
					row.Delete();
				}
			}
			return items;
		}

		public static List<int> ReadLimited(int itemId, int size)
		{
			List<int> takenNumbers = new List<int>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("itemId", itemId);
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `catalog_limited_items` WHERE `item_id` = @itemId").Rows)
				{
					takenNumbers.Add((int)row["number"]);
					row.Delete();
				}
			}
			return CatalogManager.GetAvailableNumbers(takenNumbers, size);
		}

		public static void AddLimited(int itemId, int number)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("itemId", itemId);
				dbClient.AddParameter("number", number);
				dbClient.Query("INSERT INTO	`catalog_limited_items` (`item_id`, `number`) VALUES (@itemId, @number)");
			}
		}

		public static List<CatalogFeatured> ReadFeatured()
		{
			List<CatalogFeatured> featured = new List<CatalogFeatured>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `catalog_featured`").Rows)
				{
					CatalogFeatured item = new CatalogFeatured();
					item.SlotId = (int)row["slot_id"];
					item.Caption = (string)row["caption"];
					item.Image = (string)row["image"];
					item.Type = (int)row["type"];
					item.PageName = (string)row["page_name"];
					item.PageId = (int)row["page_id"];
					item.ProductName = (string)row["product_name"];
					featured.Add(item);
					row.Delete();
				}
			}
			return featured;
		}
	}
}
