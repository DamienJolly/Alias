using System.Collections.Generic;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Hotel.Players.Messenger.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Messenger.Events
{
	class InviteFriendsEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			if (session.Player.Muted)
			{
				session.Send(new RoomInviteErrorComposer(RoomInviteErrorComposer.YOU_ARE_MUTED, -1));
				return;
			}

			List<Player> friends = new List<Player>();
			int amount = message.PopInt();
			for (int i = 0; i < amount; i++)
			{
				int targetId = message.PopInt();
				Player targetPlayer = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(targetId);
				if (targetPlayer != null)
				{
					friends.Add(targetPlayer);
				}
			}

			string data = message.PopString();
			if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data) || data.Length > 200)
			{
				return;
			}

			foreach (Player friend in friends)
			{
				if (session.Player.Messenger.Friends.ContainsKey(friend.Id) || !friend.Messenger.Friends.ContainsKey(session.Player.Id))
				{
					session.Send(new RoomInviteErrorComposer(RoomInviteErrorComposer.NO_FRIENDS, friend.Id));
					continue;
				}

				if (friend.Session != null)
				{
					if (friend.Settings.IgnoreInvites)
					{
						session.Send(new RoomInviteErrorComposer(RoomInviteErrorComposer.FRIEND_BUSY, friend.Id));
						continue;
					}

					if (friend.Muted)
					{
						session.Send(new RoomInviteErrorComposer(RoomInviteErrorComposer.FRIEND_MUTED, friend.Id));
					}

					//todo: log invite
					friend.Session.Send(new RoomInviteComposer(session.Player.Id, data));
				}
			}
		}
	}
}
