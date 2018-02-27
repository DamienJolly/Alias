using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Rooms.Items.Tasks;
using Alias.Emulator.Hotel.Rooms.Models;
using Alias.Emulator.Hotel.Rooms.Pathfinding;
using Alias.Emulator.Hotel.Rooms.Tasks;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Rooms.Users.Tasks;

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

		public RoomItemManager ItemManager
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

		public DynamicRoomModel DynamicModel
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

		public void Cycle()
		{
			if (this.Disposing)
			{
				return;
			}

			RoomTask.Start(this);
			if (this.UserManager != null)
			{
				WalkTask.Start(this.UserManager.Users);
			}
			if (this.ItemManager != null)
			{
				ItemTask.Start(this.ItemManager.Items);
			}
		}

		public void Initialize()
		{
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
			RoomItemDatabase.SaveFurniture(this.ItemManager.Items);
			RoomDatabase.SaveRoom(this.RoomData);
			RoomManager.RemoveLoadedRoom(this);
		}
	}
}
