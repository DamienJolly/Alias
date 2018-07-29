using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Groups;
using Alias.Emulator.Hotel.Rooms.Rights.Composers;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Rooms.Rights
{
	class RoomRights
	{
		private Room Room;

		public List<UserRight> UserRights
		{
			get; set;
		}

		public RoomRights(Room room)
		{
			this.UserRights = RoomRightsDatabase.ReadRights(room.Id);
			this.Room = room;
		}

		public void GiveRights(int userId)
		{
			if (this.HasRights(userId))
			{
				return;
			}

			RoomRightsDatabase.GiveRights(this.Room.Id, userId);
			UserRight right = new UserRight()
			{
				Id       = userId,
				Username = (string)UserDatabase.Variable(userId, "username")
			};
			this.UserRights.Add(right);
		}

		public void TakeRights(int userId)
		{
			if (!this.HasRights(userId))
			{
				return;
			}

			RoomRightsDatabase.TakeRights(this.Room.Id, userId);
			this.UserRights.Remove(this.UserRights.Where(right => right.Id == userId).First());
		}

		public void RefreshRights(Habbo habbo)
		{
			RoomRightLevels flatCtrl = RoomRightLevels.NONE;

			if (this.Room.RoomData.OwnerId == habbo.Id)
			{
				habbo.Session.Send(new RoomOwnerComposer());
				flatCtrl = RoomRightLevels.MODERATOR;
			}
			else if (this.Room.RoomData.Group != null)
			{
				GroupMember member = this.Room.RoomData.Group.GetMember(habbo.Id);
				if (member != null)
				{
					if (member.Rank == GroupRank.ADMIN || member.Rank == GroupRank.MOD)
					{
						flatCtrl = RoomRightLevels.GROUP_ADMIN;
					}
					else if (member.Rank == GroupRank.MEMBER && this.Room.RoomData.Group.Rights)
					{
						flatCtrl = RoomRightLevels.GROUP_RIGHTS;
					}
				}
				else if (this.HasRights(habbo.Id))
				{
					flatCtrl = RoomRightLevels.RIGHTS;
				}
			}
			else if (this.HasRights(habbo.Id))
			{
				flatCtrl = RoomRightLevels.RIGHTS;
			}

			habbo.Session.Send(new RoomRightsComposer((int)flatCtrl));
			Room.UserManager.UserBySession(habbo.Session).Actions.Add("flatctrl", (int)flatCtrl + "");
		}

		public bool HasRights(int userId)
		{
			return userId == this.Room.RoomData.OwnerId || this.UserRights.Where(right => right.Id == userId).Count() > 0;
		}
	}
}
