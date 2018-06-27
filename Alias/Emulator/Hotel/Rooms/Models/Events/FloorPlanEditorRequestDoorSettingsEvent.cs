using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Rooms.Models.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Models.Events
{
	class FloorPlanEditorRequestDoorSettingsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (session.Habbo.CurrentRoom == null)
			{
				return;
			}

			session.Send(new FloorPlanEditorDoorSettingsComposer(session.Habbo.CurrentRoom));
			session.Send(new RoomThicknessComposer(session.Habbo.CurrentRoom));
		}
	}
}
