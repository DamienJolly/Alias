using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms.Users
{
	public class RoomUserManager
	{
		public List<RoomUser> Users
		{
			get; set;
		}

		public int UserCount
		{
			get { return this.Users.Count(); }
		}

		private int VirtualId = 0;

		private Room Room
		{
			get; set;
		}

		public RoomUserManager(Room room)
		{
			this.Users = new List<RoomUser>();
			this.Room = room;
		}

		public void OnUserJoin(Session session)
		{
			if (!this.UserExists(session))
			{
				RoomUser user = new RoomUser()
				{
					Habbo     = session.Habbo(),
					Room      = Room,
					VirtualId = NextVirtualId(),
					Position = new UserPosition()
					{
						X = Room.Model.Door.X,
						Y = Room.Model.Door.Y,
						Rotation = Room.Model.Door.Rotation,
						HeadRotation = Room.Model.Door.Rotation
					}
				};
				//todo: get Door height.
				this.Send(new RoomUsersComposer(user), user);
				this.Send(new RoomUserStatusComposer(user), user);
				this.Users.Add(user);
				this.Room.GameMap.AddUserToMap(user, new Point(user.Position.X, user.Position.Y));
				session.Send(new RoomUsersComposer(this.Users));
				session.Send(new RoomUserStatusComposer(this.Users));
			}
			else
			{
				Logging.Debug("User requested Join but is in room.");
			}
		}

		public void OnUserLeave(Session session)
		{
			if (this.UserExists(session))
			{
				RoomUser user = this.UserBySession(session);
				this.Users.Remove(user);
				this.Room.GameMap.RemoveUserFromMap(user, new Point(user.Position.X, user.Position.Y));
				this.Send(new RoomUserRemoveComposer(user.VirtualId));
				user.Habbo.CurrentRoom = null;
				user.Dispose();
			}
			else
			{
				Logging.Debug("User requested Leave but isn't in room.");
			}
		}

		public RoomUser UserBySession(Session session)
		{
			return this.Users.Where(user => user.Habbo.Id == session.Habbo().Id).First();
		}

		public RoomUser UserByVirtualid(int virtualId)
		{
			return this.Users.Where(user => user.VirtualId == virtualId).First();
		}

		public RoomUser UserByUserid(int userId)
		{
			return this.Users.Where(user => user.Habbo.Id == userId).First();
		}

		public bool UserExists(Session session)
		{
			return this.Users.Where(user => user.Habbo.Id == session.Habbo().Id).Count() > 0;
		}

		internal RoomUser UserByName(string targetname)
		{
			if (this.Users.Where(user => user.Habbo.Username == targetname).Count() > 0)
			{
				return this.Users.Where(user => user.Habbo.Username == targetname).First();
			}
			return null;
		}

		public bool UserExists(int virtualId)
		{
			return this.Users.Where(user => user.VirtualId == virtualId).Count() > 0;
		}

		public int NextVirtualId()
		{
			return this.VirtualId++;
		}

		public void Send(MessageComposer composer, List<RoomUser> except)
		{
			ServerMessage message = composer.Compose();
			this.Users.ForEach(user =>
			{
				if (user.Habbo.Session() != null && !except.Contains(user))
				{
					try
					{
						user.Habbo.Session().Send(message, false);
					}
					catch (Exception ex)
					{
						Logging.Error("Couldn't send message to user", ex, "RoomUserManager", "Send(composer, List<> except)");
					}
				}
			});
		}

		public void Send(MessageComposer composer, RoomUser except)
		{
			ServerMessage message = composer.Compose();
			this.Users.ForEach(user =>
			{
				if (user.Habbo.Session() != null && user.VirtualId != except.VirtualId)
				{
					try
					{
						user.Habbo.Session().Send(message, false);
					}
					catch (Exception ex)
					{
						Logging.Error("Couldn't send message to user", ex, "RoomUserManager", "Send(composer, RoomUser except)");
					}
				}
			});
		}

		public void Send(MessageComposer composer)
		{
			ServerMessage message = composer.Compose();
			this.Users.ForEach(user =>
			{
				if (user.Habbo.Session() != null)
				{
					try
					{
						user.Habbo.Session().Send(message, false);
					}
					catch (Exception ex)
					{
						Logging.Error("Couldn't send message to user", ex, "RoomUserManager", "Send(composer)");
					}
				}
			});
		}
	}
}
