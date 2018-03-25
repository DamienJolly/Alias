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
			if (session.Habbo.CurrentRoom == null)
			{
				return;
			}

			if (session.Habbo.CurrentRoom.RoomData.OwnerId == session.Habbo.Id)
			{
				session.Habbo.CurrentRoom.Unload();
			}
		}
	}
}
