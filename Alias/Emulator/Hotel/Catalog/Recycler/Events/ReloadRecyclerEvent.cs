using Alias.Emulator.Hotel.Catalog.Recycler.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Recycler.Events
{
    class ReloadRecyclerEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new ReloadRecyclerComposer());
		}
	}
}
