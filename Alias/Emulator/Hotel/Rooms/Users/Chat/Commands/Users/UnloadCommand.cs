using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Chat.Commands.Users
{
	public class UnloadCommand : ICommand
	{
		public override string Name
		{
			get
			{
				return "unload";
			}
		}

		public override string Description
		{
			get
			{
				return "Unloads the current room you're in.";
			}
		}

		public override string Arguments
		{
			get
			{
				return string.Empty;
			}
		}

		public override bool Handle(string[] args, Session session)
		{
			if (session.Habbo().CurrentRoom == null)
			{
				return false;
			}

			if (session.Habbo().CurrentRoom.RoomRights.IsOwner(session.Habbo()))
			{
				session.Habbo().CurrentRoom.Unload();
			}
			return true;
		}
	}
}
