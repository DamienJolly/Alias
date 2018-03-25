using Alias.Emulator.Utilities;
using Alias.Emulator.Database;
using Alias.Emulator.Network;
using System;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Catalog;
using Alias.Emulator.Hotel.Navigator;
using Alias.Emulator.Hotel.Achievements;
using Alias.Emulator.Hotel.Permissions;
using Alias.Emulator.Hotel.Chat;
using Alias.Emulator.Hotel.Moderation;
using Alias.Emulator.Tasks;
using MySql.Data.MySqlClient;

namespace Alias.Emulator
{
	sealed class AliasServer
    {
		/// <summary>
		/// Server database factory for handling entities and running queries.
		/// </summary>
		private DatabaseManager _database;

		/// <summary>
		/// Stores globally registered packets
		/// </summary>
		private PacketManager _packetManager;

		/// <summary>
		/// Task manager factory for handling our tasks.
		/// </summary>
		private TaskManager _taskManager;
		
		private RoomManager _roomManager;
		private ItemManager _itemManager;
		private CatalogManager _catalogManager;
		private NavigatorManager _navigatorManager;
		private AchievementManager _achievementManager;
		private PermissionManager _permissionManager;
		private ChatManager _chatManager;
		private ModerationManager _moderationManager;

		/// <summary>
		/// Initializes a new instance of the AliasServer.
		/// </summary>
		public AliasServer()
		{
			//todo:
		}

		public void Initialize()
		{
			MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder
			{
				ConnectionLifeTime    = (60 * 5),
				ConnectionTimeout     = 30,
				Database              = Configuration.Value("mysql.database"),
				DefaultCommandTimeout = 120,
				Logging               = false,
				MaximumPoolSize       = uint.Parse(Configuration.Value("mysql.maxsize")),
				MinimumPoolSize       = uint.Parse(Configuration.Value("mysql.minsize")),
				Password              = Configuration.Value("mysql.password"),
				Pooling               = true,
				Port                  = uint.Parse(Configuration.Value("mysql.port")),
				Server                = Configuration.Value("mysql.hostname"),
				UseCompression        = false,
				UserID                = Configuration.Value("mysql.username"),
				SslMode               = MySqlSslMode.None
			};

			_database = new DatabaseManager(cs.ToString());

			if (!_database.TestConnection())
			{
				Logging.Error("Unable to connect to database, check settings and restart Alias. Press any key to quit.");
				Console.ReadKey();

				Environment.Exit(0);
			}

			this._packetManager = new PacketManager();
			this._packetManager.Initialize();

			//todo:
			SessionManager.Initialize();

			this._roomManager = new RoomManager();
			this._roomManager.Initialize();

			this._itemManager = new ItemManager();
			this._itemManager.Initialize();

			this._catalogManager = new CatalogManager();
			this._catalogManager.Initialize();

			this._navigatorManager = new NavigatorManager();
			this._navigatorManager.Initialize();

			this._achievementManager = new AchievementManager();
			this._achievementManager.Initialize();

			this._permissionManager = new PermissionManager();
			this._permissionManager.Initialize();

			this._chatManager = new ChatManager();
			this._chatManager.Initialize();

			this._moderationManager = new ModerationManager();
			this._moderationManager.Initialize();
			
			this._taskManager = new TaskManager();

			SocketServer.Initialize();

			Logging.Info("Server is now online");
		}
		
		/// <summary>
		/// Safely shutdowns the AiasServer class.
		/// </summary>
		public void Shutdown()
		{
			Console.Clear();
			Console.WriteLine();
			Console.WriteLine("Server is shutting down, do not terminate Alias Server.");

			//todo:

			Console.WriteLine("All done... Press any key to exit.");
			Console.ReadKey();
			Environment.Exit(0);
		}

		public DatabaseManager GetDatabase() => this._database;

		public PacketManager GetPacketManager() => this._packetManager;

		public CatalogManager GetCatalogManager() => this._catalogManager;

		public ItemManager GetItemManager() => this._itemManager;

		public NavigatorManager GetNavigatorManager() => this._navigatorManager;

		public RoomManager GetRoomManager() => this._roomManager;

		public AchievementManager GetAchievementManager() => this._achievementManager;

		public PermissionManager GetPermissionManager() => this._permissionManager;

		public ChatManager GetChatManager() => this._chatManager;

		public ModerationManager GetModerationManager() => this._moderationManager;

		public TaskManager GetTaskManager() => this._taskManager;
	}
}
