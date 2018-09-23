using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationKickEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			if (!session.Player.HasPermission("acc_modtool_user_kick"))
			{
				return;
			}

			int userId = message.PopInt();
			Player target = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(userId);
			if (target == null || target.Session == null)
			{
				return;
			}

			target.CurrentRoom.EntityManager.OnUserLeave(target.Entity);
			target.Session.Send(new ModerationIssueHandledComposer(message.PopString()));
		}
	}
}
