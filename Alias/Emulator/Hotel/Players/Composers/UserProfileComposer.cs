using Alias.Emulator.Hotel.Groups;
using Alias.Emulator.Hotel.Players.Messenger;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Composers
{
	class UserProfileComposer : IPacketComposer
	{
		private Player _player;
		private Session _viewer;

		public UserProfileComposer(Player player, Session viewer)
		{
			_player = player;
			_viewer = viewer;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserProfileMessageComposer);
			message.WriteInteger(_player.Id);
			message.WriteString(_player.Username);
			message.WriteString(_player.Look);
			message.WriteString(_player.Motto);
			message.WriteString("01.01.1970 00:00:00"); //Account created
			message.WriteInteger(_player.AchievementScore);
			message.WriteInteger(Alias.Server.PlayerManager.IsOnline(_player.Id) ? _player.Messenger.Friends.Count : 0);
			message.WriteBoolean(false);//todo: is friend
			message.WriteBoolean(false);//todo: has requested
			message.WriteBoolean(Alias.Server.PlayerManager.IsOnline(_player.Id));

			message.WriteInteger(_player.Groups.Count);
			_player.Groups.ForEach(groupId =>
			{
				Group group = Alias.Server.GroupManager.GetGroup(groupId);
				if (group != null)
				{
					message.WriteInteger(group.Id);
					message.WriteString(group.Name);
					message.WriteString(group.Badge);
					message.WriteString(group.ColourOne.ToString());
					message.WriteString(group.ColourTwo.ToString());
					message.WriteBoolean(group.Id == _player.GroupId);
					message.WriteInteger(group.OwnerId);
					message.WriteBoolean(group.OwnerId == _player.Id);
				}
			});
			
			message.WriteInteger(0); //Last online (seconds)
			message.WriteBoolean(true);
			return message;
		}
	}
}
