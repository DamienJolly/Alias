using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Hotel.Rooms.Models;

namespace Alias.Emulator.Hotel.Rooms.Cycle.Tasks
{
	public class WalkTask
	{
		public static void Start(List<RoomUser> users)
		{
			users.ForEach(usr =>
			{
				try
				{
					if (usr.TargetPosition != null && usr.Path != null && usr.Path.Count > 0)
					{
						if (usr.Actions.Has("mv"))
						{
							usr.Actions.Remove("mv");
						}
						if (usr.Actions.Has("sit"))
						{
							usr.Actions.Remove("sit");
						}
						if (usr.Actions.Has("lay"))
						{
							usr.Actions.Remove("lay");
						}

						Point p = usr.Path.First();
						double height = 0.0;

						usr.Room.GameMap.UpdateUserMovement(usr, new Point(usr.Position.X, usr.Position.Y), p);

						usr.Actions.Add("mv", p.X + "," + p.Y + "," + height);
						usr.Position.Rotation = usr.Room.PathFinder.Rotation(usr.Position.X, usr.Position.Y, p.X, p.Y);
						usr.Position.HeadRotation = usr.Position.Rotation;
						usr.Path.RemoveFirst();
						usr.Room.UserManager.Send(new RoomUserStatusComposer(usr));
						usr.Position.X = p.X;
						usr.Position.Y = p.Y;
						usr.Position.Z = height;

						if (usr.Path.Count() != 0)
						{
							usr.Path = usr.Room.PathFinder.Path(usr.Position, usr.TargetPosition);
						}
					}
					else if (usr.Path != null && usr.Path.Count == 0)
					{
						if (usr.TargetPosition.X == usr.Position.X && usr.TargetPosition.Y == usr.Position.Y)
						{
							if (usr.Actions.Has("mv"))
							{
								usr.Actions.Remove("mv");
								usr.Room.UserManager.Send(new RoomUserStatusComposer(usr));
							}

							if (usr.Room.Model.Door.X == usr.Position.X && usr.Room.Model.Door.Y == usr.Position.Y)
							{
								usr.Room.UserManager.OnUserLeave(usr.Habbo.Session());
								usr.Habbo.Session().Send(new HotelViewComposer());
							}
						}
					}
				}
				catch { }
			});
		}
	}
}
