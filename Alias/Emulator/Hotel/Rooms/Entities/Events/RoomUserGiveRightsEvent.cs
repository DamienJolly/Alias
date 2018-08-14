using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserGiveRightsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int userId = message.PopInt();

			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			if (room.RoomData.OwnerId == session.Habbo.Id)
			{
				room.RoomRights.GiveRights(userId);

				Habbo target = room.EntityManager.UserByUserid(userId).Habbo;
				if (target != null)
				{
					room.RoomRights.RefreshRights(target);
				}

				room.EntityManager.Send(new RoomAddRightsListComposer(room.Id, userId, ""));
			}
		}
	}
}
