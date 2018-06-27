using System.Collections.Generic;
using Alias.Emulator.Hotel.Groups;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	class RoomUsersComposer : IPacketComposer
	{
		public List<RoomUser> users;

		public RoomUsersComposer(RoomUser user)
		{
			this.users = new List<RoomUser>{ user };
		}

		public RoomUsersComposer(List<RoomUser> users)
		{
			this.users = users;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUsersMessageComposer);
			message.WriteInteger(this.users.Count);
			this.users.ForEach(user =>
			{
				message.WriteInteger(user.Id);
				message.WriteString(user.Name);
				message.WriteString(user.Motto);
				message.WriteString(user.Look);
				message.WriteInteger(user.VirtualId);
				message.WriteInteger(user.Position.X);
				message.WriteInteger(user.Position.Y);
				message.WriteString(user.Position.Z.ToString());
				switch (user.Type)
				{
					case RoomUserType.Player:
					default:
						{
							message.WriteInteger(2);
							message.WriteInteger(1);
							message.WriteString(user.Gender.ToLower());
							if (Alias.Server.GroupManager.TryGetGroup(user.Habbo.GroupId, out Group group))
							{
								message.WriteInteger(group.Id);
								message.WriteInteger(0); //??
								message.WriteString(group.Name);
							}
							else
							{
								message.WriteInteger(-1);
								message.WriteInteger(0); //??
								message.WriteString("");
							}
							message.WriteString("");
							message.WriteInteger(user.Habbo.AchievementScore); //achievement points
							message.WriteBoolean(false); //idk
							break;
						}
					case RoomUserType.Bot:
						{
							message.WriteInteger(0);
							message.WriteInteger(4);
							message.WriteString(user.Gender.ToLower()); // ?
							message.WriteInteger(1); //Owner Id
							message.WriteString("Damien"); // Owner name
							message.WriteInteger(5);
							{
								message.WriteInteger(5);//Action Count
								message.WriteShort(1);//Copy looks
								message.WriteShort(2);//Setup speech
								message.WriteShort(3);//Relax
								message.WriteShort(4);//Dance
								message.WriteShort(5);//Change name
							}
							break;
						}
					case RoomUserType.Pet:
						{
							message.WriteInteger(0);
							message.WriteInteger(2);
							message.WriteInteger(1); // pet type
							message.WriteInteger(1); // userid
							message.WriteString("Damien"); // username
							message.WriteInteger(1);
							message.WriteBoolean(false); // has saddle
							message.WriteBoolean(false); // being rode
							message.WriteInteger(0);
							message.WriteInteger(0);
							message.WriteString("");
							break;
						}
				}
			});
			return message;
		}
	}
}
