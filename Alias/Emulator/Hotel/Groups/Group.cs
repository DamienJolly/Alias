namespace Alias.Emulator.Hotel.Groups
{
    class Group
    {
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public string Description
		{
			get; set;
		}

		public int OwnerId
		{
			get; set;
		}

		public int CreatedAt
		{
			get; set;
		}

		public int RoomId
		{
			get; set;
		}

		public int ColourOne
		{
			get; set;
		}

		public int ColourTwo
		{
			get; set;
		}

		public string Badge
		{
			get; set;
		}

		public Group(int id, string name, string description, int ownerId, int createdAt, int roomId, int colourOne, int colourTwo, string badge)
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
			this.OwnerId = ownerId;
			this.CreatedAt = createdAt;
			this.RoomId = roomId;
			this.ColourOne = colourOne;
			this.ColourTwo = colourTwo;
			this.Badge = badge;
		}
	}
}
