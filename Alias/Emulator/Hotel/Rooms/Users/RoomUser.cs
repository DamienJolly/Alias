using Alias.Emulator.Hotel.Chat.WordFilter;
using Alias.Emulator.Hotel.Rooms.Users.Chat;
using Alias.Emulator.Hotel.Rooms.Users.Composers;

namespace Alias.Emulator.Hotel.Rooms.Users
{
	public class RoomUser : RoomUserData
	{
		public int VirtualId
		{
			get; set;
		}

		public Room Room
		{
			get; set;
		}
		
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
			
			if (Alias.Server.ChatManager.GetFilter().CheckBanned(text))
			{
				Alias.Server.ModerationManager.QuickTicket(this.Habbo, "User said a banned word");
				return;
			}

			text = Alias.Server.ChatManager.GetFilter().Filter(text);

			if (text.StartsWith(":") && Alias.Server.ChatManager.GetCommands().Parse(this.Habbo.Session, text))
			{
				return;
			}

			RoomUserChatComposer packet = new RoomUserChatComposer(this.VirtualId, text, Alias.Server.ChatManager.GetEmotions().GetEmotionsForText(text), colour, chatType);

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
			WordFilterDatabase.StoreMessage(this.Habbo.Id, this.Room.Id, text, ChatTypeToInt(chatType), target != null ? target.Habbo.Id : 0);
		}

		private int ChatTypeToInt(ChatType type)
		{
			switch (type)
			{
				case ChatType.CHAT:
					return 0;
				case ChatType.SHOUT:
					return 1;
				case ChatType.WHISPER:
					return 2;
				default:
					return 0;
			}
		}

		public void Dispose()
		{
			this.Room = null;
			this.Habbo = null;
			//todo:
		}
	}
}
