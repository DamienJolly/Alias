using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Hotel.Achievements;
using Alias.Emulator.Hotel.Achievements.Composers;
using Alias.Emulator.Hotel.Players.Badges;
using Alias.Emulator.Hotel.Players.Composers;
using Alias.Emulator.Hotel.Rooms.Entities.Composers;

namespace Alias.Emulator.Hotel.Players.Achievements
{
    internal class AchievementComponent
    {
		private readonly AchievementDao _dao;
		private readonly Player _player;
		private Dictionary<int, int> _achievements;

		internal AchievementComponent(AchievementDao dao, Player player)
		{
			_dao = dao;
			_player = player;
			_achievements = new Dictionary<int, int>();
		}

		public async Task Initialize()
		{
			if (_achievements.Count > 0)
			{
				_achievements.Clear();
			}

			_achievements = await _dao.ReadPlayerAchievementsByIdAsync(_player.Id);
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

		public async void ProgressAchievement(string name, int amount = 1)
		{
			if (Alias.Server.AchievementManager.TryGetAchievement(name, out Achievement achievement))
			{
				if (!GetAchievementProgress(achievement.Id, out int progress))
				{
					progress = 0;
					_achievements.Add(achievement.Id, amount);
					await _dao.AddPlayerAchievementAsync(achievement.Id, amount, _player.Id);
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

				_achievements[achievement.Id] += amount;
				await _dao.UpdatePlayerAchievementAsync(achievement.Id, amount, _player.Id);
				_player.Session.Send(new AchievementProgressComposer(_player, achievement));

				AchievementLevel newLevel = achievement.GetLevelForProgress(progress + amount);
				if (oldLevel.Level != newLevel.Level && newLevel.Level >= achievement.Levels.Count)
				{
					_player.Session.Send(new AchievementUnlockedComposer(_player, achievement));
					
					if (!_player.Badges.TryGetBadge("ACH_" + achievement.Name + oldLevel.Level, out BadgeDefinition badge))
					{
						await _player.Badges.AddBadgeAsync("ACH_" + achievement.Name + newLevel.Level);
					}
					else
					{
						badge.Code = "ACH_" + achievement.Name + newLevel.Level;
						await _player.Badges.UpdateBadgeAsync(badge, "ACH_" + achievement.Name + oldLevel.Level);
					}

					if (badge.Slot > 0)
					{
						if (_player.CurrentRoom != null)
						{
							_player.CurrentRoom.EntityManager.Send(new UserBadgesComposer(_player.Badges.GetWearingBadges, _player.Id));
						}
						else
						{
							_player.Session.Send(new UserBadgesComposer(_player.Badges.GetWearingBadges, _player.Id));
						}
					}

					_player.AchievementScore += newLevel.RewardPoints;

					if (_player.CurrentRoom != null)
					{
						_player.CurrentRoom.EntityManager.Send(new RoomUserDataComposer(_player.Entity));
					}

					//todo: talent track shit
				}
			}
		}

		public void Dispose()
		{
			_achievements.Clear();
		}

		public bool GetAchievementProgress(int id, out int progress) => _achievements.TryGetValue(id, out progress);
	}
}
