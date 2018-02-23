using System;
using Alias.Emulator.Network;
using Alias.Emulator.Utilities;

namespace Alias.Emulator
{
    class Environment
    {
		public static void Initialize()
		{
			SocketServer.Initialize();
			while (true) Logging.ReadLine();
		}
	}
}
