using System.Collections.Generic;
using System.Drawing;
using Alias.Emulator.Hotel.Rooms.Entities.Types;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Rooms.Entities
{
    class RoomEntityData
    {
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public string Motto
		{
			get; set;
		}

		public string Look
		{
			get; set;
		}

		public string Gender
		{
			get; set;
		}

		public int HandItem
		{
			get; set;
		} = 0;

		public int HandItemTick
		{
			get; set;
		} = 0;

		public int EffectId
		{
			get; set;
		} = 0;

		public int DanceId
		{
			get; set;
		} = 0;

		public int OwnerId
		{
			get; set;
		} = 0;

		public bool CanWalk
		{
			get; set;
		} = true;

		public Habbo Habbo
		{
			get; set;
		} = null;

		public RoomEntityType Type
		{
			get; set;
		} = RoomEntityType.Player;

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

		private IEntityType _entityType
		{
			get; set;
		} = null;

		public IEntityType EntityType
		{
			get
			{
				if (_entityType == null)
				{
					switch (this.Type)
					{
						case RoomEntityType.Player: default: _entityType = new EntityPlayer(); break;
						case RoomEntityType.Bot: _entityType = new EntityGenericBot(); break;
					}
				}

				return _entityType;
			}
		}
	}
}
