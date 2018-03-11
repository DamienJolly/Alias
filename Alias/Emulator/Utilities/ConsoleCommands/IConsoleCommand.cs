namespace Alias.Emulator.Utilities.ConsoleCommands
{
    public interface IConsoleCommand
    {
		string Name { get; }
		string Description { get; }
		string Arguments { get; }
		bool Handle(string[] args);
	}
}
