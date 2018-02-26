using System;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms.Cycle.Tasks
{
	public class RoomTask
	{
		public static void Start(Room room)
		{
			if (room.Disposing)
			{
				return;
			}

			try
			{
				if (room.UserManager.UserCount == 0)
				{
					room.IdleTime++;
				}
				else if (room.IdleTime > 0)
				{
					room.IdleTime = 0;
				}

				if (room.IdleTime >= 60)
				{
					room.Dispose();
				}
			}
			catch (Exception ex)
			{
				Logging.Error("Room: " + room.Id + " has crashed, disposing room..", ex, "RoomTask", "Start");
				room.OnRoomCrash();
			}
		}
	}
}