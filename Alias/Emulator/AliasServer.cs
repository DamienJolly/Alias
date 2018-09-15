using Alias.Emulator.Utilities;
using Alias.Emulator.Database;
using Alias.Emulator.Network;
using System;
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
using Alias.Emulator.Hotel.Groups;
using Camera;
using Alias.Emulator.Hotel.Landing;
using Alias.Emulator.Settings;
using System.Threading.Tasks;

namespace Alias.Emulator
{
	sealed class AliasServer
    {
		/// <summary>
		/// Server database factory for handling entities and running queries.
		/// </summary>
		public DatabaseManager DatabaseManager { get; set; }

		public SocketServer SocketServer { get; set; }

		public SettingsManager Settings { get; set; }

		/// <summary>
		/// Task manager factory for handling our tasks.
		/// </summary>
		public TaskManager TaskManager { get; set; }

		/// <summary>
		/// CameraAPI for image creation.
		/// </summary>
		public CameraAPI CameraAPI { get; set; }

		public RoomManager RoomManager { get; set; }
		public ItemManager ItemManager { get; set; }
		public CatalogManager CatalogManager { get; set; }
		public NavigatorManager NavigatorManager { get; set; }
		public AchievementManager AchievementManager { get; set; }
		public GroupManager GroupManager { get; set; }
		public PermissionManager PermissionManager { get; set; }
		public ChatManager ChatManager { get; set; }
		public ModerationManager ModerationManager { get; set; }
		public LandingManager LandingManager { get; set; }

		/// <summary>
		/// Initializes a new instance of the AliasServer.
		/// </summary>
		public AliasServer()
		{

		}

		public async Task Initialize()
		{
			this.Settings = new SettingsManager();

			MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder
			{
				ConnectionLifeTime    = (60 * 5),
				ConnectionTimeout     = 30,
				Database              = this.Settings.ConfigData.MySQLDatabase,
				DefaultCommandTimeout = 120,
				Logging               = false,
				MaximumPoolSize       = this.Settings.ConfigData.MySQLMaximumPoolSize,
				MinimumPoolSize       = this.Settings.ConfigData.MySQLMinimumPoolSize,
				Password              = this.Settings.ConfigData.MySQLPassword,
				Pooling               = true,
				Port                  = this.Settings.ConfigData.MySQLPort,
				Server                = this.Settings.ConfigData.MySQLHostName,
				UseCompression        = false,
				UserID                = this.Settings.ConfigData.MySQLUsername,
				SslMode               = MySqlSslMode.None
			};

			this.DatabaseManager = new DatabaseManager(cs.ToString());
			AbstractDao.ConnectionString = cs.ToString();

			if (!this.DatabaseManager.TestConnection())
			{
				Logging.Exit("Unable to connect to database, check settings and restart Alias.");
			}

			this.Settings.Initialize();

			this.SocketServer = new SocketServer(this.Settings.ConfigData.ServerIPAddress, this.Settings.ConfigData.ServerPort);
			this.SocketServer.Initialize();
			
			//this.CameraAPI = new CameraAPI();
			//this.CameraAPI.AttemptLogin("Damien", "password123");

			this.RoomManager = new RoomManager();
			this.RoomManager.Initialize();

			this.ItemManager = new ItemManager();
			this.ItemManager.Initialize();

			this.CatalogManager = new CatalogManager();
			this.CatalogManager.Initialize();

			this.NavigatorManager = new NavigatorManager();
			this.NavigatorManager.Initialize();

			AchievementManager = new AchievementManager(new AchievementDao());
			await AchievementManager.Initialize();

			this.LandingManager = new LandingManager();
			this.LandingManager.Initialize();

			this.GroupManager = new GroupManager();
			this.GroupManager.Initialize();

			this.PermissionManager = new PermissionManager();
			this.PermissionManager.Initialize();

			this.ChatManager = new ChatManager();
			this.ChatManager.Initialize();

			this.ModerationManager = new ModerationManager();
			this.ModerationManager.Initialize();
			
			this.TaskManager = new TaskManager();

			this.SocketServer.PacketManager.Initialize();

			// Lets start our socket server
			this.SocketServer.Connect();

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
			this.SocketServer.Stop();

			this.CameraAPI.Dispose();

			Logging.Exit("All done...");
		}
	}
}
