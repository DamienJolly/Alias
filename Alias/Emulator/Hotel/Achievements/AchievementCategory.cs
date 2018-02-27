namespace Alias.Emulator.Hotel.Achievements
{
	public enum AchievementCategory
	{
		IDENTITY,
		EXPLORE,
		MUSIC,
		SOCIAL,
		GAMES,
		ROOM_BUILDER,
		PETS,
		TOOLS,
		EVENTS,
		INVISIBLE,
		OTHER
	}

	public class AchievementCategories
	{
		public static AchievementCategory GetCategoryFromString(string category)
		{
			switch (category)
			{
				case "identity": return AchievementCategory.IDENTITY;
				case "explore": return AchievementCategory.EXPLORE;
				case "music": return AchievementCategory.MUSIC;
				case "social": return AchievementCategory.SOCIAL;
				case "games": return AchievementCategory.GAMES;
				case "room_builder": return AchievementCategory.ROOM_BUILDER;
				case "pets": return AchievementCategory.PETS;
				case "tools": return AchievementCategory.TOOLS;
				case "events": return AchievementCategory.EVENTS;
				case "invisible": return AchievementCategory.INVISIBLE;
				case "other": default: return AchievementCategory.OTHER;
			}
		}
	}
}
