using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationRoomAlertEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_room_alert"))
			{
				return;
			}

			int type = message.PopInt(); //message - caution

			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			room.UserManager.Send(new ModerationIssueHandledComposer(message.PopString()));
		}
	}
}
