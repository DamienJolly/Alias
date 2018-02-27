using System.Collections.Generic;
using System.Drawing;
using Alias.Emulator.Hotel.Rooms.Users;

namespace Alias.Emulator.Hotel.Rooms.Models
{
    public class GameMap
    {
		private Dictionary<Point, List<RoomUser>> users;
		//private List<RoomItem> items;

		public Room Room
		{
			get; set;
		}

		public DynamicRoomModel DynamicModel
		{
			get; set;
		}

		public GameMap(Room room)
		{
			this.Room = room;
			this.users = new Dictionary<Point, List<RoomUser>>();
			//this.items = new List<RoomItem>();
			this.DynamicModel = new DynamicRoomModel(room);
		}

		public void AddUserToMap(RoomUser user, Point position)
		{
			if (this.users.ContainsKey(position))
			{
				this.users[position].Add(user);
			}
			else
			{
				List<RoomUser> users = new List<RoomUser>();
				users.Add(user);
				this.users.TryAdd(position, users);
			}
		}

		public void RemoveUserFromMap(RoomUser user, Point position)
		{
			if (this.users.ContainsKey(position))
			{
				this.users[position].Remove(user);
			}
		}

		public void UpdateUserMovement(RoomUser user, Point oldPosition, Point newPosition)
		{
			RemoveUserFromMap(user, oldPosition);
			AddUserToMap(user, newPosition);
		}

		public bool MapHasUser(Point position)
		{
			return GetRoomUsers(position).Count > 0;
		}

		public List<RoomUser> GetRoomUsers(Point position)
		{
			if (this.users.ContainsKey(position))
			{
				return this.users[position];
			}
			else
			{
				return new List<RoomUser>();
			}
		}

		public bool ValidTile(int x, int y)
		{
			return (x < 0 || y < 0 || x >= this.DynamicModel.SizeX || y >= this.DynamicModel.SizeY || this.DynamicModel.Tiles[x, y].State == TileState.OPEN);
		}

		public bool IsValidPosition(int x, int y, bool Override = false)
		{
			if (!ValidTile(x, y))
			{
				return false;
			}

			// do items

			if (this.MapHasUser(new Point(x, y))) // room blocking
			{
				return false;
			}

			return true;
		}
	}
}
