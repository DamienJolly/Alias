using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Rooms.States;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Events
{
	class RequestRoomLoadEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int roomId = message.PopInt();
			if (!Alias.Server.RoomManager.TryGetRoomData(roomId, out RoomData roomData))
			{
				return;
			}

			string password = message.PopString();
			if (roomData.OwnerId == session.Habbo.Id || (roomData.DoorState == RoomDoorState.PASSWORD && roomData.Password != password))
			{
				if (!Alias.Server.RoomManager.TryGetRoom(roomId, out Room room))
				{
					room = Alias.Server.RoomManager.LoadRoom(roomId);
				}

				if (room != null)
				{
					session.Send(new RoomOpenComposer());
					session.Send(new RoomModelComposer(room));
					session.Send(new RoomPaintComposer("floor", "0.0"));
					session.Send(new RoomPaintComposer("wallpaper", "0.0"));
					session.Send(new RoomPaintComposer("landscape", "0.0"));
					session.Send(new RoomScoreComposer(room.RoomData.Likes.Count, room.RoomData.Likes.Contains(room.Id)));
					if (session.Habbo.CurrentRoom != null)
					{
						session.Habbo.CurrentRoom.EntityManager.OnUserLeave(session.Habbo.Entity);
					}
					session.Habbo.CurrentRoom = room;
					return;
				}
			}

			session.Send(new RoomAccessDeniedComposer(""));
			session.Send(new GenericErrorComposer(-100002));
			session.Send(new HotelViewComposer());
		}
	}
}
