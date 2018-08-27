using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Achievements
{
    sealed class AchievementManager
    {
		public Dictionary<string, Achievement> Achievements
		{
			get; set;
		}

		public AchievementManager()
		{
			this.Achievements = new Dictionary<string, Achievement>();
		}

		public void Initialize()
		{
			if (this.Achievements.Count > 0)
			{
				this.Achievements.Clear();
			}

			this.Achievements = AchievementDatabase.ReadAchievements();
		}

		public bool TryGetAchievement(string name, out Achievement achievement)
		{
			return this.Achievements.TryGetValue(name, out achievement);
		}
	}
}
