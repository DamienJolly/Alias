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
						usr.Actions.Clear();
						Point p = usr.Path.First();
						double height = 0.0;

						usr.Actions.Add("mv", p.X + "," + p.Y + "," + height);
						usr.Position.Rotation = usr.Room.PathFinder.Rotation(usr.Position.X, usr.Position.Y, p.X, p.Y);
						usr.Position.HeadRotation = usr.Position.Rotation;
						usr.Path.RemoveFirst();
						usr.Room.UserManager.Send(new RoomUserStatusComposer(usr));
						usr.Position.X = p.X;
						usr.Position.Y = p.Y;
						usr.Position.Z = height;
					}
					else if (usr.Path != null && usr.Path.Count == 0)
					{
						if (usr.TargetPosition.X == usr.Position.X && usr.TargetPosition.Y == usr.Position.Y)
						{
							bool update = false;
							if (usr.Actions.Has("mv"))
							{
								usr.Actions.Remove("mv");
								update = true;
							}

							if (update)
							{
								usr.Room.UserManager.Send(new RoomUserStatusComposer(usr));
							}

							if (usr.Room.DynamicModel.Tiles[usr.Position.X, usr.Position.Y].State == TileState.DOOR)
							{
								usr.Habbo.Session().Send(new HotelViewComposer());
								usr.Room.UserManager.OnUserLeave(usr.Habbo.Session());
							}
						}
					}
				}
				catch { }
			});
		}
	}
}
