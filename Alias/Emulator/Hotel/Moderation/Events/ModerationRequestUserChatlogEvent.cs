using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationRequestUserChatlogEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Player.HasPermission("acc_modtool_user_logs"))
			{
				return;
			}

			int userId = message.PopInt();
			if (userId <= 0)
			{
				return;
			}

			//todo: chatlogs
		}
	}
}
