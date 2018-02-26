using System.Data;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Users
{
    public class UserDatabase
    {
		public static object Variable(int userId, string column)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				return dbClient.DataRow("SELECT `" + column + "` FROM `habbos` WHERE `id` = @id")[column];
			}
		}

		public static int Id(string username)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("username", username);
				return dbClient.Int32("SELECT `id` FROM `habbos` WHERE `Username` = @username");
			}
		}

		public static UserSettings Settings(int userId)
		{
			UserSettings settings = new UserSettings();

			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				DataRow row = dbClient.DataRow("SELECT * FROM `habbo_settings` WHERE `id` = @id LIMIT 1");
				if (row == null)
				{
					dbClient.AddParameter("volumeSystem", settings.VolumeSystem);
					dbClient.AddParameter("volumeFurni", settings.VolumeFurni);
					dbClient.AddParameter("volumeTrax", settings.VolumeTrax);
					dbClient.AddParameter("oldChat", AliasEnvironment.BoolToString(settings.OldChat));
					dbClient.AddParameter("ignoreInvited", AliasEnvironment.BoolToString(settings.IgnoreInvites));
					dbClient.AddParameter("cameraFollow", AliasEnvironment.BoolToString(settings.CameraFollow));
					dbClient.Query("INSERT INTO `habbo_settings` (`id`, `volume_system`, `volume_furni`, `volume_trax`, `old_chat`, `ignore_invited`, `camera_follow`) VALUES (@id, @volumeSystem, @volumeFurni, @volumeTrax, @oldChat, @ignoreInvited, @cameraFollow)");
				}
				else
				{
					settings.VolumeSystem = (int)row["volume_system"];
					settings.VolumeFurni = (int)row["volume_furni"];
					settings.VolumeTrax = (int)row["volume_trax"];
					settings.OldChat = AliasEnvironment.ToBool((string)row["old_chat"]);
					settings.IgnoreInvites = AliasEnvironment.ToBool((string)row["ignore_invited"]);
					settings.CameraFollow = AliasEnvironment.ToBool((string)row["camera_follow"]);
				}
			}

			return settings;
		}

		public static void UpdateSettings(UserSettings settings, int userId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				dbClient.AddParameter("volumeSystem", settings.VolumeSystem);
				dbClient.AddParameter("volumeFurni", settings.VolumeFurni);
				dbClient.AddParameter("volumeTrax", settings.VolumeTrax);
				dbClient.AddParameter("oldChat", AliasEnvironment.BoolToString(settings.OldChat));
				dbClient.AddParameter("ignoreInvited", AliasEnvironment.BoolToString(settings.IgnoreInvites));
				dbClient.AddParameter("cameraFollow", AliasEnvironment.BoolToString(settings.CameraFollow));
				dbClient.Query("UPDATE `habbo_settings` SET `volume_system` = @volumeSystem, `volume_furni` = @volumeFurni, `volume_trax` = @volumeTrax, `old_chat` = @oldChat, `ignore_invited` = @ignoreInvited, `camera_follow` = @cameraFollow WHERE `id` = @id");
			}
		}
	}
}
