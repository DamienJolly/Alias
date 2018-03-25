using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class SaveBlockCameraFollowEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			bool cameraFollow = message.Boolean();

			session.Habbo.Settings.CameraFollow = cameraFollow;
		}
	}
}
