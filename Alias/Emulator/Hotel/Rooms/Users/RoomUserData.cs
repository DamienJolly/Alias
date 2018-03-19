using System.Collections.Generic;
using System.Drawing;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Rooms.Users
{
    public class RoomUserData
    {
		public Habbo Habbo
		{
			get; set;
		} = null;

		public RoomUserType Type
		{
			get; set;
		} = RoomUserType.Player;

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

		public bool isSitting
		{
			get; set;
		} = false;

		public RoomUserData()
		{

		}
	}
}
