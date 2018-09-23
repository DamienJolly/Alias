using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Players;

namespace Alias.Emulator.Hotel.Groups
{
    sealed class GroupManager
    {
		public List<Group> Groups { get; set; }

		public List<GroupBadgeParts> BadgeParts { get; set; }

		public GroupManager()
		{
			this.Groups = new List<Group>();

			this.BadgeParts = new List<GroupBadgeParts>();
		}

		public void Initialize()
		{
			this.BadgeParts.Clear();

			GroupDatabase.ReadBadgeParts(this);
		}

		public void DoGroupCycle()
		{
			this.Groups.Where(group => !group.Disposing).ToList().ForEach(group => group.Cycle());
		}

		public Group CreateGroup(Player habbo, int roomId, string roomName, string name, string description, string badge, int colourOne, int colourTwo)
		{
			Group group = new Group(0, name, description, habbo.Id, 0, roomId, 0, true, colourOne, colourTwo, badge, new List<GroupMember>());
			group.Id = GroupDatabase.CreateGroup(group);
			group.Members.Add(new GroupMember(habbo.Id, habbo.Username, habbo.Look, group.CreatedAt, 0));
			Groups.Add(group);
			return group;
		}

		public void RemoveGroup(Group group)
		{
			if (Groups.Contains(group))
			{
				Groups.Remove(group);
			}
		}

		public void DeleteGroup(Group group)
		{
			GroupDatabase.RemoveGroup(group);
			RemoveGroup(group);
		}

		public Group GetGroup(int id)
		{
			Group group = null;
			if (this.Groups.Where(g => g.Id == id && !g.Disposing).ToList().Count > 0)
			{
				group = this.Groups.Where(g => g.Id == id).First();
				group.IdleTime = 0;
			}
			else
			{
				group = GroupDatabase.TryGetGroup(id);
				if (group != null)
				{
					this.Groups.Add(group);
				}
			}
			return group;
		}

		public List<GroupBadgeParts> GetBases => this.BadgeParts.Where(part => part.Type == BadgePartType.BASE).ToList();

		public List<GroupBadgeParts> GetSymbols => this.BadgeParts.Where(part => part.Type == BadgePartType.SYMBOL).ToList();

		public List<GroupBadgeParts> GetBaseColours => this.BadgeParts.Where(part => part.Type == BadgePartType.BASE_COLOUR).ToList();

		public List<GroupBadgeParts> GetSymbolColours => this.BadgeParts.Where(part => part.Type == BadgePartType.SYMBOL_COLOUR).ToList();

		public List<GroupBadgeParts> GetBackgroundColours => this.BadgeParts.Where(part => part.Type == BadgePartType.BACKGROUND_COLOUR).ToList();
	}
}
