using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Hotel.Groups.Events;

namespace Alias.Emulator.Hotel.Groups
{
    public class GroupEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestGroupInfoMessageEvent, new RequestGroupInfoEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestGroupBuyRoomsMessageEvent, new RequestGroupBuyRoomsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestGroupPartsMessageEvent, new RequestGroupPartsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestGroupBuyMessageEvent, new RequestGroupBuyEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestGroupManageMessageEvent, new RequestGroupManageEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestGroupMembersMessageEvent, new RequestGroupMembersEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.GroupSetAdminMessageEvent, new GroupSetAdminEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.GroupRemoveAdminMessageEvent, new GroupRemoveAdminEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.GroupRemoveMemberMessageEvent, new GroupRemoveMemberEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.GroupDeleteMessageEvent, new GroupDeleteEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.GroupChangeNameDescMessageEvent, new GroupChangeNameDescEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.GroupChangeBadgeMessageEvent, new GroupChangeBadgeEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.GroupChangeColorsMessageEvent, new GroupChangeColorsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.GroupChangeSettingsMessageEvent, new GroupChangeSettingsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestGroupJoinMessageEvent, new RequestGroupJoinEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.GroupAcceptMembershipMessageEvent, new GroupAcceptMembershipEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.GroupDeclineMembershipMessageEvent, new GroupDeclineMembershipEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.GetPlayerGroupBadgesMessageEvent, new GetPlayerGroupBadgesEvent());
		}
	}
}
