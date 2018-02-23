using System;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users
{
	public sealed class Habbo : IDisposable
	{
		public int Id = 1;
		public string Username = "Damien";
		public string Mail = "DamienJolly@hotmail.com";
		public string Look = "";
		public string Gender = "M";
		public string Motto = "Hello world!";
		public int Rank = 1;
		public bool Disconnecting = false;

		public Habbo()
		{

		}

		public void Init()
		{
			//todo: Do some stuff
		}

		public void OnDisconnect()
		{
			this.Disconnecting = true;
		}

		public Session Session()
		{
			return SessionManager.SessionById(this.Id);
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
