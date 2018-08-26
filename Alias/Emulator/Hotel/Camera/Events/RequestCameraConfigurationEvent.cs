using Alias.Emulator.Hotel.Camera.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Camera.Events
{
	class RequestCameraConfigurationEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			//todo: add to db
			int creditsCost = 0;
			int pointsCost = 0;
			int pointsPublishCost = 0;

			session.Send(new CameraPriceComposer(creditsCost, pointsCost, pointsPublishCost));
		}
	}
}
