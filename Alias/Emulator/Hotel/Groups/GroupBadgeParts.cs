namespace Alias.Emulator.Hotel.Groups
{
	class GroupBadgeParts
	{
		public int Id
		{
			get; set;
		}

		public string AssetOne
		{
			get; set;
		}

		public string AssetTwo
		{
			get; set;
		}

		public BadgePartType Type
		{
			get; set;
		}

		public GroupBadgeParts(int id, string assetOne, string assetTwo, string type)
		{
			this.Id = id;
			this.AssetOne = assetOne;
			this.AssetTwo = assetTwo;

			switch (type.ToLower())
			{
				case "base":
					Type = BadgePartType.BASE;
					break;

				case "symbol":
					Type = BadgePartType.SYMBOL;
					break;

				case "base_color":
					Type = BadgePartType.BASE_COLOUR;
					break;

				case "symbol_color":
					Type = BadgePartType.SYMBOL_COLOUR;
					break;

				case "other_color":
					Type = BadgePartType.BACKGROUND_COLOUR;
					break;
			}
		}
	}
}
