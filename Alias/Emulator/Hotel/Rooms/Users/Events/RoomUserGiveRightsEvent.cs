using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	public class RoomUserGiveRightsEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int userId = message.Integer();

			Room room = session.Habbo().CurrentRoom;
			if (room == null)
			{
				return;
			}

			if (room.RoomData.OwnerId == session.Habbo().Id)
			{
				room.RoomRights.GiveRights(userId);

				Habbo target = room.UserManager.UserByUserid(userId).Habbo;
				if (target != null)
				{
					room.RoomRights.RefreshRights(target);
				}

				room.UserManager.Send(new RoomAddRightsListComposer(room.Id, userId, ""));
			}
		}
	}
}
