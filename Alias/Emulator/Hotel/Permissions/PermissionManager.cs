using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Permissions
{
    public class PermissionManager
    {
		private static List<Permission> permissions;
		private static List<Permission> commandPermissions;

		public static void Initialize()
		{
			permissions = PermissionDatabase.ReadPermissions();
			commandPermissions = PermissionDatabase.ReadCommandPermissions();
		}

		public static void Reload()
		{
			permissions.Clear();
			commandPermissions.Clear();
			Initialize();
		}

		public static bool HasPermission(int rank, string param)
		{
			return permissions.Count(perm => perm.Param == param && perm.Ranks.Contains(rank)) > 0;
		}

		public static bool HasCommandPermission(int rank, string param)
		{
			return commandPermissions.Count(perm => perm.Param == param && perm.Ranks.Contains(rank)) > 0;
		}
	}
}
