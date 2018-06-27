using Alias.Emulator.Hotel.Rooms.Rights.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Rights.Events
{
	class RequestRoomRightsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (session.Habbo.CurrentRoom == null || !session.Habbo.CurrentRoom.RoomRights.HasRights(session.Habbo.Id))
			{
				return;
			}

			session.Send(new RoomRightsListComposer(session.Habbo.CurrentRoom));
		}
	}
}
