using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RequestPetInformationEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int petId = message.PopInt();

			RoomEntity pet = room.EntityManager.PetById(petId);
			if (pet == null)
			{
				return;
			}

			session.Send(new PetInformationComposer(pet));
		}
	}
}
