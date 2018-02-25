using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Rooms.Models
{
	public class RoomModelDatabase
	{
		public static List<RoomModel> Models()
		{
			List<RoomModel> result = new List<RoomModel>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `room_models`").Rows)
				{
					RoomModel model = new RoomModel();
					model.Name = (string)row["name"];
					model.MaxUsers = (int)row["max_user"];
					model.MinRank = (int)row["min_rank"];
					model.Map = (string)row["map"];
					model.ClubOnly = AliasEnvironment.ToBool((string)row["club_only"]);
					model.Door = Door.Parse((string)row["door"]);
					result.Add(model);
					row.Delete();
				}
			}
			return result;
		}
	}
}
