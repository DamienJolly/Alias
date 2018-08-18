using System.Drawing;
using System.Linq;
using Alias.Emulator.Hotel.Chat.WordFilter;
using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Hotel.Rooms.Entities.Chat;
using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Hotel.Rooms.Mapping;

namespace Alias.Emulator.Hotel.Rooms.Entities
{
	class RoomEntity : RoomEntityData
	{
		public int VirtualId
		{
			get; set;
		}

		public Room Room
		{
			get; set;
		}

		public bool Disposing
		{
			get; set;
		} = false;

		public void OnCycle()
		{
			if (this.HandItem > 0)
			{
				this.HandItemTick--;
				if (this.HandItemTick <= 0)
				{
					SetHandItem(0);
				}
			}
			this.EntityType.OnCycle(this);
			this.WalkCycle();
		}

		public void WalkCycle()
		{
			try
			{
				if (this.TargetPosition != null && this.Path != null && this.Path.Count > 0)
				{
					if (this.Actions.Has("mv"))
					{
						this.Actions.Remove("mv");
					}
					if (this.Actions.Has("sit"))
					{
						this.Actions.Remove("sit");
					}
					if (this.Actions.Has("lay"))
					{
						this.Actions.Remove("lay");
					}

					RoomTile oldTile = this.Room.Mapping.Tiles[this.Position.X, this.Position.Y];
					oldTile.RemoveEntity(this);

					Point p = this.Path.First();
					RoomTile tile = this.Room.Mapping.Tiles[p.X, p.Y];
					tile.AddEntity(this);

					double height = 0.0;

					if (oldTile.TopItem != null)
					{
						// walk off
						if (tile.TopItem == null || tile.TopItem != oldTile.TopItem)
						{
							oldTile.TopItem.GetInteractor().OnUserWalkOff(this, this.Room, oldTile.TopItem);
						}
					}

					if (tile.TopItem != null)
					{
						// walk on
						if (oldTile.TopItem == null || oldTile.TopItem != tile.TopItem)
						{
							tile.TopItem.GetInteractor().OnUserWalkOn(this, this.Room, tile.TopItem);
						}

						height += tile.TopItem.Position.Z;
						if (!tile.TopItem.ItemData.CanSit && !tile.TopItem.ItemData.CanLay)
						{
							// todo: multiheight furni
							height += tile.TopItem.ItemData.Height;
						}
					}
					else
					{
						height += tile.Position.Z;
					}

					this.Actions.Add("mv", p.X + "," + p.Y + "," + height);
					this.Position.Rotation = this.Room.PathFinder.Rotation(this.Position.X, this.Position.Y, p.X, p.Y);
					this.Position.HeadRotation = this.Position.Rotation;
					this.Path.RemoveFirst();
					this.Room.EntityManager.Send(new RoomUserStatusComposer(this));
					this.Position.X = p.X;
					this.Position.Y = p.Y;
					this.Position.Z = height;

					if (this.Path.Count() != 0)
					{
						this.Path = this.Room.PathFinder.Path(this);
					}
				}
				else
				{

					bool update = false;
					if (this.Actions.Has("mv"))
					{
						this.Actions.Remove("mv");
						update = true;
					}
					RoomTile tile = this.Room.Mapping.Tiles[this.TargetPosition.X, this.TargetPosition.Y];

					if (tile.TopItem != null && tile.TopItem.ItemData.CanSit)
					{
						this.Actions.Add("sit", tile.TopItem.ItemData.Height.ToString());
						this.Position.Rotation = tile.TopItem.Position.Rotation;
						this.Position.HeadRotation = this.Position.Rotation;
						this.Position.Z = tile.TopItem.ItemData.Height + tile.TopItem.Position.Z;
						this.isSitting = false;
						update = true;
					}
					else if (tile.TopItem != null && tile.TopItem.ItemData.CanLay)
					{
						this.Actions.Add("lay", tile.TopItem.ItemData.Height.ToString());
						this.Position.Rotation = tile.TopItem.Position.Rotation;
						this.Position.HeadRotation = this.Position.Rotation;
						this.Position.Z = tile.TopItem.ItemData.Height + tile.TopItem.Position.Z;
						this.isSitting = false;
						update = true;
					}
					else
					{
						if (!this.isSitting && this.Actions.Has("sit"))
						{
							this.Actions.Remove("sit");
							this.Position.Z = tile.Position.Z;
							update = true;
						}

						if (this.Actions.Has("lay"))
						{
							this.Actions.Remove("lay");
							this.Position.Z = tile.Position.Z;
							update = true;
						}
					}

					if (update)
					{
						this.Room.EntityManager.Send(new RoomUserStatusComposer(this));
					}

					if ((this.Room.Model.Door.X == this.Position.X && this.Room.Model.Door.Y == this.Position.Y) &&
						this.Type == RoomEntityType.Player)
					{
						//this.Habbo.Session.Send(new HotelViewComposer());
						//this.Room.EntityManager.OnUserLeave(this);
					}
				}
			}
			catch { }
		}

		public void SetHandItem(int itemId)
		{
			this.HandItem = itemId;

			if (itemId > 0)
			{
				this.HandItemTick = 240;
			}
			else
			{
				this.HandItemTick = 0;
			}

			Room.EntityManager.Send(new RoomUserHandItemComposer(this));
		}

		public void SetEffectId(int effectId)
		{
			this.EffectId = effectId;
			Room.EntityManager.Send(new RoomUserEffectComposer(this));
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

			this.Room.EntityManager.Send(new RoomUserStatusComposer(this));
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

			this.Room.EntityManager.Send(new RoomUserStatusComposer(this));
		}

		public void OnChat(string text, int colour, ChatType chatType, RoomEntity target = null)
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
				if (this.Type == RoomEntityType.Player)
				{
					Alias.Server.ModerationManager.QuickTicket(this.Habbo, "User said a banned word");
				}
				return;
			}

			text = Alias.Server.ChatManager.GetFilter().Filter(text);

			if (this.Type == RoomEntityType.Player && (text.StartsWith(":") && Alias.Server.ChatManager.GetCommands().Parse(this.Habbo.Session, text)))
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
				this.Room.EntityManager.Send(packet);
			}

			if (this.Type == RoomEntityType.Player)
			{
				WordFilterDatabase.StoreMessage(this.Habbo.Id, this.Room.Id, text, ChatTypeToInt(chatType), target != null ? target.Habbo.Id : 0);
			}
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
