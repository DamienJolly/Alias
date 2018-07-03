using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Groups
{
    sealed class GroupManager
    {
		private readonly object _groupLoadingSync;
		public List<Group> Groups { get; set; }

		public List<GroupBadgeParts> BadgeParts { get; set; }

		public GroupManager()
		{
			this._groupLoadingSync = new object();
			this.Groups = new List<Group>();

			this.BadgeParts = new List<GroupBadgeParts>();
		}

		public void Initialize()
		{
			this.BadgeParts.Clear();

			GroupDatabase.ReadBadgeParts(this);
		}

		public Group CreateGroup(Habbo habbo, int roomId, string roomName, string name, string description, string badge, int colourOne, int colourTwo)
		{
			Group group = new Group(0, name, description, habbo.Id, 0, roomId, colourOne, colourTwo, badge, new List<GroupMember>());
			group.Id = GroupDatabase.CreateGroup(group);
			group.Members.Add(new GroupMember(habbo.Id, habbo.Username, habbo.Look, group.CreatedAt, 0));
			Groups.Add(group);
			return group;
		}

		public void DeleteGroup(Group group)
		{
			GroupDatabase.RemoveGroup(group);
			Groups.Remove(group);
		}

		public Group GetGroup(int id)
		{
			lock (this._groupLoadingSync)
			{
				if (this.Groups.Where(group => group.Id == id).ToList().Count > 0)
				{
					return this.Groups.Where(group => group.Id == id).First();
				}
				else
				{
					return GroupDatabase.TryGetGroup(id);
				}
			}
		}

		public List<GroupBadgeParts> GetBases => this.BadgeParts.Where(part => part.Type == BadgePartType.BASE).ToList();

		public List<GroupBadgeParts> GetSymbols => this.BadgeParts.Where(part => part.Type == BadgePartType.SYMBOL).ToList();

		public List<GroupBadgeParts> GetBaseColours => this.BadgeParts.Where(part => part.Type == BadgePartType.BASE_COLOUR).ToList();

		public List<GroupBadgeParts> GetSymbolColours => this.BadgeParts.Where(part => part.Type == BadgePartType.SYMBOL_COLOUR).ToList();

		public List<GroupBadgeParts> GetBackgroundColours => this.BadgeParts.Where(part => part.Type == BadgePartType.BACKGROUND_COLOUR).ToList();
	}
}
