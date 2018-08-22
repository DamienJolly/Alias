using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserWalkEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int x = message.PopInt();
			int y = message.PopInt();

			if ((session.Habbo.Entity.Position.X == x && session.Habbo.Entity.Position.Y == y) || !room.Mapping.Tiles[x, y].IsValidTile(session.Habbo.Entity, true))
			{
				return;
			}

			session.Habbo.Entity.TargetPosition = new UserPosition
			{
				X = x,
				Y = y
			};
			session.Habbo.Entity.Path = room.PathFinder.Path(session.Habbo.Entity);
		}
	}
}
