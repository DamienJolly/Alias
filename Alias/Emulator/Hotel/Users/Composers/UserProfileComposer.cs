using Alias.Emulator.Hotel.Groups;
using Alias.Emulator.Hotel.Users.Messenger;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Composers
{
	class UserProfileComposer : IPacketComposer
	{
		private Habbo habbo;
		private Session viewer;

		public UserProfileComposer(Habbo habbo, Session viewer)
		{
			this.habbo = habbo;
			this.viewer = viewer;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserProfileMessageComposer);
			message.WriteInteger(habbo.Id);
			message.WriteString(habbo.Username);
			message.WriteString(habbo.Look);
			message.WriteString(habbo.Motto);
			message.WriteString("01.01.1970 00:00:00"); //Account created
			message.WriteInteger(habbo.AchievementScore);
			message.WriteInteger(Alias.Server.SocketServer.SessionManager.IsOnline(habbo.Id) ? habbo.Messenger.FriendList().Count : MessengerComponent.GetFriendList(habbo.Id).Count);
			message.WriteBoolean(viewer.Habbo.Messenger.IsFriend(habbo.Id));
			message.WriteBoolean(viewer.Habbo.Messenger.RequestExists(habbo.Id));
			message.WriteBoolean(Alias.Server.SocketServer.SessionManager.IsOnline(habbo.Id));

			message.WriteInteger(habbo.Groups.Count);
			habbo.Groups.ForEach(groupId =>
			{
				Group group = Alias.Server.GroupManager.GetGroup(groupId);
				if (group != null)
				{
					message.WriteInteger(group.Id);
					message.WriteString(group.Name);
					message.WriteString(group.Badge);
					message.WriteString(group.ColourOne.ToString());
					message.WriteString(group.ColourTwo.ToString());
					message.WriteBoolean(group.Id == habbo.GroupId);
					message.WriteInteger(group.OwnerId);
					message.WriteBoolean(group.OwnerId == habbo.Id);
				}
			});
			
			message.WriteInteger(0); //Last online (seconds)
			message.WriteBoolean(true);
			return message;
		}
	}
}
