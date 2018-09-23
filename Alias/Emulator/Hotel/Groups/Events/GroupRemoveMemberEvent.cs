using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class GroupRemoveMemberEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			int groupId = message.PopInt();
			int userId = message.PopInt();

			Group group = Alias.Server.GroupManager.GetGroup(groupId);
			if (group == null)
			{
				return;
			}

			if ((group.OwnerId == session.Player.Id && userId != session.Player.Id) || userId == session.Player.Id)
			{
				group.RemoveMember(userId);

				if (userId != session.Player.Id)
				{
					session.Send(new GroupRefreshMembersListComposer(group));
				}

				Player targetPlayer = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(userId);
				if (targetPlayer != null)
				{
					targetPlayer.GroupId = 0;
					Room room = targetPlayer.CurrentRoom;
					if (room != null)
					{
						if (room.RoomData.Group == group)
						{
							targetPlayer.Session.Send(new GroupInfoComposer(group, targetPlayer, false, null));
							room.RoomRights.RefreshRights(targetPlayer);
						}
					}
				}
				// todo: eject furni
			}
		}
	}
}
