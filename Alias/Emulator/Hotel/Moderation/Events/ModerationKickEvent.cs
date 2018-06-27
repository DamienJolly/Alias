using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationKickEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_user_kick"))
			{
				return;
			}

			int userId = message.PopInt();
			if (userId <= 0)
			{
				return;
			}

			Session target = Alias.Server.SocketServer.SessionManager.SessionById(message.PopInt());
			if (target != null)
			{
				session.Habbo.CurrentRoom.UserManager.OnUserLeave(target);
				target.Send(new ModerationIssueHandledComposer(message.PopString()));
			}
		}
	}
}
