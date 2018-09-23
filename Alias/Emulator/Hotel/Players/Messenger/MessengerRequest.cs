using System.Data.Common;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Messenger
{
	public class MessengerRequest
	{
		public int Id
		{
			get; set;
		}

		public string Username
		{
			get; set;
		}

		public string Look
		{
			get; set;
		}

		public MessengerRequest(DbDataReader reader)
		{
			Id = reader.ReadData<int>("id");
			Username = reader.ReadData<string>("username");
			Look = reader.ReadData<string>("look");
		}

		public MessengerRequest(int id, string username, string look)
		{
			Id = id;
			Username = username;
			Look = look;
		}
	}
}
