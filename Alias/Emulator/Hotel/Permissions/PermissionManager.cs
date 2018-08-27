using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Permissions
{
    sealed class PermissionManager
    {
		private Dictionary<int, RankPermission> _rank;

		public PermissionManager()
		{
			this._rank = new Dictionary<int, RankPermission>();
		}

		public void Initialize()
		{
			if (this._rank.Count > 0)
			{
				this._rank.Clear();
			}

			this._rank = PermissionDatabase.ReadRankPermissions();
		}

		public bool HasPermission(int rank, string param)
		{
			if (this._rank.TryGetValue(rank, out RankPermission permission))
			{
				return permission.HasPermission(param);
			}
			return false;
		}
	}
}
