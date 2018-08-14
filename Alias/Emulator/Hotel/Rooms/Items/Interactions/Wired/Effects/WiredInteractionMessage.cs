using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions.Wired
{
	class WiredInteractionMessage : IWiredInteractor
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

		private RoomEntity _target
		{
			get; set;
		} = null;

		public void Serialize(ServerPacket message)
		{
			message.WriteBoolean(false);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(_item.ItemData.Id);
			message.WriteInteger(_item.Id);
			message.WriteString("");
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0); //invalids
		}

		public void LoadBox(RoomItem item)
		{
			_item = item;
			_room = item.Room;
			_tick = item.WiredData.Tick;
		}

		public void OnTrigger(RoomEntity target)
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

		}
	}
}
