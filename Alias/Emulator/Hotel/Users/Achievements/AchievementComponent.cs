using System.Collections.Generic;
using Alias.Emulator.Hotel.Achievements;
using Alias.Emulator.Hotel.Achievements.Composers;
using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Hotel.Users.Badges;
using Alias.Emulator.Hotel.Users.Composers;

namespace Alias.Emulator.Hotel.Users.Achievements
{
    sealed class AchievementComponent
    {
		private Habbo habbo;

		public Dictionary<int, int> Achievements
		{
			get; set;
		}

		public AchievementComponent(Habbo h)
		{
			this.Achievements = AchievementDatabase.ReadAchievements(h.Id);
			this.habbo = h;
		}

		public bool HasAchieved(Achievement achievement)
		{
			if (!GetAchievementProgress(achievement.Id, out int progress))
			{
				return false;
			}

			AchievementLevel level = achievement.GetLevelForProgress(progress);
			if (level == null)
			{
				return false;
			}

			AchievementLevel nextLevel = achievement.GetNextLevel(level.Level);
			if (nextLevel == null && progress >= level.Progress)
			{
				return true;
			}

			return false;
		}

		public void ProgressAchievement(string name, int amount = 1)
		{
			if (Alias.Server.AchievementManager.TryGetAchievement(name, out Achievement achievement))
			{
				if (!GetAchievementProgress(achievement.Id, out int progress))
				{
					progress = 0;
					Achievements.Add(achievement.Id, amount);
					AchievementDatabase.AddAchievement(achievement.Id, amount, this.habbo.Id);
				}

				AchievementLevel oldLevel = achievement.GetLevelForProgress(progress);
				if (oldLevel == null)
				{
					return;
				}

				if (oldLevel.Level == achievement.Levels.Count && amount == oldLevel.Progress) //Maximum achievement reached.
				{
					return;
				}

				Achievements[achievement.Id] += amount;
				AchievementDatabase.UpdateAchievement(achievement.Id, amount, this.habbo.Id);

				this.habbo.Session.Send(new AchievementProgressComposer(this.habbo, achievement));

				AchievementLevel newLevel = achievement.GetLevelForProgress(progress + amount);
				if (oldLevel.Level != newLevel.Level && newLevel.Level >= achievement.Levels.Count)
				{
					this.habbo.Session.Send(new AchievementUnlockedComposer(this.habbo, achievement));

					BadgeDefinition badge = this.habbo.Badges.GetBadge("ACH_" + achievement.Name + oldLevel.Level);
					if (badge == null)
					{
						this.habbo.Badges.GiveBadge("ACH_" + achievement.Name + newLevel.Level);
					}
					else
					{
						badge.Code = "ACH_" + achievement.Name + newLevel.Level;
						this.habbo.Badges.UpdateBadge(badge, "ACH_" + achievement.Name + oldLevel.Level);
					}

					if (badge.Slot > 0)
					{
						if (this.habbo.CurrentRoom != null)
						{
							this.habbo.CurrentRoom.EntityManager.Send(new UserBadgesComposer(this.habbo.Badges.GetWearingBadges(), this.habbo.Id));
						}
						else
						{
							this.habbo.Session.Send(new UserBadgesComposer(this.habbo.Badges.GetWearingBadges(), this.habbo.Id));
						}
					}

					this.habbo.AchievementScore += newLevel.RewardPoints;

					if (this.habbo.CurrentRoom != null)
					{
						this.habbo.CurrentRoom.EntityManager.Send(new RoomUserDataComposer(this.habbo.Entity));
					}

					//todo: talent track shit
				}
			}
		}

		public void Dispose()
		{
			this.Achievements.Clear();
			this.habbo = null;
		}

		public bool GetAchievementProgress(int id, out int progress) => this.Achievements.TryGetValue(id, out progress);
	}
}
