using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	public class RoomUsersComposer : IPacketComposer
	{
		public List<RoomUser> users;

		public RoomUsersComposer(RoomUser user)
		{
			this.users = new List<RoomUser>();
			this.users.Add(user);
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
				message.WriteInteger(user.Habbo.Id);
				message.WriteString(user.Habbo.Username);
				message.WriteString(user.Habbo.Motto);
				message.WriteString(user.Habbo.Look);
				message.WriteInteger(user.VirtualId);
				message.WriteInteger(user.Position.X);
				message.WriteInteger(user.Position.Y);
				message.WriteString(user.Position.Z.ToString());
				message.WriteInteger(2);
				message.WriteInteger(1);
				message.WriteString(user.Habbo.Gender.ToLower());
				message.WriteInteger(-1); //groupid
				message.WriteInteger(0); //groupwhat
				message.WriteString(""); //Groupname?
				message.WriteString("");
				message.WriteInteger(user.Habbo.AchievementScore); //achievement points
				message.WriteBoolean(false); //idk

			});
			return message;
		}
	}
}
