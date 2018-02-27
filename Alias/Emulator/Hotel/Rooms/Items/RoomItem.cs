using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Rooms.Items
{
	public class RoomItem
	{
		public int Id
		{
			get; set;
		}

		public ItemPosition Position
		{
			get; set;
		}

		public Room Room
		{
			get; set;
		}

		public int Owner
		{
			get; set;
		}

		public int Mode
		{
			get; set;
		} = 0;

		public ItemData ItemData
		{
			get; set;
		}

		public string Username
		{
			get
			{
				return (string)UserDatabase.Variable(this.Owner, "Username");
			}
		}
	}
}
