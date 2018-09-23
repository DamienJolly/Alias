using System.Data.Common;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Inventory
{
	public class InventoryPet
	{
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public int Type
		{
			get; set;
		}

		public int Race
		{
			get; set;
		}

		public string Colour
		{
			get; set;
		}

		public int RoomId
		{
			get; set;
		}

		public InventoryPet(DbDataReader reader)
		{
			Id = reader.ReadData<int>("id");
			Name = reader.ReadData<string>("name");
			Type = reader.ReadData<int>("type");
			Race = reader.ReadData<int>("race");
			Colour = reader.ReadData<string>("colour");
			RoomId = 0;
		}

		public InventoryPet(int id, string name, int type, int race, string colour)
		{
			Id = id;
			Name = name;
			Type = type;
			Race = race;
			Colour = colour;
			RoomId = 0;
		}
	}
}
