using System.Collections.Generic;

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
