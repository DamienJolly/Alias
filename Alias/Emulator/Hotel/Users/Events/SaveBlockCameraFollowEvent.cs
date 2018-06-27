using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	class SaveBlockCameraFollowEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			bool cameraFollow = message.PopBoolean();

			session.Habbo.Settings.CameraFollow = cameraFollow;
		}
	}
}
