using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Messenger.Events
{
	class DeclineFriendRequestEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			if (message.PopBoolean())
			{
				foreach (MessengerRequest request in session.Player.Messenger.Requests.Values)
				{
					await session.Player.Messenger.RemoveRequest(request.Id);
				}
			}
			else
			{
				message.PopInt(); //dunno? count maybe for mutli deleting requests
				int userId = message.PopInt();
				await session.Player.Messenger.RemoveRequest(userId);
			}
		}
	}
}
