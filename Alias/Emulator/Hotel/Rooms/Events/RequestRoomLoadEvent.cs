using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Events
{
	public class RequestRoomLoadEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int roomId = message.Integer();
			string password = message.String();

			if (RoomLoader.CanEnter(session, roomId, password))
			{
				session.Send(new RoomOpenComposer());
				RoomLoader.Enter(session, RoomManager.Room(roomId));
			}
			else
			{
				session.Send(new RoomAccessDeniedComposer(""));
				session.Send(new GenericErrorComposer(-100002));
				session.Send(new HotelViewComposer());
			}
		}
	}
}
