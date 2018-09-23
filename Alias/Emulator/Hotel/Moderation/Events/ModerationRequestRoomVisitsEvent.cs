using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationRequestRoomVisitsEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			if (!session.Player.HasPermission("acc_modtool_user_info"))
			{
				return;
			}

			int userId = message.PopInt();
			if (userId <= 0)
			{
				return;
			}

			Player target = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(userId);
			if (target != null)
			{
				session.Send(new ModerationUserRoomVisitsComposer(target));
			}
		}
	}
}
