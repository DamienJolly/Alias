using System.Collections.Generic;
using System.Drawing;
using Alias.Emulator.Hotel.Achievements;
using Alias.Emulator.Hotel.Rooms.Users.Chat;
using Alias.Emulator.Hotel.Rooms.Users.Chat.Commands;
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

		public bool isSitting
		{
			get; set;
		} = false;

		public RoomUser()
		{

		}

		public void MakeSit()
		{
			if (this.Actions.Has("mv"))
			{
				return;
			}

			if (!this.Actions.Has("sit"))
			{
				if ((this.Position.Rotation % 2) != 0)
				{
					this.Position.Rotation--;
				}

				this.Actions.Add("sit", 0.5 + "");
				this.isSitting = true;
			}
			else
			{
				this.Actions.Remove("sit");
				this.isSitting = false;
			}

			this.Room.UserManager.Send(new RoomUserStatusComposer(this));
		}

		public void LookAtPoint(int x, int y)
		{
			if (x == this.Position.X && y == this.Position.Y)
			{
				return;
			}

			if (this.Actions.Has("mv") || this.Actions.Has("lay"))
			{
				return;
			}

			this.Position.CalculateRotation(x, y, this.Actions.Has("sit"));

			this.Room.UserManager.Send(new RoomUserStatusComposer(this));
		}

		public void OnChat(string text, int colour, ChatType chatType, RoomUser target = null)
		{
			if (colour == 1 || colour == -1 || colour == 2)
			{
				colour = 0;
			}

			if (text.Length > 100)
			{
				text = text.Substring(0, 100);
			}

			if (text.StartsWith(":") && CommandHandler.Handle(this.Habbo.Session, text))
			{
				return;
			}

			RoomUserChatComposer packet = new RoomUserChatComposer(this.VirtualId, text, Expression(text), colour, chatType);

			if (target != null)
			{
				if (target != this)
				{
					target.Habbo.Session.Send(packet, false);
				}
				this.Habbo.Session.Send(packet);
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
