using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class SaveBlockCameraFollowEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			bool cameraFollow = message.Boolean();

			session.Habbo.Settings.CameraFollow = cameraFollow;
		}
	}
}
