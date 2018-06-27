namespace Alias.Emulator.Hotel.Users.Wardrobe
{
    class WardrobeItem
    {
		public int UserId
		{
			get; set;
		}

		public int SlotId
		{
			get; set;
		}

		public string Figure
		{
			get; set;
		}

		public string Gender
		{
			get; set;
		}

		public WardrobeItem(int userId, int slotId, string figure, string gender)
		{
			this.UserId = userId;
			this.SlotId = slotId;
			this.Figure = figure;
			this.Gender = gender;
		}
	}
}
