using Alias.Emulator.Hotel.Rooms.Cycle;
using Alias.Emulator.Hotel.Rooms.Models;
using Alias.Emulator.Hotel.Rooms.Pathfinding;
using Alias.Emulator.Hotel.Rooms.Users;

namespace Alias.Emulator.Hotel.Rooms
{
	public class Room
	{
		public int Id
		{
			get; set;
		}

		public RoomData RoomData
		{
			get; set;
		}

		public RoomUserManager UserManager
		{
			get; set;
		}

		public RoomModel Model
		{
			get
			{
				return RoomModelManager.GetModel(this.RoomData.ModelName);
			}
		}

		public RoomCycleTask Cycle
		{
			get; set;
		}

		public GameMap GameMap
		{
			get; set;
		}

		public PathFinder PathFinder
		{
			get; set;
		}

		public int IdleTime = 0;

		public bool Disposing
		{
			get; set;
		} = false;

		public Room()
		{

		}

		public void Initialize()
		{
			this.Cycle = new RoomCycleTask();
			this.Cycle.Room = this;
			this.Cycle.StartCycle();
			this.PathFinder = new PathFinder(this);
		}

		public void OnRoomCrash()
		{
			this.Disposing = true;
			foreach (RoomUser user in this.UserManager.Users)
			{
				if (user.Habbo == null || user.Habbo.Session() == null)
				{
					user.Habbo.Notification("Sorry, it appears that room has crashed!");
				}

				this.UserManager.OnUserLeave(user.Habbo.Session());
			}
			this.Dispose();
		}

		public void Dispose()
		{
			this.Disposing = true;
			this.Cycle.StopCycle();
			RoomDatabase.SaveRoom(this.RoomData);
			RoomManager.RemoveLoadedRoom(this);
		}
	}
}
