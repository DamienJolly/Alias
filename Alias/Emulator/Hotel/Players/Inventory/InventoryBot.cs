using System.Data.Common;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Inventory
{
	public class InventoryBot
	{
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public string Motto
		{
			get; set;
		}

		public string Look
		{
			get; set;
		}

		public string Gender
		{
			get; set;
		}

		public int DanceId
		{
			get; set;
		}

		public int EffectId
		{
			get; set;
		}

		public bool CanWalk
		{
			get; set;
		}

		public int RoomId
		{
			get; set;
		}

		public InventoryBot(DbDataReader reader)
		{
			Id = reader.ReadData<int>("id");
			Name = reader.ReadData<string>("name");
			Motto = reader.ReadData<string>("motto");
			Look = reader.ReadData<string>("look");
			Gender = reader.ReadData<string>("gender");
			DanceId = reader.ReadData<int>("dance_id");
			EffectId = reader.ReadData<int>("effect_id");
			CanWalk = reader.ReadBool("can_walk");
			RoomId = 0;
		}

		public InventoryBot(int id, string name, string motto, string look, string gender, int danceId = 0, int effectId = 0, bool canWalk = true)
		{
			Id = id;
			Name = name;
			Motto = motto;
			Look = look;
			Gender = gender;
			DanceId = danceId;
			EffectId = effectId;
			CanWalk = canWalk;
			RoomId = 0;
		}
	}
}
