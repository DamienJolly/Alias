using Alias.Emulator.Hotel.Moderation.Events;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Moderation
{
    public class ModerationEvents
    {
		public static void Register()
		{
			MessageHandler.Register(Incoming.ModerationAlertMessageEvent, new ModerationAlertEvent());
			MessageHandler.Register(Incoming.ModerationRequestUserInfoMessageEvent, new ModerationRequestUserInfoEvent());
			MessageHandler.Register(Incoming.ModerationRequestRoomInfoMessageEvent, new ModerationRequestRoomInfoEvent());
			MessageHandler.Register(Incoming.ModerationRequestRoomVisitsMessageEvent, new ModerationRequestRoomVisitsEvent());
			MessageHandler.Register(Incoming.ModerationRequestRoomChatlogMessageEvent, new ModerationRequestRoomChatlogEvent());
			MessageHandler.Register(Incoming.ModerationChangeRoomSettingsMessageEvent, new ModerationChangeRoomSettingsEvent());
			MessageHandler.Register(Incoming.ModerationRequestUserChatlogMessageEvent, new ModerationRequestUserChatlogEvent());
			MessageHandler.Register(Incoming.ModerationKickMessageEvent, new ModerationKickEvent());
			MessageHandler.Register(Incoming.ModerationPickTicketMessageEvent, new ModerationPickTicketEvent());
			MessageHandler.Register(Incoming.ModerationReleaseTicketMessageEvent, new ModerationReleaseTicketEvent());
			MessageHandler.Register(Incoming.ModerationCloseTicketMessageEvent, new ModerationCloseTicketEvent());
			MessageHandler.Register(Incoming.ModerationRoomAlertMessageEvent, new ModerationRoomAlertEvent());
			MessageHandler.Register(Incoming.ModerationRequestIssueChatlogMessageEvent, new ModerationRequestIssueChatlogEvent());
			
			//sanction shit
			MessageHandler.Register(Incoming.ModerationSanctionAlertMessageEvent, new ModerationSanctionAlertEvent());
			MessageHandler.Register(Incoming.ModerationSanctionMuteMessageEvent, new ModerationSanctionMuteEvent());
			MessageHandler.Register(Incoming.ModerationSanctionBanMessageEvent, new ModerationSanctionBanEvent());
			MessageHandler.Register(Incoming.ModerationSanctionTradeLockMessageEvent, new ModerationSanctionTradeLockEvent());
		}
	}
}
