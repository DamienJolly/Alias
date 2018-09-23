using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserGiveRightsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null || room.RoomData.OwnerId != session.Player.Id)
			{
				return;
			}

			int userId = message.PopInt();
			room.RoomRights.GiveRights(userId);

			Player target = room.EntityManager.UserByUserid(userId).Player;
			if (target != null)
			{
				room.RoomRights.RefreshRights(target);
			}

			room.EntityManager.Send(new RoomAddRightsListComposer(room.Id, userId, ""));
		}
	}
}
