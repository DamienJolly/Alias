namespace Alias.Emulator.Hotel.Users.Inventory
{
	public class InventoryBots
	{
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public string Motto
		{
			get; set;
		}

		public string Look
		{
			get; set;
		}

		public string Gender
		{
			get; set;
		}

		public int DanceId
		{
			get; set;
		} = 0;

		public int EffectId
		{
			get; set;
		} = 0;

		public bool CanWalk
		{
			get; set;
		} = true;

		public int RoomId
		{
			get; set;
		} = 0;
	}
}
