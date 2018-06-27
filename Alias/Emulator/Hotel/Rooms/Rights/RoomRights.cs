using System.Collections.Generic;
using System.Linq;
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
			if (this.Room.RoomData.OwnerId == habbo.Id)
			{
				habbo.Session.Send(new RoomOwnerComposer());
				habbo.Session.Send(new RoomRightsComposer(5));
				Room.UserManager.UserBySession(habbo.Session).Actions.Add("flatctrl", 5 + "");
			}
			else if (this.HasRights(habbo.Id))
			{
				habbo.Session.Send(new RoomRightsComposer(1));
				Room.UserManager.UserBySession(habbo.Session).Actions.Add("flatctrl", 1 + "");
			}
			else
			{
				habbo.Session.Send(new RoomRightsComposer(0));
				Room.UserManager.UserBySession(habbo.Session).Actions.Add("flatctrl", 0 + "");
			}
		}

		public bool HasRights(int userId)
		{
			return userId == this.Room.RoomData.OwnerId || this.UserRights.Where(right => right.Id == userId).Count() > 0;
		}
	}
}
