using System;
using System.Collections.Generic;
using System.Text;
using Alias.Emulator.Hotel.Users.Handshake;

namespace Alias.Emulator.Hotel.Users
{
	public class UserEvents
	{
		public static void Register()
		{
			HandshakeEvents.Register();
		}
	}
}
