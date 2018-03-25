using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.States;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Rooms
{
	public class RoomData
	{
		public int Id
		{
			get; set;
		} = 0;

		public string Name
		{
			get; set;
		} = "Room";

		public int OwnerId
		{
			get; set;
		} = 0;

		public string OwnerName
		{
			get
			{
				return (string)UserDatabase.Variable(this.OwnerId, "Username");
			}
		}

		public RoomDoorState DoorState
		{
			get; set;
		} = RoomDoorState.OPEN;

		public int UsersNow
		{
			get
			{
				return (int)Alias.GetServer().GetRoomManager().Room(this.Id).UserManager.UserCount;
			}
		}

		public int MaxUsers
		{
			get; set;
		} = 70;

		public string Description
		{
			get; set;
		} = "Description";

		public RoomTradeState TradeState
		{
			get; set;
		} = RoomTradeState.ALLOWED;

		public List<int> Likes
		{
			get; set;
		} = new List<int>();

		public int Rankings
		{
			get; set;
		} = 0;

		public int Category
		{
			get; set;
		} = 0;

		public List<string> Tags
		{
			get; set;
		} = new List<string>();

		public int EnumType
		{
			get
			{
				int type = 8;
				if (this.Image.Length > 0)
				{
					type += 1;
				}
				return type;
				/*  int RoomType = 0;
			   if (Data.Group != null)
				   RoomType += 2;
			   if (Data.Promotion != null)
				   RoomType += 4;
			   if (Data.Type == "private")
				   RoomType += 8;
			   if (Data.AllowPets == 1)
				   RoomType += 16;
				   */
			}
		}

		public string Image
		{
			get; set;
		} = "";

		public string Password
		{
			get; set;
		} = "";

		public string ModelName
		{
			get; set;

		} = "model_a";

		public bool WalkDiagonal
		{
			get; set;
		} = true;

		public string Wallpaper
		{
			get; set;
		} = "0.0";

		public string Floor
		{
			get; set;
		} = "0.0";

		public string Landscape
		{
			get; set;
		} = "0.0";

		public RoomSettings Settings
		{
			get; set;
		} = new RoomSettings();

		public RoomData()
		{

		}
	}
}
