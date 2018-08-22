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
			Room room = session.Habbo.CurrentRoom;
			if (room == null || room.RoomData.OwnerId != session.Habbo.Id)
			{
				return;
			}

			int userId = message.PopInt();
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
