using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	class RoomBotSettingsComposer : IPacketComposer
	{
		private RoomEntity bot;
		private int settingId;

		public RoomBotSettingsComposer(RoomEntity bot, int settingId)
		{
			this.bot = bot;
			this.settingId = settingId;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomBotSettingsMessageComposer);
			message.WriteInteger(this.bot.Id);
			message.WriteInteger(this.settingId);
			switch (this.settingId)
			{
				case 1: case 3: case 4: break;
				case 2:
					//to-do
					break;
				case 5:
					message.WriteString(bot.Name);
					break;
			}
			return message;
		}
	}
}
