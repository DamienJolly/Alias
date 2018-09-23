using Alias.Emulator.Hotel.Players.Messenger.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Messenger.Events
{
	class ChangeRelationEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			int userId = message.PopInt();
			if (!session.Player.Messenger.TryGetFriend(userId, out MessengerFriend friend))
			{
				return;
			}

			int type = message.PopInt();
			if (type < 0 || type > 3)
			{
				return;
			}

			friend.Relation = type;
			await session.Player.Messenger.UpdateRelation(friend);
			session.Send(new UpdateFriendComposer(friend));
		}
	}
}
