using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Hotel.Rooms.Items;
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

						Point p = usr.Path.First();
						RoomItem item = usr.Room.DynamicModel.GetTopItemAt(p.X, p.Y);
						if (usr.Room.DynamicModel.CanSitAt(p.X, p.Y, true))
						{
							RoomItem lowestChair = usr.Room.DynamicModel.GetLowestChair(p.X, p.Y);
							if (lowestChair != null)
							{
								item = lowestChair;
							}
						}

						RoomItem habboItem = usr.Room.DynamicModel.GetTopItemAt(usr.Position.X, usr.Position.Y);
						if (habboItem != null)
						{
							if (habboItem != item || !usr.Room.DynamicModel.PointInSquare(habboItem.Position.X, habboItem.Position.Y, habboItem.Position.X + habboItem.ItemData.Width - 1, habboItem.Position.Y + habboItem.ItemData.Length - 1, p.X, p.Y))
								habboItem.GetInteractor().OnUserWalkOff(usr.Habbo.Session, usr.Room, habboItem);
						}

						double height = 0.0;

						if (item != null)
						{
							if (item != habboItem || !usr.Room.DynamicModel.PointInSquare(item.Position.X, item.Position.Y, item.Position.X + item.ItemData.Width - 1, item.Position.Y + item.ItemData.Length - 1, usr.Position.X, usr.Position.Y))
							{
								item.GetInteractor().OnUserWalkOn(usr.Habbo.Session, usr.Room, habboItem);
							}
							height += item.Position.Z;
							if (!item.ItemData.CanSit && !item.ItemData.CanLay)
							{
								// todo: multiheight furni
								height += item.ItemData.Height;
							}
						}
						else
						{
							height += usr.Room.DynamicModel.GetHeightAtSquare(p.X, p.Y);
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
							usr.Path = usr.Room.PathFinder.Path(usr.Position, usr.TargetPosition);
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

							RoomItem chair = null;
							if (usr.Room.DynamicModel.CanSitAt(usr.Position.X, usr.Position.Y, true))
							{
								chair = usr.Room.DynamicModel.GetLowestChair(usr.Position.X, usr.Position.Y);
							}
							else
							{
								chair = usr.Room.DynamicModel.GetTopItemAt(usr.Position.X, usr.Position.Y);
								if (chair != null && !chair.ItemData.CanSit)
								{
									chair = null;
								}
							}

							if (chair != null)
							{
								usr.Actions.Add("sit", chair.ItemData.Height.ToString());
								usr.Position.Rotation = chair.Position.Rotation;
								usr.Position.HeadRotation = usr.Position.Rotation;
								usr.Position.Z = usr.Room.DynamicModel.GetTileHeight(usr.Position.X, usr.Position.Y, chair);
								usr.isSitting = false;
								update = true;
							}
							else
							{
								if (!usr.isSitting && usr.Actions.Has("sit"))
								{
									usr.Actions.Remove("sit");
									usr.Position.Z = usr.Room.DynamicModel.GetTileHeight(usr.Position.X, usr.Position.Y);
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
