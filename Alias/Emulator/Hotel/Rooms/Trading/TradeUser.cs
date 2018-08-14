using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Hotel.Users.Inventory;

namespace Alias.Emulator.Hotel.Rooms.Trading
{
    class TradeUser
    {
		public RoomEntity User
		{
			get; set;
		}

		public bool Accepted
		{
			get; set;
		} = false;

		public bool Confirmed
		{
			get; set;
		} = false;

		public List<InventoryItem> OfferedItems
		{
			get; set;
		} = new List<InventoryItem>();
	}
}
