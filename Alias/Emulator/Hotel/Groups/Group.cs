using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Groups
{
    class Group
    {
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public string Description
		{
			get; set;
		}

		public int OwnerId
		{
			get; set;
		}

		public int CreatedAt
		{
			get; set;
		}

		public int RoomId
		{
			get; set;
		}

		public int ColourOne
		{
			get; set;
		}

		public int ColourTwo
		{
			get; set;
		}

		public string Badge
		{
			get; set;
		}

		public List<GroupMember> Members
		{
			get; set;
		}
		
		public Group(int id, string name, string description, int ownerId, int createdAt, int roomId, int colourOne, int colourTwo, string badge, List<GroupMember> members)
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
			this.OwnerId = ownerId;
			this.CreatedAt = createdAt;
			this.RoomId = roomId;
			this.ColourOne = colourOne;
			this.ColourTwo = colourTwo;
			this.Badge = badge;
			this.Members = members;
		}

		public GroupMember GetMember(int userId)
		{
			return this.Members.Where(member => member.UserId == userId).FirstOrDefault();
		}

		public void RemoveMember(int userId)
		{
			GroupMember member = GetMember(userId);
			if (member == null)
			{
				return;
			}

			GroupDatabase.RemoveMember(this.Id, userId);
			this.Members.Remove(member);
		}

		public void SetMemberRank(int userId, int rank)
		{
			GroupMember member = GetMember(userId);
			if (member == null || (int)member.Rank == rank)
			{
				return;
			}

			GroupDatabase.SetMemberRank(this.Id, userId, rank);
			member.Rank = (GroupRank)rank;
		}

		public List<GroupMember> SearchMembers(int page, int levelId, string query)
		{
			List<GroupMember> members = new List<GroupMember>();
			switch (levelId)
			{
				case 2: members = this.Members.Where(member => (int)member.Rank == 3 && member.Username.Contains(query)).ToList(); break;
				case 1: members = this.Members.Where(member => (int)member.Rank <= 1 && member.Username.Contains(query)).ToList(); break;
				default: members = this.Members.Where(member => (int)member.Rank <= 2 && member.Username.Contains(query)).ToList(); break;
			}

			while (page * 14 > members.Count)
			{
				page--;
			}

			return members.GetRange(page * 14, (page * 14) + 14 > members.Count ? members.Count - page * 14 : (page * 14) + 14);
		}

		public int GetMembers
		{
			get
			{
				return this.Members.Where(Members => (int)Members.Rank <= 2).Count();
			}
		}

		public int GetRequests
		{
			get
			{
				return this.Members.Where(Members => (int)Members.Rank == 3).Count();
			}
		}
	}
}
