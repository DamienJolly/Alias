using System;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Rooms.Users.Chat;
using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions.Wired
{
	public class WiredInteractionMessage : IWiredInteractor
	{
		private RoomItem _item
		{
			get; set;
		}

		private Room _room
		{
			get; set;
		}

		private int _tick
		{
			get; set;
		}

		private bool _active
		{
			get; set;
		} = false;

		private RoomUser _target
		{
			get; set;
		} = null;

		public void Serialize(ServerMessage message)
		{
			message.Boolean(false);
			message.Int(0);
			message.Int(0);
			message.Int(_item.ItemData.Id);
			message.Int(_item.Id);
			message.String("");
			message.Int(0);
			message.Int(0);
			message.Int(0);
			message.Int(0);
			message.Int(0); //invalids
		}

		public void LoadBox(RoomItem item)
		{
			_item = item;
			_room = item.Room;
			_tick = item.WiredData.Tick;
		}

		public void OnTrigger(RoomUser target)
		{
			if (!_active)
			{
				_tick = _item.WiredData.Tick;
				_target = target;
				_active = true;
			}
		}

		public void OnCycle()
		{
			if (_active)
			{
				if (_tick == 0)
				{
					if (_item.WiredData.Text != "")
					{
						if (_target != null)
						{
							_target.Habbo.Session.Send(new RoomUserChatComposer(_target.VirtualId, _item.WiredData.Text, 0, 34, ChatType.WHISPER));
						}
						else
						{
							_room.UserManager.Send(new RoomUserChatComposer(0, _item.WiredData.Text, 0, 34, ChatType.WHISPER));
						}
					}
					_active = false;
				}
				_tick--;
			}
		}
	}
}
