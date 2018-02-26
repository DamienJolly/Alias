using System.Collections.Generic;
using Alias.Emulator.Hotel.Items;

namespace Alias.Emulator.Hotel.Catalog
{
	public class CatalogItem
	{
		public int Id
		{
			get; set;
		}

		public int PageId
		{
			get; set;
		}

		public string ItemIds
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public int Credits
		{
			get; set;
		}

		public int Points
		{
			get; set;
		}

		public int PointsType
		{
			get; set;
		}

		public int Amount
		{
			get; set;
		}

		public int LimitedStack
		{
			get; set;
		}

		public int LimitedSells
		{
			get; set;
		}

		public int ClubLevel
		{
			get; set;
		}

		public bool HasOffer
		{
			get; set;
		}

		public int OfferId
		{
			get; set;
		}

		public bool IsLimited
		{
			get
			{
				return this.LimitedStack > 0;
			}
		}

		public int GetItemAmount(int id)
		{
			if (!string.IsNullOrEmpty(this.ItemIds))
			{
				string[] itemIds = this.ItemIds.Split(';');
				foreach (string itemId in itemIds)
				{
					string am = "1";
					string bit = itemId;
					if (itemId.Contains(":"))
					{
						am = bit.Split(':')[1];
						bit = bit.Split(':')[0];

						int identifier = 0;
						if (int.TryParse(bit, out identifier))
						{
							if (identifier != id)
								continue;

							if (int.TryParse(am, out identifier))
								return identifier;
						}
					}
				}

			}
			return this.Amount;
		}

		public List<ItemData> GetItems()
		{
			List<ItemData> items = new List<ItemData>();

			if (!string.IsNullOrEmpty(this.ItemIds))
			{
				string[] itemIds = this.ItemIds.Split(';');

				foreach (string itemId in itemIds)
				{
					string id = itemId;
					if (id.Contains(":"))
					{
						id = id.Split(':')[0];
					}

					int identifier = 0;
					if (int.TryParse(id, out identifier))
					{
						if (identifier > 0)
						{
							ItemData item = ItemManager.GetItemData(identifier);

							if (item != null)
								items.Add(item);
						}
					}
				}
			}

			return items;
		}
	}
}