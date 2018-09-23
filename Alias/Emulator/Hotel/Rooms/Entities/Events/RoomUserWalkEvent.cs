using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserWalkEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int x = message.PopInt();
			int y = message.PopInt();

			if ((session.Player.Entity.Position.X == x && session.Player.Entity.Position.Y == y) || !room.Mapping.Tiles[x, y].IsValidTile(session.Player.Entity, true))
			{
				return;
			}
			
			session.Player.Entity.TargetPosition = new UserPosition
			{
				X = x,
				Y = y
			};
			session.Player.Entity.Path = room.PathFinder.Path(session.Player.Entity);
		}
	}
}
