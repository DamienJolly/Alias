using Alias.Emulator.Hotel.Rooms.Users.Events;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Rooms.Users
{
    public class RoomUserEvents
    {
		public static void Register()
		{
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomUserWalkMessageEvent, new RoomUserWalkEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomUserTalkMessageEvent, new RoomUserTalkEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomUserShoutMessageEvent, new RoomUserTalkEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomUserWhisperMessageEvent, new RoomUserTalkEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomUserSignMessageEvent, new RoomUserSignEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomUserLookAtPointMessageEvent, new RoomUserLookAtPointEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomUserDanceMessageEvent, new RoomUserDanceEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomUserSitMessageEvent, new RoomUserSitEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomUserActionMessageEvent, new RoomUserActionEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomUserGiveRightsMessageEvent, new RoomUserGiveRightsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomUserRemoveRightsMessageEvent, new RoomUserRemoveRightsEvent());
		}
	}
}
