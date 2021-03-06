using Alias.Emulator.Utilities;

namespace Alias.Emulator.ConsoleCommands.Commands
{
	class TestCommand : IConsoleCommand
	{
		public string Name
		{
			get
			{
				return "Test";
			}
		}

		public string Description
		{
			get
			{
				return "This is a test command.";
			}
		}

		public string Arguments
		{
			get
			{
				return string.Empty;
			}
		}

		public bool Handle(string[] args)
		{
			Logging.Info("Hello there console! :D");
			return true;
		}
	}
}
