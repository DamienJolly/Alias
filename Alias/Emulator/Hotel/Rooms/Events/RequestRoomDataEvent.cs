using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Events
{
	public class RequestRoomDataEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			RoomLoader.PrepareLoading(session, message.Integer(), message.Integer(), message.Integer());
		}
	}
}
