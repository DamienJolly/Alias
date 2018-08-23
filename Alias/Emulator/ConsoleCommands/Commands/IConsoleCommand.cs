namespace Alias.Emulator.ConsoleCommands.Commands
{
	public interface IConsoleCommand
	{
		string Name { get; }
		string Description { get; }
		string Arguments { get; }
		bool Handle(string[] args);
	}
}
