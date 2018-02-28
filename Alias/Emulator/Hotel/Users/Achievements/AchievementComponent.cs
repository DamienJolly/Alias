using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Users.Achievements
{
    public class AchievementComponent
    {
		private List<AchievementProgress> achievementProgress;
		private Habbo habbo;

		public AchievementComponent(Habbo h)
		{
			this.achievementProgress = AchievementDatabase.ReadAchievements(h.Id);
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
