namespace Alias.Emulator.Hotel.Rooms
{
	public class RoomSettings
	{
		public int WhoMutes
		{
			get; set;
		} = 0;

		public int WhoKicks
		{
			get; set;
		} = 0;

		public int WhoBans
		{
			get; set;
		} = 0;

		public int ChatMode
		{
			get; set;
		} = 0;

		public int ChatSize
		{
			get; set;
		} = 0;

		public int ChatSpeed
		{
			get; set;
		} = 0;

		public int ChatFlood
		{
			get; set;
		} = 0;

		public int ChatDistance
		{
			get; set;
		} = 0;

		public bool AllowPets
		{
			get; set;
		} = false;

		public bool AllowPetsEat
		{
			get; set;
		} = false;

		public bool RoomBlocking
		{
			get; set;
		} = false;

		public bool HideWalls
		{
			get; set;
		} = false;

		public int WallHeight
		{
			get; set;
		} = 0;

		public int FloorSize
		{
			get; set;
		} = 0;
	}
}
