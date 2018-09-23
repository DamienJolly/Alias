using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationSanctionMuteEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			if (!session.Player.HasPermission("acc_modtool_user_mute"))
			{
				return;
			}
			
			int userId = message.PopInt();
			Player target = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(userId);
			if (target == null || target.Session == null)
			{
				return;
			}
			
			string text = message.PopString();
			double duration = 60 * 60;

			// todo: muting
			target.Muted = true;
			target.Session.Send(new ModerationIssueHandledComposer(message.PopString()));
		}
	}
}
