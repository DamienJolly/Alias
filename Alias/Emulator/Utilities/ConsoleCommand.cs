using System;
using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Utilities.ConsoleCommands;

namespace Alias.Emulator.Utilities
{
	public class ConsoleCommand
    {
		private static List<IConsoleCommand> _commands;

		public static void Initialize()
		{
			_commands = new List<IConsoleCommand>();
			RegisterCommands();

			while (true)
			{
				if (Console.ReadKey(true).Key == ConsoleKey.Enter)
				{
					Logging.Command();
					string text = Console.ReadLine().ToLower();
					if (Handle(text))
					{
						Logging.Info("Command was succesfully executed.");
					}
					else
					{
						Logging.Info("There was an error executing that command.");
					}
				}
			}
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
