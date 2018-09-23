using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class GroupRemoveAdminEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			int groupId = message.PopInt();
			int userId = message.PopInt();

			Group group = Alias.Server.GroupManager.GetGroup(groupId);
			if (group == null || group.OwnerId != session.Player.Id)
			{
				return;
			}

			group.SetMemberRank(userId, 2);
			GroupMember member = group.GetMember(userId);
			session.Send(new GroupMemberUpdateComposer(group, member));

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
