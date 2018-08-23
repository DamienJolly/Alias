using System;
using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.ConsoleCommands.Commands;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.ConsoleCommands
{
    class ConsoleCommandManager
    {
		private static List<IConsoleCommand> _commands;

		public static void Initialize()
		{
			_commands = new List<IConsoleCommand>();
			RegisterCommands();
		}

		public static bool Handle(string text)
		{
			IConsoleCommand command = _commands.Where(cmd => cmd.Name.ToLower().Equals(text.Split(' ')[0])).FirstOrDefault();
			if (command == null)
			{
				return false;
			}

			string[] parameters;
			if (text.Length <= 2 + command.Name.Length)
			{
				parameters = new string[0];
			}
			else
			{
				parameters = text.Substring(2 + command.Name.Length).Split(' ');
			}

			return command.Handle(parameters);
		}

		public static void RegisterCommands()
		{
			_commands.Add(new TestCommand());
		}
	}
}
