using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Permissions
{
	class RankPermission
	{
		public int Rank
		{
			get; set;
		}

		public List<string> Permissions
		{
			get; set;
		}

		public bool HasPermission(string param) => this.Permissions.Contains(param);
	}
}
