using System.Collections.Generic;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	public class RoomUsersComposer : IMessageComposer
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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomUsersMessageComposer);
			result.Int(this.users.Count);
			this.users.ForEach(user =>
			{
				result.Int(user.Habbo.Id);
				result.String(user.Habbo.Username);
				result.String(user.Habbo.Motto);
				result.String(user.Habbo.Look);
				result.Int(user.VirtualId);
				result.Int(user.Position.X);
				result.Int(user.Position.Y);
				result.String(user.Position.Z.ToString());
				result.Int(0);
				result.Int(1);
				result.String(user.Habbo.Gender.ToLower());
				result.Int(0); //groupid
				result.Int(0); //groupwhat
				result.String(""); //Groupname?
				result.String("");
				result.Int(user.Habbo.AchievementScore); //achievement points
				result.Boolean(false); //idk

			});
			return result;
		}
	}
}
