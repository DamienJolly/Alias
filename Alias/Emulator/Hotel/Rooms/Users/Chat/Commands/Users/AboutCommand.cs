using System.Diagnostics;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Chat.Commands.Users
{
	public class AboutCommand : ICommand
	{
		public override string Name
		{
			get
			{
				return "about";
			}
		}

		public override string Description
		{
			get
			{
				return "Shows important information about the hotel.";
			}
		}

		public override string Arguments
		{
			get
			{
				return string.Empty;
			}
		}

		public override bool Handle(string[] args, Session session)
		{
			string message = "";
			message += "<b>Hotel Statistics</b> \r"
					+ "- Online Users: " + SessionManager.OnlineUsers() + "\r"
					+ "- Active Rooms: " + RoomManager.ReadLoadedRooms().Count + "\r"
					+ "- Furni: " + 0 + "\r"
					+ "\n"
					+ "<b>Server Statistics</b> \r"
					+ "- Uptime: " + AliasEnvironment.GetUpTime() + "\r"
					+ "- RAM Usage: " + Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024) + "MBs \r"
					+ "- Build:  " + Constant.ProductionVersion + " \r"
					+ "\n"
					+ "Thank you for choosing <b> Alias</b> \r"
					+ "\n"
					+ "For more Information about <b>Alias</b> please visit the git: \r"
					+ "<b>https://github.com/DamienJolly/Alias</b> \r"
					+ "\n"
					+ "- Damien Jolly";

			session.Habbo.Notification(message, true);
			return true;
		}
	}
}
