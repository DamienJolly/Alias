using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Users.Achievements
{
    sealed class AchievementComponent
    {
		public Dictionary<string, int> Achievements
		{
			get; set;
		}

		private Habbo habbo;

		public AchievementComponent(Habbo h)
		{
			this.Achievements = AchievementDatabase.ReadAchievements(h.Id);
			this.habbo = h;
		}

		public Habbo Habbo()
		{
			return this.habbo;
		}

		public Dictionary<string, int> RequestAchievements()
		{
			return this.Achievements;
		}

		public bool GetAchievementProgress(string name, out int progress)
		{
			return this.Achievements.TryGetValue(name, out progress);
		}

		public void Dispose()
		{
			this.Achievements.Clear();
			this.habbo = null;
		}

		public void AddAchievement(string name, int progress) => Achievements.Add(name, progress);
	}
}
