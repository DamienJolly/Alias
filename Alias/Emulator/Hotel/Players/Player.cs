using System.Data.Common;
using System.Threading.Tasks;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Players.Achievements;
using Alias.Emulator.Hotel.Players.Badges;
using Alias.Emulator.Hotel.Players.BonusRares;
using Alias.Emulator.Hotel.Players.Crafting;
using Alias.Emulator.Hotel.Players.Currency;
using Alias.Emulator.Hotel.Players.Inventory;
using Alias.Emulator.Hotel.Players.Messenger;
using Alias.Emulator.Hotel.Players.Navigator;
using Alias.Emulator.Hotel.Players.Wardrobe;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players
{
	internal class Player : PlayerData
	{
		public bool IsDisconnecting { get; set; } = false;
		public Room CurrentRoom { get; set; } = null;
		public RoomEntity Entity { get; set; } = null;
		public Session Session { get; set; } = null;

		public PlayerSettings Settings { get; set; }
		public AchievementComponent Achievements { get; set; }
		public BadgeComponent Badges { get; set; }
		public BonusRareComponent BonusRare { get; set; }
		public CraftingComponent Crafting { get; set; }
		public CurrencyComponent Currency { get; set; }
		public InventoryComponent Inventory { get; set; }
		public MessengerComponent Messenger { get; set; }
		public NavigatorComponent Navigator { get; set; }
		public WardrobeComponent Wardrobe { get; set; }

		internal Player(DbDataReader reader)
		{
			Id                  = reader.ReadData<int>("id");
			Username            = reader.ReadData<string>("username");
			Mail                = reader.ReadData<string>("mail");
			Look                = reader.ReadData<string>("look");
			Motto               = reader.ReadData<string>("motto");
			Gender              = reader.ReadData<string>("gender");
			Rank                = reader.ReadData<int>("rank");
			ClubLevel           = reader.ReadData<int>("club_level");
			Credits             = reader.ReadData<int>("credits");
			HomeRoom            = reader.ReadData<int>("home_room");
			AchievementScore    = reader.ReadData<int>("achievement_score");
			ClubExpireTimestamp = reader.ReadData<int>("club_expire_time");
			Muted               = false;
			AllowTrading        = true;
			GroupId             = reader.ReadData<int>("group_id");
			Groups              = null; //todo
		}

		public async Task Initialize(Session session)
		{
			Session = session;
			PlayerSettings playerSettings = await Alias.Server.PlayerManager.ReadPlayerSettingsByIdAsync(Id);
			if (playerSettings == null)
			{
				await Alias.Server.PlayerManager.AddPlayerSettingsAsync(Id);
				playerSettings = await Alias.Server.PlayerManager.ReadPlayerSettingsByIdAsync(Id);
			}
			Settings = playerSettings;

			Achievements = new AchievementComponent(new AchievementDao(), this);
			await Achievements.Initialize();

			Badges = new BadgeComponent(new BadgeDao(), this);
			await Badges.Initialize();

			BonusRare = new BonusRareComponent(new BonusRareDao(), this);
			await Badges.Initialize();

			Crafting = new CraftingComponent(new CraftingDao(), this);
			await Crafting.Initialize();

			Currency = new CurrencyComponent(new CurrencyDao(), this);
			await Currency.Initialize();

			Inventory = new InventoryComponent(new InventoryDao(), this);
			await Inventory.Initialize();

			Messenger = new MessengerComponent(new MessengerDao(), this);
			await Messenger.Initialize();

			Navigator = new NavigatorComponent(new NavigatorDao(), this);
			await Navigator.Initialize();

			Wardrobe = new WardrobeComponent(new WardrobeDao(), this);
			await Wardrobe.Initialize();
			
			await Messenger.UpdateStatus();
		}

		public async void Dispose()
		{
			IsDisconnecting = true;
			Alias.Server.PlayerManager.RemovePlayer(Id);
			if (CurrentRoom != null)
			{
				CurrentRoom.EntityManager.OnUserLeave(Entity);
				CurrentRoom = null;
				Entity = null;
			}
			if (Settings != null)
			{
				await Alias.Server.PlayerManager.UpdatePlayerSettingsAsync(Settings, Id);
			}
			if (Currency != null)
			{
				Currency.Dispose();
			}
			if (Messenger != null)
			{
				await Messenger.UpdateStatus();
			}
		}

		public bool HasPermission(string param) => Alias.Server.PermissionManager.HasPermission(Rank, param);
	}
}
