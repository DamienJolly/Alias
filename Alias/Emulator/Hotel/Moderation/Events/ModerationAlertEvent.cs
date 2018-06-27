using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationAlertEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_user_alert"))
			{
				return;
			}

			int userId = message.PopInt();
			if (userId <= 0)
			{
				return;
			}

			Session target = Alias.Server.SocketServer.SessionManager.SessionById(userId);
			if (target != null)
			{
				target.Send(new ModerationIssueHandledComposer(message.PopString()));
			}
		}
	}
}
