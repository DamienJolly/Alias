using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	public class RoomUserRemoveRightsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int amount = message.PopInt();

			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			if (room.RoomData.OwnerId == session.Habbo.Id)
			{
				for (int i = 0; i < amount; i++)
				{
					int userId = message.PopInt();

					room.RoomRights.TakeRights(userId);

					Habbo target = room.UserManager.UserByUserid(userId).Habbo;
					if (target != null)
					{
						room.RoomRights.RefreshRights(target);
					}

					room.UserManager.Send(new RoomRemoveRightsListComposer(room.Id, userId));
				}
			}
		}
	}
}
