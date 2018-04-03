using Alias.Emulator.Hotel.Rooms.Models.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Rooms.Models
{
	public class RoomModelEvents
	{
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.FloorPlanEditorRequestDoorSettingsMessageEvent, new FloorPlanEditorRequestDoorSettingsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.FloorPlanEditorRequestBlockedTilesMessageEvent, new FloorPlanEditorRequestBlockedTilesEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.FloorPlanEditorSaveMessageEvent, new FloorPlanEditorSaveEvent());
		}
	}
}
