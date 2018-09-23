using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Chat.Commands.Users
{
	class UnloadCommand : IChatCommand
	{
		public string Name => "unload";

		public string Description => "Unloads the current room you're in.";

		public bool IsAsynchronous => false;

		public void Handle(Session session, string[] args)
		{
			if (session.Player.CurrentRoom == null)
			{
				return;
			}

			if (session.Player.CurrentRoom.RoomData.OwnerId == session.Player.Id)
			{
				session.Player.CurrentRoom.Disposing = true;
			}
		}
	}
}
