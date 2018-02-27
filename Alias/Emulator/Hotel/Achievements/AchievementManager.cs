using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Hotel.Users.Achievements;
using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Hotel.Achievements.Composers;
using Alias.Emulator.Hotel.Users.Badges;
using Alias.Emulator.Hotel.Users.Composers;

namespace Alias.Emulator.Hotel.Achievements
{
    public class AchievementManager
    {
		private static List<Achievement> achievements;

		public static void Initialize()
		{
			achievements = AchievementDatabase.ReadAchievements();
		}

		public static void Reload()
		{
			achievements.Clear();
			Initialize();
		}

		public static List<Achievement> GetAchievements()
		{
			return achievements.Where(ach => ach.Category != AchievementCategory.INVISIBLE).ToList();
		}

		public static Achievement GetAchievement(string name)
		{
			return achievements.Where(ach => ach.Name == name).FirstOrDefault();
		}

		public static void ProgressAchievement(int habboId, Achievement achievement, int amount = 1)
		{
			if (achievement != null)
			{
				Habbo habbo = SessionManager.SessionById(habboId).Habbo();
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

		public static void ProgressAchievement(Habbo habbo, Achievement achievement, int amount = 1)
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
			AchievementProgress currentProgress = habbo.Achievements().GetAchievementProgress(achievement);
			if (currentProgress == null)
			{
				AchievementDatabase.AddUserAchievement(habbo, achievement, amount);
				AchievementProgress newAchievement = new AchievementProgress()
				{
					Achievement = achievement,
					Progress = amount
				};
				habbo.Achievements().RequestAchievementProgress().Add(newAchievement);
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
			
			habbo.Achievements().GetAchievementProgress(achievement).Progress += amount;

			AchievementLevel newLevel = achievement.GetLevelForProgress(progress + amount);
			if (oldLevel.Level == newLevel.Level && newLevel.Level < achievement.Levels.Count)
			{
				habbo.Session().Send(new AchievementProgressComposer(habbo, achievement));
			}
			else
			{
				habbo.Session().Send(new AchievementProgressComposer(habbo, achievement));
				habbo.Session().Send(new AchievementUnlockedComposer(habbo, achievement));

				BadgeDefinition badge = habbo.GetBadgeComponent().GetBadge("ACH_" + achievement.Name + oldLevel.Level);
				if (badge == null)
				{
					habbo.GetBadgeComponent().GiveBadge("ACH_" + achievement.Name + newLevel.Level);
				}
				else
				{
					badge.Code = "ACH_" + achievement.Name + newLevel.Level;
					habbo.GetBadgeComponent().UpdateBadge(badge, "ACH_" + achievement.Name + oldLevel.Level);
				}

				if (badge.Slot > 0)
				{
					if (habbo.CurrentRoom != null)
					{
						habbo.CurrentRoom.UserManager.Send(new UserBadgesComposer(habbo.GetBadgeComponent().GetWearingBadges(), habbo.Id));
					}
					else
					{
						habbo.Session().Send(new UserBadgesComposer(habbo.GetBadgeComponent().GetWearingBadges(), habbo.Id));
					}
				}

				habbo.AchievementScore += newLevel.RewardPoints;

				if (habbo.CurrentRoom != null)
				{
					habbo.CurrentRoom.UserManager.Send(new RoomUserDataComposer(habbo));
				}

				//todo: talent track shit
			}
		}

		public static bool HasAchieved(Habbo habbo, Achievement achievement)
		{
			AchievementProgress achievementProgress = habbo.Achievements().GetAchievementProgress(achievement);
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
