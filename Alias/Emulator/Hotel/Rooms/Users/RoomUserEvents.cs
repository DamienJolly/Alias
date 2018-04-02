using Alias.Emulator.Hotel.Rooms.Users.Events;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Rooms.Users
{
    public class RoomUserEvents
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
		}
	}
}
