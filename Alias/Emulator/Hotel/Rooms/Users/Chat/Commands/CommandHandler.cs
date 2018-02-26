using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms.Users.Chat.Commands.Users;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Chat.Commands
{
	public class CommandHandler
	{
		private static List<ICommand> Commands
		{
			get; set;
		} = new List<ICommand>();

		public static void Initialize()
		{
			CommandHandler.Commands.Add(new AboutCommand());
		}

		public static bool Handle(Session session, string text)
		{
			if (text.StartsWith(":") && CommandHandler.Commands.Where(cmd => cmd.Name.Equals(text.Substring(1).Split(' ')[0])).Count() > 0)
			{
				ICommand command = CommandHandler.Commands.Where(cmd => cmd.Name.Equals(text.Substring(1).Split(' ')[0])).First();
				string[] parameters;
				if (text.Length <= 2 + command.Name.Length)
				{
					parameters = new string[0];
				}
				else
				{
					parameters = text.Substring(2 + command.Name.Length).Split(' ');
				}
				return command.Handle(parameters, session);
			}
			return false;
		}
	}
}
