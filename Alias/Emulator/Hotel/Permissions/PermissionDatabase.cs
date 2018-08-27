using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Permissions
{
    class PermissionDatabase
    {
		public static Dictionary<int, RankPermission> ReadRankPermissions()
		{
			Dictionary<int, RankPermission> permissions = new Dictionary<int, RankPermission>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `permission_ranks`"))
				{
					while (Reader.Read())
					{
						RankPermission permission = new RankPermission()
						{
							Rank = Reader.GetInt32("id"),
							Permissions = ReadPermissions(Reader.GetInt32("id"))
						};
						permissions.Add(permission.Rank, permission);
					}
				}
			}
			return permissions;
		}

		public static List<string> ReadPermissions(int rankId)
		{
			List<string> permissions = new List<string>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("rankId", rankId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `permissions` WHERE `rank_id` = @rankId"))
				{
					while (Reader.Read())
					{
						permissions.Add(Reader.GetString("permission"));
					}
				}
				
			}
			return permissions;
		}
	}
}
