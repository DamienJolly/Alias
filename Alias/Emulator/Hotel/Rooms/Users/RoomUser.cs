using System.Collections.Generic;
using System.Drawing;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Rooms.Users
{
	public class RoomUser
	{
		public int VirtualId
		{
			get; set;
		}

		public Room Room
		{
			get; set;
		}

		public Habbo Habbo
		{
			get; set;
		}

		public UserPosition Position
		{
			get; set;
		}

		public LinkedList<Point> Path
		{
			get; set;
		}

		public UserPosition TargetPosition
		{
			get; set;
		}

		public UserActions Actions
		{
			get; set;
		} = new UserActions();

		public RoomUser()
		{

		}

		public void Dispose()
		{
			this.Room = null;
			this.Habbo = null;
			//todo:
		}
	}
}
