using System.Data.Common;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Messenger
{
	public class MessengerFriend
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

		public string Motto
		{
			get; set;
		}

		public bool InRoom
		{
			get; set;
		}

		public int Relation
		{
			get; set;
		}

		public MessengerFriend(DbDataReader reader)
		{
			Id = reader.ReadData<int>("id");
			Username = reader.ReadData<string>("username");
			Look = reader.ReadData<string>("look");
			Motto = reader.ReadData<string>("motto");
			//todo: checkroom
			InRoom = false;
			Relation = reader.ReadData<int>("relation");
		}

		public MessengerFriend(int id, string usernmae, string look, string motto, bool inRoom)
		{
			Id = id;
			Username = usernmae;
			Look = look;
			Motto = motto;
			InRoom = inRoom;
			Relation = 0;
		}
	}
}
