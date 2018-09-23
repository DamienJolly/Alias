using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Hotel.Players.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Wardrobe.Events
{
	class UserSaveLookEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			if (!session.Player.Wardrobe.CanChangeFigure)
			{
				return;
			}

			string gender = message.PopString().ToUpper();
			if (gender != "M" && gender != "F")
			{
				return;
			}

			string look = message.PopString();

			if (look.Length == 0 || (look == session.Player.Look))
			{
				return;
			}

			session.Player.Look = session.Player.Entity.Look = look;
			session.Player.Gender = session.Player.Entity.Gender = gender;
			session.Send(new UpdateUserLookComposer(session.Player));

			if (session.Player.CurrentRoom != null)
			{
				session.Player.CurrentRoom.EntityManager.Send(new RoomUserDataComposer(session.Player.Entity));
			}

			session.Player.Wardrobe.SetFigureUpdated();
			await session.Player.Messenger.UpdateStatus();

			session.Player.Achievements.ProgressAchievement("AvatarLooks");
		}
	}
}
