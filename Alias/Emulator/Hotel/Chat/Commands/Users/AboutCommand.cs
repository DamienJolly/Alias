using System;
using System.Diagnostics;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Chat.Commands.Users
{
	class AboutCommand : IChatCommand
	{
		public string Name => "about";

		public string Description => "Shows important information about the hotel.";

		public bool IsAsynchronous => false;

		public void Handle(Session session, string[] args)
		{
			TimeSpan Uptime = DateTime.Now - Alias.ServerStarted;
			string message = "";
			message += "<b>Hotel Statistics</b> \r"
					+ "- Online Users: " + Alias.Server.SocketServer.SessionManager.OnlineUsers() + "\r"
					+ "- Active Rooms: " + Alias.Server.RoomManager.LoadedRooms.Count + "\r"
					+ "- Furni: " + 0 + "\r"
					+ "\n"
					+ "<b>Server Statistics</b> \r"
					+ "- Uptime: " + Uptime.Days + " day(s), " + Uptime.Hours + " hour(s) and " + Uptime.Minutes + " minute(s)" + "\r"
					+ "- RAM Usage: " + Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024) + "MBs \r"
					+ "- Build:  " + Alias.ProductionVersion + " \r"
					+ "\n"
					+ "Thank you for choosing <b> Alias</b> \r"
					+ "\n"
					+ "For more Information about <b>Alias</b> please visit the git: \r"
					+ "<b>https://github.com/DamienJolly/Alias</b> \r"
					+ "\n"
					+ "- Damien Jolly";

			session.Habbo.Notification(message, true);
		}
	}
}
