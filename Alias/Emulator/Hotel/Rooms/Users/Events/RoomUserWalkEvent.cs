using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	class RoomUserWalkEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (session.Habbo != null && session.Habbo.CurrentRoom != null)
			{
				int x = message.PopInt();
				int y = message.PopInt();
				
				RoomUser usr = session.Habbo.CurrentRoom.UserManager.UserBySession(session);

				usr.TargetPosition = new UserPosition
				{
					X = x,
					Y = y
				};

				if ((usr.Position.X == x && usr.Position.Y == y) || !usr.Room.Mapping.Tiles[x, y].IsValidTile(usr, true))
				{
					return;
				}

				usr.Path = usr.Room.PathFinder.Path(usr);
			}
		}
	}
}
