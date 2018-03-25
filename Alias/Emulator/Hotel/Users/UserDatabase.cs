using System.Data;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users
{
    public class UserDatabase
    {
		public static object Variable(int userId, string column)
		{
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
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
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
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

			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
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
				dbClient.AddParameter("oldChat", Alias.BoolToString(settings.OldChat));
				dbClient.AddParameter("ignoreInvited", Alias.BoolToString(settings.IgnoreInvites));
				dbClient.AddParameter("cameraFollow", Alias.BoolToString(settings.CameraFollow));
				dbClient.Query("INSERT INTO `habbo_settings` (`id`, `volume_system`, `volume_furni`, `volume_trax`, `old_chat`, `ignore_invited`, `camera_follow`) VALUES (@id, @volumeSystem, @volumeFurni, @volumeTrax, @oldChat, @ignoreInvited, @cameraFollow)");
			}
			return settings;
		}

		public static void UpdateSettings(UserSettings settings, int userId)
		{
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("id", userId);
				dbClient.AddParameter("volumeSystem", settings.VolumeSystem);
				dbClient.AddParameter("volumeFurni", settings.VolumeFurni);
				dbClient.AddParameter("volumeTrax", settings.VolumeTrax);
				dbClient.AddParameter("oldChat", Alias.BoolToString(settings.OldChat));
				dbClient.AddParameter("ignoreInvited", Alias.BoolToString(settings.IgnoreInvites));
				dbClient.AddParameter("cameraFollow", Alias.BoolToString(settings.CameraFollow));
				dbClient.Query("UPDATE `habbo_settings` SET `volume_system` = @volumeSystem, `volume_furni` = @volumeFurni, `volume_trax` = @volumeTrax, `old_chat` = @oldChat, `ignore_invited` = @ignoreInvited, `camera_follow` = @cameraFollow WHERE `id` = @id");
			}
		}
	}
}
