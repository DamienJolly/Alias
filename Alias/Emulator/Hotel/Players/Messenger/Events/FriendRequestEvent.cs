using Alias.Emulator.Hotel.Players.Messenger.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Messenger.Events
{
	class FriendRequestEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			string username = message.PopString();

			int userId = 0; //todo: get userid from username
			Player targetPlayer = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(userId);
			if (targetPlayer == null)
			{
				return;
			}

			if (!session.Player.Messenger.TryGetRequest(targetPlayer.Id, out MessengerRequest request))
			{
				MessengerRequest newRequest = new MessengerRequest(session.Player.Id, session.Player.Username, session.Player.Look);
				await session.Player.Messenger.AddRequest(newRequest, targetPlayer.Id);

				if (targetPlayer.Session != null && targetPlayer.Messenger != null)
				{
					if(!targetPlayer.Messenger.Requests.ContainsKey(request.Id))
					{
						targetPlayer.Messenger.Requests.Add(request.Id, request);
					}
					targetPlayer.Session.Send(new FriendRequestComposer(newRequest));
				}
			}
		}
	}
}
