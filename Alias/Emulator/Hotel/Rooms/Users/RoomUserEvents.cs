using Alias.Emulator.Hotel.Rooms.Users.Events;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Rooms.Users
{
    public class RoomUserEvents
    {
		public static void Register()
		{
			MessageHandler.Register(Incoming.RoomUserWalkMessageEvent, new RoomUserWalkEvent());
			MessageHandler.Register(Incoming.RoomUserTalkMessageEvent, new RoomUserTalkEvent());
			MessageHandler.Register(Incoming.RoomUserShoutMessageEvent, new RoomUserTalkEvent());
			MessageHandler.Register(Incoming.RoomUserWhisperMessageEvent, new RoomUserTalkEvent());
			MessageHandler.Register(Incoming.RoomUserSignMessageEvent, new RoomUserSignEvent());
			MessageHandler.Register(Incoming.RoomUserLookAtPointMessageEvent, new RoomUserLookAtPointEvent());
			MessageHandler.Register(Incoming.RoomUserDanceMessageEvent, new RoomUserDanceEvent());
			MessageHandler.Register(Incoming.RoomUserSitMessageEvent, new RoomUserSitEvent());
			MessageHandler.Register(Incoming.RoomUserActionMessageEvent, new RoomUserActionEvent());
			MessageHandler.Register(Incoming.RoomUserGiveRightsMessageEvent, new RoomUserGiveRightsEvent());
			MessageHandler.Register(Incoming.RoomUserRemoveRightsMessageEvent, new RoomUserRemoveRightsEvent());
		}
	}
}
