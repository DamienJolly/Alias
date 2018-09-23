using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Messenger.Composers
{
	public class UpdateFriendComposer : IPacketComposer
	{
		private MessengerFriend _friend;
		private int _friendId = 0;

		public UpdateFriendComposer(MessengerFriend friend)
		{
			_friend = friend;
		}

		public UpdateFriendComposer(int friendId)
		{
			_friendId = friendId;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UpdateFriendMessageComposer);
			if (_friendId > 0)
			{
				message.WriteInteger(0);
				message.WriteInteger(1);
				message.WriteInteger(-1);
				message.WriteInteger(_friendId);
			}
			else
			{
				message.WriteInteger(0);
				message.WriteInteger(1);
				message.WriteInteger(0);
				message.WriteInteger(_friend.Id);
				message.WriteString(_friend.Username);
				message.WriteInteger(1); //Gender???
				message.WriteBoolean(Alias.Server.PlayerManager.IsOnline(_friend.Id));
				message.WriteBoolean(_friend.InRoom);
				message.WriteString(_friend.Look);
				message.WriteInteger(0); //category id
				message.WriteString(_friend.Motto);
				message.WriteString("");
				message.WriteString("");
				message.WriteBoolean(true);
				message.WriteBoolean(false);
				message.WriteBoolean(false);
				message.WriteShort(_friend.Relation);
			}
			return message;
		}
	}
}
