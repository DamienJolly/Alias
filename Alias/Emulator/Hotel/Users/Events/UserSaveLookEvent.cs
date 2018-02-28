using Alias.Emulator.Hotel.Achievements;
using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Hotel.Users.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class UserSaveLookEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			string gender = message.String().ToUpper();
			if (gender != "M" && gender != "F")
			{
				return;
			}

			string look = message.String();

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

			AchievementManager.ProgressAchievement(session.Habbo, AchievementManager.GetAchievement("AvatarLooks"));
		}
	}
}
