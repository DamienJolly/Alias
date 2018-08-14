using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserWalkEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (session.Habbo != null && session.Habbo.CurrentRoom != null)
			{
				int x = message.PopInt();
				int y = message.PopInt();
				
				RoomEntity usr = session.Habbo.CurrentRoom.EntityManager.UserBySession(session);
				if ((usr.Position.X == x && usr.Position.Y == y) || !usr.Room.Mapping.Tiles[x, y].IsValidTile(usr, true))
				{
					return;
				}

				usr.TargetPosition = new UserPosition
				{
					X = x,
					Y = y
				};
				usr.Path = usr.Room.PathFinder.Path(usr);
			}
		}
	}
}
