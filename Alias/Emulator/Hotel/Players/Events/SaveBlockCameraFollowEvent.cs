using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Events
{
	class SaveBlockCameraFollowEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			bool cameraFollow = message.PopBoolean();

			session.Player.Settings.CameraFollow = cameraFollow;
		}
	}
}
