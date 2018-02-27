using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Achievements
{
    public class Achievement
    {
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public AchievementCategory Category
		{
			get; set;
		}

		public List<AchievementLevel> Levels
		{
			get; set;
		}

		public AchievementLevel GetLevelForProgress(int progress)
		{
			AchievementLevel l = null;

			foreach (AchievementLevel level in Levels)
			{
				if (l == null && level.Level == 1)
				{
					l = level;
				}

				if (progress >= level.Progress)
				{
					if (l != null)
					{
						if (l.Level > level.Level)
						{
							continue;
						}
					}

					l = level;
				}
			}

			return l;
		}

		public AchievementLevel GetNextLevel(int currentLevel)
		{
			foreach (AchievementLevel level in Levels)
			{
				if (level.Level == (currentLevel + 1))
				{
					return level;
				}
			}

			return null;
		}
	}
}
