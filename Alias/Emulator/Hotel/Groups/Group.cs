using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Network.Sessions;

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

		public GroupState State
		{
			get; set;
		}

		public bool Rights
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
		
		public Group(int id, string name, string description, int ownerId, int createdAt, int roomId, int state, bool rights, int colourOne, int colourTwo, string badge, List<GroupMember> members)
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
			this.OwnerId = ownerId;
			this.CreatedAt = createdAt;
			this.RoomId = roomId;
			this.State = (GroupState)state;
			this.Rights = rights;
			this.ColourOne = colourOne;
			this.ColourTwo = colourTwo;
			this.Badge = badge;
			this.Members = members;
		}

		// temp
		public void Save()
		{
			GroupDatabase.UpdateGroup(this);
		}

		public GroupMember GetMember(int userId)
		{
			return this.Members.Where(member => member.UserId == userId).FirstOrDefault();
		}

		public void JoinGroup(Session session, int userId, bool acceptRequest)
		{
			int groupsCount = GroupDatabase.GetGroupsCount(userId);
			if (groupsCount >= 100)
			{
				if (acceptRequest)
				{
					session.Send(new GroupJoinErrorComposer(GroupJoinErrorComposer.MEMBER_FAIL_JOIN_LIMIT_EXCEED_NON_HC));
					return;
				}
				else
				{
					session.Send(new GroupJoinErrorComposer(GroupJoinErrorComposer.GROUP_LIMIT_EXCEED));
					return;
				}
			}

			if (this.GetMembers >= 50000)
			{
				session.Send(new GroupJoinErrorComposer(GroupJoinErrorComposer.GROUP_FULL));
				return;
			}

			if (!acceptRequest && this.GetRequests >= 100)
			{
				session.Send(new GroupJoinErrorComposer(GroupJoinErrorComposer.GROUP_NOT_ACCEPT_REQUESTS));
				return;
			}

			if (acceptRequest)
			{
				this.SetMemberRank(userId, (int)GroupRank.MEMBER);
			}
			else
			{
				GroupMember member = new GroupMember(userId, session.Habbo.Username, session.Habbo.Look, (int)Alias.GetUnixTimestamp(), this.State == GroupState.LOCKED ? (int)GroupRank.REQUESTED : (int)GroupRank.MEMBER);
				GroupDatabase.AddMemmber(this.Id, userId, (int)member.Rank);
				this.Members.Add(member);
			}
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

		public List<GroupMember> SearchMembers(int levelId, string query)
		{
			List<GroupMember> members = new List<GroupMember>();
			switch (levelId)
			{
				case 2: members = this.Members.Where(member => (int)member.Rank == 3 && member.Username.Contains(query)).ToList(); break;
				case 1: members = this.Members.Where(member => (int)member.Rank <= 1 && member.Username.Contains(query)).ToList(); break;
				default: members = this.Members.Where(member => (int)member.Rank <= 2 && member.Username.Contains(query)).ToList(); break;
			}
			return members;
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
