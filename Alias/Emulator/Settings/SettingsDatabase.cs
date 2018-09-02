using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Settings
{
	class SettingsDatabase
	{
		public static Dictionary<string, string> ReadSettings()
		{
			Dictionary<string, string> settings = new Dictionary<string, string>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `server_settings`"))
				{
					while (Reader.Read())
					{
						if (!settings.ContainsKey(Reader.GetString("key")))
						{
							settings.Add(Reader.GetString("key"), Reader.GetString("value"));
						}
					}
				}
			}
			return settings;
		}
	}
}
