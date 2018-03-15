using Alias.Emulator.Hotel.Items;

namespace Alias.Emulator.Hotel.Users.Inventory
{
	public class InventoryItem
	{
		public int Id
		{
			get; set;
		}

		public int LimitedNumber
		{
			get; set;
		}

		public int LimitedStack
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

		public ItemData ItemData
		{
			get; set;
		}
	}
}
