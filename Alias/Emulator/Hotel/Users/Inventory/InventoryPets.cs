namespace Alias.Emulator.Hotel.Users.Inventory
{
	public class InventoryPets
	{
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public int Type
		{
			get; set;
		}

		public int Race
		{
			get; set;
		}

		public string Colour
		{
			get; set;
		}

		public int RoomId
		{
			get; set;
		} = 0;
	}
}
