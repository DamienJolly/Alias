using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Models.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Events
{
	public class RequestRoomHeightmapEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (session.Habbo.CurrentRoom == null)
			{
				return;
			}
			session.Send(new RoomRelativeMapComposer(session.Habbo.CurrentRoom));
			session.Send(new RoomHeightMapComposer(session.Habbo.CurrentRoom));
			session.Send(new RoomEntryInfoComposer(session.Habbo.CurrentRoom, session.Habbo));
			session.Send(new RoomThicknessComposer(session.Habbo.CurrentRoom));

			session.Habbo.CurrentRoom.UserManager.OnUserJoin(session);

			session.Send(new RoomFloorItemsComposer(session.Habbo.CurrentRoom.ItemManager.Items));
			//session.Send(new ItemsComposer()); //todo: wall items
		}
	}
}
