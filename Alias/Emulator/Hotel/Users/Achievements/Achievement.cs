using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Users.Achievements
{
    public class Achievement
    {
		private List<AchievementProgress> achievementProgress;
		private Habbo habbo;

		public Achievement(Habbo h)
		{
			this.achievementProgress = new List<AchievementProgress>();
			this.habbo = h;
		}

		public Habbo Habbo()
		{
			return this.habbo;
		}

		public List<AchievementProgress> RequestAchievementProgress()
		{
			return this.achievementProgress;
		}

		public AchievementProgress GetAchievementProgress(Hotel.Achievements.Achievement achievement)
		{
			return this.achievementProgress.Where(prog => prog.Achievement == achievement).FirstOrDefault();
		}

		public void Dispose()
		{
			this.achievementProgress.Clear();
			this.habbo = null;
		}
	}
}
