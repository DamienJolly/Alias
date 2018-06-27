using System.Collections.Concurrent;

namespace Alias.Emulator.Hotel.Groups
{
    sealed class GroupManager
    {
		private readonly object _groupLoadingSync;
		private ConcurrentDictionary<int, Group> _groups;

		public GroupManager()
		{
			this._groupLoadingSync = new object();
			this._groups = new ConcurrentDictionary<int, Group>();
		}

		public bool TryGetGroup(int id, out Group group)
		{
			lock (this._groupLoadingSync)
			{
				if (this._groups.ContainsKey(id))
				{
					return this._groups.TryGetValue(id, out group);
				}

				group = GroupDatabase.TryGetGroup(id);
				if (this._groups.TryAdd(id, group))
				{
					return true;
				}
			}
			return false;
		}
	}
}
