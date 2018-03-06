using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomSettingsSavedComposer : IMessageComposer
	{
		private int roomId;

		public RoomSettingsSavedComposer(int roomId)
		{
			this.roomId = roomId;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomSettingsSavedMessageComposer);
			result.Int(this.roomId);
			return result;
		}
	}
}
