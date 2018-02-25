using Alias.Emulator.Hotel.Rooms.Models;
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
			//todo: 
		}

		public void OnRoomCrash()
		{
			this.Disposing = true;
			foreach (RoomUser user in this.UserManager.Users)
			{
				//if (user.Habbo == null || user.Habbo.Session() == null)
					//todo: notify user the room crashed

				this.UserManager.OnUserLeave(user.Habbo.Session());
			}
			this.Dispose();
		}

		public void Dispose()
		{
			this.Disposing = true;
			RoomDatabase.SaveRoom(this.RoomData);
			RoomManager.RemoveLoadedRoom(this);
		}
	}
}
