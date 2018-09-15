using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alias.Emulator.Hotel.Achievements
{
    sealed class AchievementManager
    {
		private readonly AchievementDao _dao;

		public Dictionary<string, Achievement> Achievements { get; set; }

		public AchievementManager(AchievementDao achievementDao)
		{
			_dao = achievementDao;
			Achievements = new Dictionary<string, Achievement>();
		}

		public async Task Initialize()
		{
			if (Achievements.Count > 0)
			{
				Achievements.Clear();
			}

			Achievements = await _dao.LoadAchievementsAsync();
		}

		public bool TryGetAchievement(string name, out Achievement achievement)
		{
			return Achievements.TryGetValue(name, out achievement);
		}
	}
}
