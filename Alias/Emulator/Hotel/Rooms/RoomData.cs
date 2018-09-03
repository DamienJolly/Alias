using System.Collections.Generic;
using Alias.Emulator.Hotel.Groups;
using Alias.Emulator.Hotel.Rooms.States;

namespace Alias.Emulator.Hotel.Rooms
{
	class RoomData
	{
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public int OwnerId
		{
			get; set;
		}

		public string OwnerName
		{
			get; set;
		}

		public Group Group
		{
			get; set;
		}

		public RoomDoorState DoorState
		{
			get; set;
		}

		public int UsersNow
		{
			get; set;
		} = 0;

		public int MaxUsers
		{
			get; set;
		}

		public string Description
		{
			get; set;
		}

		public RoomTradeState TradeState
		{
			get; set;
		}

		public List<int> Likes
		{
			get; set;
		}

		public int Rankings
		{
			get; set;
		}

		public int Category
		{
			get; set;
		}

		public List<string> Tags
		{
			get; set;
		}

		public int EnumType
		{
			get
			{
				int type = 0;
				if (this.Image.Length > 0)
				{
					type += 1;
				}
				if (this.Group != null)
				{
					type += 2;
				}
				if (this.Settings.AllowPets)
				{
					type += 16;
				}
				return type;
				/*  int RoomType = 0;
			   if (Data.Group != null)
				   RoomType += 2;
			   if (Data.Promotion != null)
				   RoomType += 4;
			   if (Data.Type == "private")
				   RoomType += 8;
				   */
			}
		}

		public string Image
		{
			get; set;
		}

		public string Password
		{
			get; set;
		}

		public string ModelName
		{
			get; set;

		}

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

		public int RollerSpeed
		{
			get; set;
		} = 1;

		public RoomSettings Settings
		{
			get; set;
		}
	}
}
