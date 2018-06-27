using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationRequestRoomVisitsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_user_info"))
			{
				return;
			}

			int userId = message.PopInt();
			if (userId <= 0)
			{
				return;
			}

			Habbo target = Alias.Server.SocketServer.SessionManager.HabboById(userId);
			if (target != null)
			{
				session.Send(new ModerationUserRoomVisitsComposer(target));
			}
		}
	}
}
