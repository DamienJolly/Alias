using System.Data.Common;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Items;

namespace Alias.Emulator.Hotel.Players.Inventory
{
	public class InventoryItem
	{
		public int Id
		{
			get; set;
		}

		public int LimitedNumber
		{
			get; set;
		}

		public int LimitedStack
		{
			get; set;
		}

		public int UserId
		{
			get; set;
		}
		
		public int RoomId
		{
			get; set;
		}

		public ItemData ItemData
		{
			get; set;
		}

		public string ExtraData
		{
			get; set;
		}

		public int Mode
		{
			get; set;
		}

		public InventoryItem(DbDataReader reader)
		{
			Id = reader.ReadData<int>("id");
			LimitedNumber = reader.ReadData<int>("limited_number");
			LimitedStack = reader.ReadData<int>("limited_stack");
			ItemData = Alias.Server.ItemManager.GetItemData(reader.ReadData<int>("base_id"));
			UserId = reader.ReadData<int>("user_id");
			ExtraData = reader.ReadData<string>("extradata");
			Mode = reader.ReadData<int>("mode");
			RoomId = 0;
		}

		public InventoryItem(int id, int limitedNumber, int limitedStack, int baseItem, int userId, string extraData)
		{
			Id = id;
			LimitedNumber = limitedNumber;
			LimitedStack = limitedStack;
			ItemData = Alias.Server.ItemManager.GetItemData(baseItem);
			UserId = userId;
			ExtraData = extraData;
			Mode = 0;
			RoomId = 0;
		}

		public bool IsLimited => this.LimitedStack > 0;
	}
}
