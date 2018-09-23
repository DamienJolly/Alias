using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Messenger.Composers
{
	class LoadFriendRequestsComposer : IPacketComposer
	{
		private ICollection<MessengerRequest> _requests;

		public LoadFriendRequestsComposer(ICollection<MessengerRequest> requests)
		{
			_requests = requests;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.LoadFriendRequestsMessageComposer);
			message.WriteInteger(_requests.Count);
			message.WriteInteger(_requests.Count);
			foreach (MessengerRequest request in _requests)
			{
				message.WriteInteger(request.Id);
				message.WriteString(request.Username);
				message.WriteString(request.Look);
			}
			return message;
		}
	}
}
