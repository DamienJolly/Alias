using Alias.Emulator.Hotel.Players.Messenger.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Messenger.Events
{
	class FriendPrivateMessageEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			int targetId = message.PopInt();
			Player targetPlayer = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(targetId);
			if (targetPlayer == null)
			{
				return;
			}

			string data = message.PopString();
			if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data) || data.Length > 200)
			{
				return;
			}

			if (session.Player.Muted)
			{
				session.Send(new RoomInviteErrorComposer(RoomInviteErrorComposer.YOU_ARE_MUTED, targetPlayer.Id));
				return;
			}

			if (!session.Player.Messenger.Friends.ContainsKey(targetId) || !targetPlayer.Messenger.Friends.ContainsKey(session.Player.Id))
			{
				session.Send(new RoomInviteErrorComposer(RoomInviteErrorComposer.NO_FRIENDS, targetPlayer.Id));
				return;
			}

			if (targetPlayer.Session != null)
			{
				if (targetPlayer.Muted)
				{
					session.Send(new RoomInviteErrorComposer(RoomInviteErrorComposer.FRIEND_MUTED, targetPlayer.Id));
				}

				//todo: log message
				targetPlayer.Session.Send(new FriendChatMessageComposer(session.Player.Id, data));
			}
			else
			{
				//todo: Offline messages
			}
		}
	}
}
