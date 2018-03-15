using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions.Wired
{
	public class WiredInteractionRepeater : IWiredInteractor
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
			message.Int(5);
			message.Int(0);
			message.Int(_item.ItemData.Id);
			message.Int(_item.Id);
			message.String("");
			message.Int(1);
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
					foreach (RoomItem wItem in _room.DynamicModel.GetWiredEffects(_item.Position.X, _item.Position.Y))
					{
						wItem.GetWiredInteractor().OnTrigger(_target);
					}
					_active = false;
				}
				_tick--;
			}
		}
	}
}
