using Alias.Emulator.Hotel.Camera.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Camera
{
    public class CameraEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.CameraRoomPictureMessageEvent, new CameraRoomPictureEvent());
		}
	}
}
