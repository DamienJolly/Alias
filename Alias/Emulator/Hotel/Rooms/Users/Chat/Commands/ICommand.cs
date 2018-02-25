using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Chat.Commands
{
	public abstract class ICommand
	{
		public abstract string Name
		{
			get;
		}

		public abstract string Description
		{
			get;
		}

		public abstract string Arguments
		{
			get;
		}

		public abstract bool Handle(string[] args, Session session);
	}
}
