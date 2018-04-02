using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Rooms.Models
{
	public class RoomModelDatabase
	{
		public static List<RoomModel> Models()
		{
			List<RoomModel> result = new List<RoomModel>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `room_models`"))
				{
					while (Reader.Read())
					{
						RoomModel model = new RoomModel()
						{
							Name     = Reader.GetString("name"),
							MaxUsers = Reader.GetInt32("max_user"),
							MinRank  = Reader.GetInt32("min_rank"),
							Map      = Reader.GetString("map"),
							ClubOnly = Reader.GetBoolean("club_only"),
							Door     = Door.Parse(Reader.GetString("door"))
						};
						result.Add(model);
					}
				}
			}
			return result;
		}
	}
}
