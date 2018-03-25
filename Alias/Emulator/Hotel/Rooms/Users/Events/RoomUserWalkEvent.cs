using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	public class RoomUserWalkEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (session.Habbo != null && session.Habbo.CurrentRoom != null)
			{
				int x = message.Integer();
				int y = message.Integer();
				
				RoomUser usr = session.Habbo.CurrentRoom.UserManager.UserBySession(session);

				if ((usr.Position.X == x && usr.Position.Y == y) || !usr.Room.DynamicModel.ValidTile(x, y, true))
				{
					return;
				}

				usr.TargetPosition = new UserPosition
				{
					X = x,
					Y = y
				};

				usr.Path = usr.Room.PathFinder.Path(usr.Position, usr.TargetPosition);
			}
		}
	}
}
