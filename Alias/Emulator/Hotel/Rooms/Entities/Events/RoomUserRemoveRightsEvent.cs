using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserRemoveRightsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int amount = message.PopInt();

			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			if (room.RoomData.OwnerId == session.Player.Id)
			{
				for (int i = 0; i < amount; i++)
				{
					int userId = message.PopInt();

					room.RoomRights.TakeRights(userId);

					Player target = room.EntityManager.UserByUserid(userId).Player;
					if (target != null)
					{
						room.RoomRights.RefreshRights(target);
					}

					room.EntityManager.Send(new RoomRemoveRightsListComposer(room.Id, userId));
				}
			}
		}
	}
}
