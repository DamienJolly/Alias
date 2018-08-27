using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Hotel.Users.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Wardrobe.Events
{
	class UserSaveLookEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.Wardrobe.CanChangeFigure)
			{
				return;
			}

			string gender = message.PopString().ToUpper();
			if (gender != "M" && gender != "F")
			{
				return;
			}

			string look = message.PopString();

			if (look.Length == 0 || (look == session.Habbo.Look))
			{
				return;
			}

			if (!FigureValidation.Validate(look, gender))
			{
				return;
			}

			session.Habbo.Look = session.Habbo.Entity.Look = look;
			session.Habbo.Gender = session.Habbo.Entity.Gender = gender;
			session.Send(new UpdateUserLookComposer(session.Habbo));

			if (session.Habbo.CurrentRoom != null)
			{
				session.Habbo.CurrentRoom.EntityManager.Send(new RoomUserDataComposer(session.Habbo.Entity));
			}

			session.Habbo.Wardrobe.SetFigureUpdated();
			session.Habbo.Messenger.UpdateStatus(true);

			session.Habbo.Achievements.ProgressAchievement("AvatarLooks");
		}
	}
}
