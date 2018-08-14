using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	class RequestCreateRoomEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			string name = message.PopString();
			string description = message.PopString();
			string modelName = message.PopString();
			int categoryId = message.PopInt();
			int maxUsers = message.PopInt();
			int tradeType = message.PopInt();

			if (!Alias.Server.RoomManager.RoomModelManager.ModelExists(modelName))
			{
				return;
			}

			// todo: category check

			if (maxUsers > 250 || maxUsers < 10)
			{
				return;
			}

			if (tradeType > 2 || tradeType < 0)
			{
				return;
			}

			// todo: room count check

			Room room = Alias.Server.RoomManager.CreateRoom(session.Habbo.Id, name, description, modelName, maxUsers, tradeType, categoryId);
			if (room != null)
			{
				if (session.Habbo.CurrentRoom != null)
				{
					session.Habbo.CurrentRoom.EntityManager.OnUserLeave(session.Habbo.Entity);
				}

				session.Send(new RoomCreatedComposer(room));
			}
		}
	}
}
