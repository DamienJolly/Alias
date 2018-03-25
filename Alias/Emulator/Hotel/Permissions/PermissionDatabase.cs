using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Permissions
{
    public class PermissionDatabase
    {
		public static List<Permission> ReadPermissions()
		{
			List<Permission> permissions = new List<Permission>();
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `permission`, `ranks` FROM `permissions`"))
				{
					while (Reader.Read())
					{
						string ranks = Reader.GetString("ranks");
						Permission permission = new Permission()
						{
							Param = Reader.GetString("permission"),
							Ranks = ranks.Split(';').Select(Int32.Parse).ToList()
						};
						permissions.Add(permission);
					}
				}
			}
			return permissions;
		}

		public static List<Permission> ReadCommandPermissions()
		{
			List<Permission> permissions = new List<Permission>();
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `command`, `ranks` FROM `permissions_commands`"))
				{
					while (Reader.Read())
					{
						string ranks = Reader.GetString("ranks");
						Permission permission = new Permission()
						{
							Param = Reader.GetString("command"),
							Ranks = ranks.Split(';').Select(Int32.Parse).ToList()
						};
						permissions.Add(permission);
					}
				}
				
			}
			return permissions;
		}
	}
}
