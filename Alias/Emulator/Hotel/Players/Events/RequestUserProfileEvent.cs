using Alias.Emulator.Hotel.Players.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Events
{
    class RequestUserProfileEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			int userId = message.PopInt();
			if (userId <= 0)
			{
				//todo: open group chat
				return;
			}

			Player target = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(userId);
			if (target == null)
			{
				return;
			}

			session.Send(new UserProfileComposer(target, session));
		}
	}
}
