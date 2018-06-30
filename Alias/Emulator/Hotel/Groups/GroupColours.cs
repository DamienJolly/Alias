namespace Alias.Emulator.Hotel.Groups
{
	class GroupColours
	{
		public int Id
		{
			get; set;
		}

		public string Colour
		{
			get; set;
		}

		public GroupColours(int id, string colour)
		{
			this.Id = id;
			this.Colour = colour;
		}
	}
}
