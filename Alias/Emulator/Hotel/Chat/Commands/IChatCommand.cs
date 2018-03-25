using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Chat.Commands
{
	interface IChatCommand
	{
		string Name { get; }
		string Description { get; }
		bool IsAsynchronous { get; }
		void Handle(Session session, string[] args);
	}
}
