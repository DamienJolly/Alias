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
			if (!int.TryParse(Alias.Server.Settings.GetSetting("camera.credits.cost"), out int creditsCost))
			{
				creditsCost = 100;
			}

			if (!int.TryParse(Alias.Server.Settings.GetSetting("camera.points.cost"), out int pointsCost))
			{
				pointsCost = 250;
			}

			if (!int.TryParse(Alias.Server.Settings.GetSetting("camera.publish.cost"), out int publishCost))
			{
				publishCost = 500;
			}

			session.Send(new CameraPriceComposer(creditsCost, pointsCost, publishCost));
		}
	}
}
