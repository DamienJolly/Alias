using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Hotel.Rooms.Mapping;
using Alias.Emulator.Hotel.Rooms.Users.Composers;

namespace Alias.Emulator.Hotel.Rooms.Users.Tasks
{
	class WalkTask
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

						RoomTile oldTile = usr.Room.Mapping.Tiles[usr.Position.X, usr.Position.Y];
						oldTile.RemoveEntity(usr);

						Point p = usr.Path.First();
						RoomTile tile = usr.Room.Mapping.Tiles[p.X, p.Y];
						tile.AddEntity(usr);

						double height = 0.0;
						
						if (oldTile.TopItem != null)
						{
							// walk off
							if (tile.TopItem == null || tile.TopItem != oldTile.TopItem)
							{
								oldTile.TopItem.GetInteractor().OnUserWalkOff(usr.Habbo.Session, usr.Room, oldTile.TopItem);
							}
						}
						
						if (tile.TopItem != null)
						{
							// walk on
							if (oldTile.TopItem == null || oldTile.TopItem != tile.TopItem)
							{
								tile.TopItem.GetInteractor().OnUserWalkOn(usr.Habbo.Session, usr.Room, tile.TopItem);
							}

							height += tile.TopItem.Position.Z;
							if (!tile.TopItem.ItemData.CanSit && !tile.TopItem.ItemData.CanLay)
							{
								// todo: multiheight furni
								height += tile.TopItem.ItemData.Height;
							}
						}
						else
						{
							height += tile.Position.Z;
						}

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
							usr.Path = usr.Room.PathFinder.Path(usr);
						}
					}
					else
					{
						if (usr.TargetPosition.X == usr.Position.X && usr.TargetPosition.Y == usr.Position.Y)
						{
							bool update = false;
							if (usr.Actions.Has("mv"))
							{
								usr.Actions.Remove("mv");
								update = true;
							}

							RoomTile tile = usr.Room.Mapping.Tiles[usr.TargetPosition.X, usr.TargetPosition.Y];

							if (tile.TopItem != null && tile.TopItem.ItemData.CanSit)
							{
								usr.Actions.Add("sit", tile.TopItem.ItemData.Height.ToString());
								usr.Position.Rotation = tile.TopItem.Position.Rotation;
								usr.Position.HeadRotation = usr.Position.Rotation;
								usr.Position.Z = tile.TopItem.ItemData.Height + tile.TopItem.Position.Z;
								usr.isSitting = false;
								update = true;
							}
							else
							{
								if (!usr.isSitting && usr.Actions.Has("sit"))
								{
									usr.Actions.Remove("sit");
									usr.Position.Z = tile.Position.Z;
									update = true;
								}
							}

							if (update)
							{
								usr.Room.UserManager.Send(new RoomUserStatusComposer(usr));
							}

							if (usr.Room.Model.Door.X == usr.Position.X && usr.Room.Model.Door.Y == usr.Position.Y)
							{
								usr.Room.UserManager.OnUserLeave(usr.Habbo.Session);
								usr.Habbo.Session.Send(new HotelViewComposer());
							}
						}
					}
				}
				catch { }
			});
		}
	}
}
