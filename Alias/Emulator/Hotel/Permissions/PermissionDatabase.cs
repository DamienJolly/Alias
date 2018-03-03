using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Permissions
{
    public class PermissionDatabase
    {
		public static List<Permission> ReadPermissions()
		{
			List<Permission> permissions = new List<Permission>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (DataRow row in dbClient.DataTable("SELECT `permission`, `ranks` FROM `permissions`").Rows)
				{
					string ranks = (string)row["ranks"];
					Permission permission = new Permission()
					{
						Param = (string)row["permission"],
						Ranks = ranks.Split(';').Select(Int32.Parse).ToList()
					};
					permissions.Add(permission);
					row.Delete();
				}
			}
			return permissions;
		}

		public static List<Permission> ReadCommandPermissions()
		{
			List<Permission> permissions = new List<Permission>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (DataRow row in dbClient.DataTable("SELECT `command`, `ranks` FROM `permissions_commands`").Rows)
				{
					string ranks = (string)row["ranks"];
					Permission permission = new Permission()
					{
						Param = (string)row["command"],
						Ranks = ranks.Split(';').Select(Int32.Parse).ToList()
					};
					permissions.Add(permission);
					row.Delete();
				}
			}
			return permissions;
		}
	}
}
