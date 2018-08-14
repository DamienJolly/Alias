using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Hotel.Users.Achievements;
using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Hotel.Achievements.Composers;
using Alias.Emulator.Hotel.Users.Badges;
using Alias.Emulator.Hotel.Users.Composers;

namespace Alias.Emulator.Hotel.Achievements
{
    sealed class AchievementManager
    {
		private List<Achievement> _achievements;

		public AchievementManager()
		{
			this._achievements = new List<Achievement>();
		}

		public void Initialize()
		{
			if (this._achievements.Count > 0)
			{
				this._achievements.Clear();
			}

			this._achievements = AchievementDatabase.ReadAchievements();
		}

		public List<Achievement> GetAchievements()
		{
			return this._achievements.Where(ach => ach.Category != AchievementCategory.INVISIBLE).ToList();
		}

		public Achievement GetAchievement(string name)
		{
			return this._achievements.Where(ach => ach.Name == name).FirstOrDefault();
		}

		public void ProgressAchievement(int habboId, Achievement achievement, int amount = 1)
		{
			if (achievement != null)
			{
				Habbo habbo = Alias.Server.SocketServer.SessionManager.SessionById(habboId).Habbo;
				if (habbo != null)
				{
					ProgressAchievement(habbo, achievement, amount);
				}
				else
				{
					AchievementDatabase.AddUserAchievement(habbo, achievement, amount);
				}
			}
		}

		public void ProgressAchievement(Habbo habbo, Achievement achievement, int amount = 1)
		{
			if (achievement == null)
			{
				return;
			}

			if (habbo == null)
			{
				return;
			}
			
			int progress = 0;
			AchievementProgress currentProgress = habbo.Achievements.GetAchievementProgress(achievement);
			if (currentProgress == null)
			{
				AchievementDatabase.AddUserAchievement(habbo, achievement, amount);
				AchievementProgress newAchievement = new AchievementProgress()
				{
					Achievement = achievement,
					Progress = amount
				};
				habbo.Achievements.RequestAchievementProgress().Add(newAchievement);
			}

			AchievementLevel oldLevel = achievement.GetLevelForProgress(progress);
			if (oldLevel == null)
			{
				return;
			}

			if (oldLevel.Level == achievement.Levels.Count && progress == oldLevel.Progress) //Maximum achievement reached.
			{
				return;
			}
			
			habbo.Achievements.GetAchievementProgress(achievement).Progress += amount;

			AchievementLevel newLevel = achievement.GetLevelForProgress(progress + amount);
			if (oldLevel.Level == newLevel.Level && newLevel.Level < achievement.Levels.Count)
			{
				habbo.Session.Send(new AchievementProgressComposer(habbo, achievement));
			}
			else
			{
				habbo.Session.Send(new AchievementProgressComposer(habbo, achievement));
				habbo.Session.Send(new AchievementUnlockedComposer(habbo, achievement));

				BadgeDefinition badge = habbo.Badges.GetBadge("ACH_" + achievement.Name + oldLevel.Level);
				if (badge == null)
				{
					habbo.Badges.GiveBadge("ACH_" + achievement.Name + newLevel.Level);
				}
				else
				{
					badge.Code = "ACH_" + achievement.Name + newLevel.Level;
					habbo.Badges.UpdateBadge(badge, "ACH_" + achievement.Name + oldLevel.Level);
				}

				if (badge.Slot > 0)
				{
					if (habbo.CurrentRoom != null)
					{
						habbo.CurrentRoom.EntityManager.Send(new UserBadgesComposer(habbo.Badges.GetWearingBadges(), habbo.Id));
					}
					else
					{
						habbo.Session.Send(new UserBadgesComposer(habbo.Badges.GetWearingBadges(), habbo.Id));
					}
				}

				habbo.AchievementScore += newLevel.RewardPoints;

				if (habbo.CurrentRoom != null)
				{
					habbo.CurrentRoom.EntityManager.Send(new RoomUserDataComposer(habbo));
				}

				//todo: talent track shit
			}
		}

		public bool HasAchieved(Habbo habbo, Achievement achievement)
		{
			AchievementProgress achievementProgress = habbo.Achievements.GetAchievementProgress(achievement);
			if (achievementProgress == null)
			{
				return false;
			}

			AchievementLevel level = achievement.GetLevelForProgress(achievementProgress.Progress);
			if (level == null)
			{
				return false;
			}

			AchievementLevel nextLevel = achievement.GetNextLevel(level.Level);
			if (nextLevel == null && achievementProgress.Progress >= level.Progress)
			{
				return true;
			}

			return false;
		}
	}
}
