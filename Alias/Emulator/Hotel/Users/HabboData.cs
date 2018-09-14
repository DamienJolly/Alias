using System.Collections.Generic;
using Alias.Emulator.Hotel.Catalog;

namespace Alias.Emulator.Hotel.Users
{
    public class HabboData
    {
		public int Id { get; set; }
		public string Username { get; set; }
		public string Mail { get; set; }
		public string Look { get; set; }
		public string Gender { get; set; }
		public string Motto { get; set; }
		public int Rank { get; set; }
		public int ClubLevel { get; set; }
		public int Credits { get; set; }
		public int HomeRoom { get; set; }
		public int AchievementScore { get; set; }
		public bool Muted { get; set; }
		public bool AllowTrading { get; set; }
		public int GroupId { get; set; }
		public List<int> Groups { get; set; }
		public Queue<CatalogItem> RecentPurchases { get; set; } = new Queue<CatalogItem>();

		public void AddPurchase(CatalogItem item)
		{
			if (!RecentPurchases.Contains(item))
			{
				RecentPurchases.Enqueue(item);
			}

			while (RecentPurchases.Count > 12)
			{
				RecentPurchases.Dequeue();
			}
		}
	}
}
