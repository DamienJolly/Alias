using System;
using Alias.Emulator.Network;

namespace Alias.Emulator
{
    class Environment
    {
		public static void Initialize()
		{
			SocketServer.Initialize();
			while (true) Console.ReadLine();
		}
	}
}
