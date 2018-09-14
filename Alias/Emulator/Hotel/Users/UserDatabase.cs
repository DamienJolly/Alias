using System.Collections.Generic;
using Alias.Emulator.Database;
using Alias.Emulator.Utilities;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users
{
    class UserDatabase
    {
		public static bool ReadUserIdBySSO(string sso, out int userId)
		{
			userId = 0;
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("sso", sso);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `id` FROM `habbos` WHERE `username` = @sso"))
				{
					if (Reader.Read())
					{
						userId = Reader.GetInt32("id");
						return true;
					}
				}
			}
			return false;
		}

		public static bool IsBanned(int userId)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				dbClient.AddParameter("now", UnixTimestamp.Now);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT null FROM `bans` WHERE `user_id` = @id AND `expires` > @now"))
				{
					return Reader.Read();
				}
			}
		}

		public static bool ReadHabboData(int userId, out Habbo habbo)
		{
			habbo = null;
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `habbos` WHERE `id` = @id"))
				{
					if (Reader.Read())
					{
						habbo = new Habbo()
						{
							Id = Reader.GetInt32("id"),
							Username = Reader.GetString("username"),
							Mail = Reader.GetString("mail"),
							Look = Reader.GetString("look"),
							Motto = Reader.GetString("motto"),
							Gender = Reader.GetString("gender"),
							Rank = Reader.GetInt32("rank"),
							ClubLevel = Reader.GetInt32("club_level"),
							Credits = Reader.GetInt32("credits"),
							HomeRoom = Reader.GetInt32("home_room"),
							AchievementScore = Reader.GetInt32("achievement_score"),
							ClubExpireTimestamp = Reader.GetInt32("club_expire_time"),
							Muted = false,
							AllowTrading = true,
							GroupId = Reader.GetInt32("group_id"),
							Groups = ReadGroups(Reader.GetInt32("id"))
						};
						return true;
					}
				}
			}
			return false;
		}

		public static List<int> ReadGroups(int userId)
		{
			List<int> groups = new List<int>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `group_id` FROM `group_members` WHERE `user_id` = @id AND `rank` <= 2"))
				{
					while (Reader.Read())
					{
						groups.Add(Reader.GetInt32("group_id"));
					}
				}
			}
			return groups;
		}

		public static object Variable(int userId, string column)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `" + column + "` FROM `habbos` WHERE `id` = @id"))
				{
					while (Reader.Read())
					{
						return Reader[column];
					}
					return null;
				}
			}
		}

		public static int Id(string username)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("username", username);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `id` FROM `habbos` WHERE `Username` = @username"))
				{
					while (Reader.Read())
					{
						return Reader.GetInt32("id");
					}
					return 0;
				}
			}
		}

		public static UserSettings Settings(int userId)
		{
			UserSettings settings = new UserSettings();

			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `habbo_settings` WHERE `id` = @id LIMIT 1"))
				{
					while (Reader.Read())
					{
						settings.VolumeSystem = Reader.GetInt32("volume_system");
						settings.VolumeFurni = Reader.GetInt32("volume_furni");
						settings.VolumeTrax = Reader.GetInt32("volume_trax");
						settings.OldChat = Reader.GetBoolean("old_chat");
						settings.IgnoreInvites = Reader.GetBoolean("ignore_invited");
						settings.CameraFollow = Reader.GetBoolean("camera_follow");
						return settings;
					}
				}

				dbClient.AddParameter("id", userId);
				dbClient.AddParameter("volumeSystem", settings.VolumeSystem);
				dbClient.AddParameter("volumeFurni", settings.VolumeFurni);
				dbClient.AddParameter("volumeTrax", settings.VolumeTrax);
				dbClient.AddParameter("oldChat", DatabaseBoolean.GetStringFromBool(settings.OldChat));
				dbClient.AddParameter("ignoreInvited", DatabaseBoolean.GetStringFromBool(settings.IgnoreInvites));
				dbClient.AddParameter("cameraFollow", DatabaseBoolean.GetStringFromBool(settings.CameraFollow));
				dbClient.Query("INSERT INTO `habbo_settings` (`id`, `volume_system`, `volume_furni`, `volume_trax`, `old_chat`, `ignore_invited`, `camera_follow`) VALUES (@id, @volumeSystem, @volumeFurni, @volumeTrax, @oldChat, @ignoreInvited, @cameraFollow)");
			}
			return settings;
		}

		public static void UpdateSettings(UserSettings settings, int userId)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				dbClient.AddParameter("volumeSystem", settings.VolumeSystem);
				dbClient.AddParameter("volumeFurni", settings.VolumeFurni);
				dbClient.AddParameter("volumeTrax", settings.VolumeTrax);
				dbClient.AddParameter("oldChat", DatabaseBoolean.GetStringFromBool(settings.OldChat));
				dbClient.AddParameter("ignoreInvited", DatabaseBoolean.GetStringFromBool(settings.IgnoreInvites));
				dbClient.AddParameter("cameraFollow", DatabaseBoolean.GetStringFromBool(settings.CameraFollow));
				dbClient.Query("UPDATE `habbo_settings` SET `volume_system` = @volumeSystem, `volume_furni` = @volumeFurni, `volume_trax` = @volumeTrax, `old_chat` = @oldChat, `ignore_invited` = @ignoreInvited, `camera_follow` = @cameraFollow WHERE `id` = @id");
			}
		}
	}
}
