using Alias.Emulator.Hotel.Players.Messenger.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Messenger.Events
{
	class AcceptFriendRequestEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			int amount = message.PopInt();
			if (amount > 50)
			{
				amount = 50;
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

				if (!session.Player.Messenger.TryGetRequest(targetPlayer.Id, out MessengerRequest request))
				{
					MessengerFriend friendOne = new MessengerFriend(targetPlayer.Id, targetPlayer.Username, targetPlayer.Look, targetPlayer.Motto, targetPlayer.CurrentRoom != null);
					await session.Player.Messenger.RemoveRequest(targetPlayer.Id);
					await session.Player.Messenger.AddFriendShip(friendOne);
					session.Send(new UpdateFriendComposer(friendOne));

					if (targetPlayer.Session != null)
					{
						MessengerFriend friendTwo = new MessengerFriend(session.Player.Id, session.Player.Username, session.Player.Look, session.Player.Motto, session.Player.CurrentRoom != null);
						if (!targetPlayer.Messenger.Friends.ContainsKey(friendTwo.Id))
						{
							targetPlayer.Messenger.Friends.Add(friendTwo.Id, friendTwo);
						}
						targetPlayer.Session.Send(new UpdateFriendComposer(friendTwo));
					}
				}
			}
		}
	}
}
