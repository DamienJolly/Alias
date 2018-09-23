using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Rooms.Items.Interactions;
using Alias.Emulator.Hotel.Rooms.Items.Interactions.Wired;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Hotel.Players;
using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Rooms.Items
{
	class RoomItem
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
		}

		public int LimitedStack
		{
			get; set;
		}

		public int LimitedNumber
		{
			get; set;
		}

		public bool IsLimited
		{
			get
			{
				return LimitedNumber != 0;
			}
		}

		public RoomEntity InteractingUser
		{
			get; set;
		}

		public string ExtraData
		{
			get; set;
		}

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
				return "";
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
				if (Interactions.TryGetValue(ItemData.Interaction, out IItemInteractor interactor))
				{
					_interaction = interactor;
				}
			}

			return _interaction;
		}

		public IWiredInteractor GetWiredInteractor()
		{
			if (_wiredInteraction == null)
			{
				if (WiredInteractions.TryGetValue(ItemData.WiredInteraction, out IWiredInteractor wiredInteractor))
				{
					_wiredInteraction = wiredInteractor;
				}
				_wiredInteraction.LoadBox(this);
			}

			return _wiredInteraction;
		}

		public void ResetItem(bool inDatabase = true)
		{
			_interaction = null;
			_wiredInteraction = null;
			LimitedNumber = 0;
			LimitedStack = 0;
			if (inDatabase)
			{
				RoomItemDatabase.UpdateItem(this);
			}
		}

		private static IDictionary<ItemInteraction, IItemInteractor> Interactions { get; } = new Dictionary<ItemInteraction, IItemInteractor>
		{
			{ ItemInteraction.WIRED_CONDITION, new InteractionWired() },
			{ ItemInteraction.WIRED_EFFECT, new InteractionWired() },
			{ ItemInteraction.WIRED_TRIGGER, new InteractionWired() },

			{ ItemInteraction.DICE, new InteractionDice() },
			{ ItemInteraction.CRACKABLE, new InteractionCrackable() },
			{ ItemInteraction.EXCHANGE, new InteractionExchange() },
			{ ItemInteraction.VENDING_MACHINE, new InteractionVendingMachine() },
			{ ItemInteraction.TILE_EFFECT, new InteractionTileEffect() },
			{ ItemInteraction.TROPHY, new InteractionTrophy() },
			{ ItemInteraction.TELEPORT, new InteractionTeleport() },
			{ ItemInteraction.ROLLER, new InteractionRoller() },
			{ ItemInteraction.DEFAULT, new InteractionDefault() },
		};

		private static IDictionary<WiredInteraction, IWiredInteractor> WiredInteractions { get; } = new Dictionary<WiredInteraction, IWiredInteractor>
		{
			{ WiredInteraction.REPEATER, new WiredInteractionRepeater() },
			{ WiredInteraction.MESSAGE, new WiredInteractionMessage() },
			{ WiredInteraction.DEFAULT, new WiredInteractionDefault() },
		};
	}
}
