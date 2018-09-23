using Alias.Emulator.Hotel.Players.Messenger.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Messenger.Events
{
	class RemoveFriendEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			int amount = message.PopInt();
			if (amount > 100)
			{
				amount = 100;
			}
			else if (amount < 0)
			{
				amount = 0;
			}

			for (int i = 0; i < amount; i++)
			{
				int userId = message.PopInt();
				Player targetPlayer = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(userId);
				if (targetPlayer == null)
				{
					return;
				}

				await session.Player.Messenger.RemoveFriend(targetPlayer.Id);
				session.Send(new UpdateFriendComposer(targetPlayer.Id));

				if (targetPlayer.Session != null)
				{
					targetPlayer.Messenger.Friends.Remove(session.Player.Id);
					targetPlayer.Session.Send(new UpdateFriendComposer(session.Player.Id));
				}
			}
		}
	}
}
