using System;
using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Users.Messenger.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	public class FindNewFriendsEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			Random rnd = new Random();
			List<Room> rooms = RoomManager.ReadLoadedRooms().OrderBy(a => rnd.Next()).ToList();

			foreach (Room room in rooms)
			{
				if (room.UserManager.UserCount > 0 && !room.Disposing)
				{
					session.Send(new ForwardToRoomComposer(room.Id));
					return;
				}
			}

			session.Send(new FriendFindingRoomComposer(FriendFindingRoomComposer.NO_ROOM_FOUND));
		}
	}
}
