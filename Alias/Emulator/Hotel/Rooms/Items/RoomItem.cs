using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Rooms.Items.Interactions;
using Alias.Emulator.Hotel.Rooms.Items.Interactions.Wired;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Rooms.Items
{
	public class RoomItem
	{
		public int Id
		{
			get; set;
		}

		public ItemPosition Position
		{
			get; set;
		}

		public Room Room
		{
			get; set;
		}

		public int Owner
		{
			get; set;
		}

		public int Mode
		{
			get; set;
		} = 0;

		public ItemData ItemData
		{
			get; set;
		}

		// todo: add to database
		public WiredData WiredData
		{
			get; set;
		} = new WiredData();

		public string Username
		{
			get
			{
				return (string)UserDatabase.Variable(this.Owner, "Username");
			}
		}

		private IItemInteractor _interaction
		{
			get; set;
		}

		private IWiredInteractor _wiredInteraction
		{
			get; set;
		}

		public IItemInteractor GetInteractor()
		{
			if (_interaction == null)
			{
				switch (this.ItemData.Interaction)
				{
					case ItemInteraction.WIRED_TRIGGER: case ItemInteraction.WIRED_EFFECT: case ItemInteraction.WIRED_CONDITION: _interaction = new InteractionWired(); break;
					case ItemInteraction.DEFAULT: default: _interaction = new InteractionDefault(); break;
				}
			}

			return _interaction;
		}

		public IWiredInteractor GetWiredInteractor()
		{
			if (_wiredInteraction == null)
			{
				switch (this.ItemData.WiredInteraction)
				{
					case WiredInteraction.REPEATER: _wiredInteraction = new WiredInteractionRepeater(); break;
					case WiredInteraction.MESSAGE: _wiredInteraction = new WiredInteractionMessage(); break;
					case WiredInteraction.DEFAULT: default: _wiredInteraction = new WiredInteractionDefault(); break;
				}
				_wiredInteraction.LoadBox(this);
			}

			return _wiredInteraction;
		}
	}
}
