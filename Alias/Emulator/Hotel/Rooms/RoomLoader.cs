using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Rooms.States;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms
{
	public class RoomLoader
	{
		public static void PrepareLoading(Session session, int roomId, int num, int num2)
		{
			if (RoomDatabase.RoomExists(roomId))
			{
				session.Send(new RoomDataComposer(RoomManager.RoomData(roomId), !(num == 0 && num2 == 1), true, session));
			}
			else
			{
				Logging.Info("Room with Id %id% doesn't exist.".Replace("%id%", roomId.ToString()));
			}
		}

		public static bool CanEnter(Session session, int roomId, string password)
		{
			if (RoomDatabase.RoomExists(roomId))
			{
				RoomData rdata = RoomManager.RoomData(roomId);
				switch (rdata.DoorState)
				{
					case RoomDoorState.OPEN:
						return true;
					case RoomDoorState.CLOSED:
						return true;
					case RoomDoorState.PASSWORD:
						{
							return rdata.Password == password || rdata.OwnerId == session.Habbo.Id;
						}
					default:
						return false;
				}
			}
			else
			{
				Logging.Info("Room with Id %id% doesn't exist.".Replace("%id%", roomId.ToString()));
				return false;
			}
		}

		public static void Enter(Session session, Room room)
		{
			session.Send(new RoomModelComposer(room));
			if (room.RoomData.Floor != "0.0")
				session.Send(new RoomPaintComposer("floor", "0.0"));
			if (room.RoomData.Wallpaper != "0.0")
				session.Send(new RoomPaintComposer("wallpaper", "0.0"));
			session.Send(new RoomPaintComposer("landscape", "0.0"));
			session.Send(new RoomScoreComposer(room.RoomData.Likes.Count, room.RoomData.Likes.Contains(room.Id)));
			if (session.Habbo.CurrentRoom != null)
				session.Habbo.CurrentRoom.UserManager.OnUserLeave(session);
			session.Habbo.CurrentRoom = room;
		}
	}
}
