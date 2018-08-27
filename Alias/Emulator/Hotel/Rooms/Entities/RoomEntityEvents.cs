using Alias.Emulator.Hotel.Rooms.Entities.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Rooms.Entities
{
    public class RoomEntityEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserWalkMessageEvent, new RoomUserWalkEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserTalkMessageEvent, new RoomUserTalkEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserShoutMessageEvent, new RoomUserTalkEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserWhisperMessageEvent, new RoomUserTalkEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserSignMessageEvent, new RoomUserSignEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserLookAtPointMessageEvent, new RoomUserLookAtPointEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserDanceMessageEvent, new RoomUserDanceEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserSitMessageEvent, new RoomUserSitEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserActionMessageEvent, new RoomUserActionEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserGiveRightsMessageEvent, new RoomUserGiveRightsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserRemoveRightsMessageEvent, new RoomUserRemoveRightsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserPlaceBotMessageEvent, new RoomUserPlaceBotEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomBotSettingsMessageEvent, new RoomBotSettingsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomBotSaveSettingsMessageEvent, new RoomBotSaveSettingsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserPickupBotMessageEvent, new RoomUserPickupBotEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserPlacePetMessageEvent, new RoomUserPlacePetEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserPickupPetMessageEvent, new RoomUserPickupPetEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestPetInformationMessageEvent, new RequestPetInformationEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserStartTypingMessageEvent, new RoomUserStartTypingEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomUserStopTypingMessageEvent, new RoomUserStopTypingEvent());
		}
	}
}
