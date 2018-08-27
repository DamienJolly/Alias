using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Games.Events
{
    class GameCenterInitializeEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			//
		}
	}
}
