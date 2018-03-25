using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Permissions
{
    sealed class PermissionManager
    {
		private List<Permission> _permissions;
		private List<Permission> _commandPermissions;

		public PermissionManager()
		{
			this._permissions = new List<Permission>();
			this._commandPermissions = new List<Permission>();
		}

		public void Initialize()
		{
			if (this._permissions.Count > 0)
			{
				this._permissions.Clear();
			}

			if (this._commandPermissions.Count > 0)
			{
				this._commandPermissions.Clear();
			}

			this._permissions = PermissionDatabase.ReadPermissions();
			this._commandPermissions = PermissionDatabase.ReadCommandPermissions();
		}

		public bool HasPermission(int rank, string param)
		{
			return this._permissions.Count(perm => perm.Param == param && perm.Ranks.Contains(rank)) > 0;
		}

		public bool HasCommandPermission(int rank, string param)
		{
			return this._commandPermissions.Count(perm => perm.Param == param && perm.Ranks.Contains(rank)) > 0;
		}
	}
}
