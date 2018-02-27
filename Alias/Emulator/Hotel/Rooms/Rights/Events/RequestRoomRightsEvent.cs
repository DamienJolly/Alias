using Alias.Emulator.Hotel.Rooms.Rights.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Rights.Events
{
	public class RequestRoomRightsEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (session.Habbo().CurrentRoom == null || !session.Habbo().CurrentRoom.RoomRights.HasRights(session.Habbo()))
			{
				return;
			}

			session.Send(new RoomRightsListComposer(session.Habbo().CurrentRoom));
		}
	}
}
