using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Utilities;
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
							IsCustom = Reader.GetBoolean("is_custom"),
							Door     = Door.Parse(Reader.GetString("door"))
						};
						result.Add(model);
					}
				}
			}
			return result;
		}

		public static void SaveModel(RoomModel model)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("name", model.Name);
				dbClient.AddParameter("users", model.MaxUsers);
				dbClient.AddParameter("rank", model.MinRank);
				dbClient.AddParameter("map", model.Map);
				dbClient.AddParameter("club", DatabaseBoolean.GetStringFromBool(model.ClubOnly));
				dbClient.AddParameter("custom", DatabaseBoolean.GetStringFromBool(model.IsCustom));
				dbClient.AddParameter("door", model.Door.X + "," + model.Door.Y + "," + model.Door.Rotation);
				dbClient.Query("REPLACE INTO `room_models` (`name`, `max_user`, `min_rank`, `map`, `club_only`, `is_custom`, `door`) VALUES (@name, @users, @rank, @map, @club, @custom, @door)");
			}
		}
	}
}
