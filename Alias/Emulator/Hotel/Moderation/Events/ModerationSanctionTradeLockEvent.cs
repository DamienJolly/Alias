using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationSanctionTradeLockEvent : IPacketEvent
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

			//todo: tradelocks
		}
	}
}
