using System;
using System.Diagnostics;
using System.IO;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Utilities
{
	public class Logging
	{
		public static void Alias(string text, string version)
		{
			Console.Title = text;
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("                      ______   _      _");
			Console.WriteLine("                     |  __  | | |    |_|  ______    ______");
			Console.WriteLine("                     | |__| | | |     _  |  __  |  |  ____|");
			Console.WriteLine("                     |  __  | | |__  | | | |__| |  |____  |");
			Console.WriteLine(@"                     |_|  |_| |____| |_| |_______\ |______|");
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("                                   Build: " + version);
			Console.WriteLine("                             https://DamienJolly.com");
			Console.WriteLine("");
			Console.WriteLine("");
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		public static void Warn(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("[Alias] [Warning] : " + message);
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		public static void Error(string information, Exception exception = null)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("[Alias] [Error] : " + information);
			Console.ForegroundColor = ConsoleColor.Gray;
			if (exception != null)
			{
				string currentText = File.ReadAllText(Constant.ExceptionFile);
				currentText += "\n\n";
				currentText += "Date: " + DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
				currentText += "\nEmulator Information: \"" + information;
				currentText += "\nInformation for developer: " + exception.ToString();
				File.WriteAllText(Constant.ExceptionFile, currentText);
			}
		}

		internal static void Command()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("[Alias] [Command] : ");
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		internal static void Message(ClientPacket message, string id)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("[Alias] [Message] [" + id + "] [" + message.Header + "] : " + message.ToString());
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		public static void Debug(string debugtext)
		{
			if (Debugger.IsAttached)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("[Alias] [Debug] : " + debugtext);
				Console.ForegroundColor = ConsoleColor.Gray;
			}
		}

		public static void Info(string information)
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("[Alias] [Information] : " + information);
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		public static void CreateExceptionFile()
		{
			if (!File.Exists(Constant.ExceptionFile))
			{
				File.Create(Constant.ExceptionFile);
			}
		}
	}
}
