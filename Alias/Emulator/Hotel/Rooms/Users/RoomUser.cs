using System.Collections.Generic;
using System.Drawing;
using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Rooms.Users
{
	public class RoomUser
	{
		public int VirtualId
		{
			get; set;
		}

		public Room Room
		{
			get; set;
		}

		public Habbo Habbo
		{
			get; set;
		}

		public UserPosition Position
		{
			get; set;
		}

		public LinkedList<Point> Path
		{
			get; set;
		}

		public UserPosition TargetPosition
		{
			get; set;
		}

		public UserActions Actions
		{
			get; set;
		} = new UserActions();

		public RoomUser()
		{

		}

		public void OnChat(string text, int colour, ChatType chatType)
		{
			string targetName = "";
			if (chatType == ChatType.WHISPER)
			{
				targetName = text.Split(' ')[0];
				text = text.Substring(text.Split(' ')[0].Length + 1);
			}

			if (colour == 1 || colour == -1 || colour == 2)
			{
				colour = 0;
			}

			if (text.Length > 100)
			{
				text = text.Substring(0, 100);
			}

			RoomUserChatComposer packet = new RoomUserChatComposer(this.VirtualId, text, Expression(text), colour, chatType);

			if (chatType == ChatType.WHISPER)
			{
				RoomUser target = this.Room.UserManager.UserByName(targetName);
				if (target != null && target.Habbo != this.Habbo)
				{
					target.Habbo.Session().Send(packet, false);
					this.Habbo.Session().Send(packet);
				}
			}
			else
			{
				this.Room.UserManager.Send(packet);
			}
		}

		private int Expression(string text)
		{
			int face = 0;
			if (text.Contains(":D") || text.Contains(":)") || text.Contains("c:") || text.Contains(":-)") || text.Contains("=D"))
			{
				face = 1;
			}
			if (text.Contains(":(") || text.Contains(":c") || text.Contains(":-(") || text.Contains("=("))
			{
				face = 4;
			}
			if (text.Contains(":@") || text.Contains(":-@") || text.Contains("=@"))
			{
				face = 2;
			}
			if (text.ToLower().Contains(":o") || text.Contains(":0") || text.ToLower().Contains(":-o") || text.Contains(":-0") || text.ToLower().Contains("=o") || text.Contains("=0"))
			{
				face = 3;
			}
			return face;
		}

		public void Dispose()
		{
			this.Room = null;
			this.Habbo = null;
			//todo:
		}
	}
}
