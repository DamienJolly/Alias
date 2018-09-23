using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class GroupAcceptMembershipEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			int groupId = message.PopInt();
			int userId = message.PopInt();

			Group group = Alias.Server.GroupManager.GetGroup(groupId);
			if (group == null || group.OwnerId != session.Player.Id || group.GetMember(session.Player.Id).Rank != GroupRank.ADMIN)
			{
				return;
			}

			GroupMember member = group.GetMember(userId);
			if (member == null)
			{
				session.Send(new GroupAcceptMemberErrorComposer(group.Id, GroupAcceptMemberErrorComposer.NO_LONGER_MEMBER));
				return;
			}

			if(member.Rank != GroupRank.REQUESTED)
			{
				session.Send(new GroupAcceptMemberErrorComposer(group.Id, GroupAcceptMemberErrorComposer.ALREADY_ACCEPTED));
				return;
			}

			group.JoinGroup(session, userId, true);
			session.Send(new GroupRefreshMembersListComposer(group));

			Player targetPlayer = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(userId);
			if (targetPlayer != null)
			{
				Room room = targetPlayer.CurrentRoom;
				if (room != null)
				{
					if (room.RoomData.Group == group)
					{
						targetPlayer.Session.Send(new GroupInfoComposer(group, targetPlayer, false, member));
						room.RoomRights.RefreshRights(targetPlayer);
					}
				}
			}
		}
	}
}
