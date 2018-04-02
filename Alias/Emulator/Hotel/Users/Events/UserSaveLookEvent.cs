using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Hotel.Users.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class UserSaveLookEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			string gender = message.PopString().ToUpper();
			if (gender != "M" && gender != "F")
			{
				return;
			}

			string look = message.PopString();

			session.Habbo.Look = look;
			session.Habbo.Gender = gender;
			session.Send(new UpdateUserLookComposer(session.Habbo));

			if (session.Habbo.Messenger != null)
			{
				session.Habbo.Messenger.UpdateStatus(true);
			}

			if (session.Habbo.CurrentRoom != null)
			{
				session.Habbo.CurrentRoom.UserManager.Send(new RoomUserDataComposer(session.Habbo));
			}

			Alias.Server.AchievementManager.ProgressAchievement(session.Habbo, Alias.Server.AchievementManager.GetAchievement("AvatarLooks"));
		}
	}
}
