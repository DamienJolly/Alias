using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Chat.Commands.Users;
using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Chat.Commands
{
	sealed class CommandManager
	{
		/// <summary>
		/// Command Prefix only applies to custom commands.
		/// </summary>
		private string _prefix = ":";

		/// <summary>
		/// Commands registered for use.
		/// </summary>
		private readonly List<IChatCommand> _commands;

		/// <summary>
		/// The default initializer for the CommandManager
		/// </summary>
		public CommandManager(string Prefix)
		{
			this._commands = new List<IChatCommand>();
			this._prefix = Prefix;

			RegisterUser();
			RegisterStaff();
		}

		/// <summary>
		/// Request the text to parse and check for commands that need to be executed.
		/// </summary>
		/// <param name="session">Session calling this method.</param>
		/// <param name="message">The message to parse.</param>
		/// <returns>True if parsed or false if not.</returns>
		public bool Parse(Session session, string message)
		{
			if (!message.StartsWith(this._prefix))
			{
				return false;
			}

			message = message.Substring(1, message.Length - 1);
			string[] MsgSplit = message.Split(' ');

			if (MsgSplit.Length == 0)
			{
				return false;
			}

			IChatCommand command = this._commands.Where(cmd => cmd.Name.Equals(MsgSplit[0].ToLower())).FirstOrDefault();
			if (command == null)
			{
				return false;
			}

			if (!session.Habbo.HasPermission("cmd_" + command.Name))
			{
				return false;
			}

			command.Handle(session, MsgSplit.Skip(1).ToArray());
			session.Habbo.CurrentRoom.EntityManager.Send(new RoomUserTypingComposer(session.Habbo.Entity, false));
			return true;
		}

		/// <summary>
		/// Registers the user set of commands.
		/// </summary>
		private void RegisterUser()
		{
			this._commands.Add(new AboutCommand());
			this._commands.Add(new UnloadCommand());
		}

		/// <summary>
		/// Registers the staff set of commands.
		/// </summary>
		private void RegisterStaff()
		{

		}
	}
}
