using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Events
{
	class RequestRoomSettingsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (session.Player.CurrentRoom == null || !session.Player.CurrentRoom.RoomRights.HasRights(session.Player.Id))
			{
				return;
			}

			session.Send(new RoomSettingsComposer(session.Player.CurrentRoom));
		}
	}
}
