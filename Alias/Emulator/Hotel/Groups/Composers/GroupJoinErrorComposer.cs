using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Groups.Composers
{
    class GroupJoinErrorComposer : IPacketComposer
	{
		public static int GROUP_FULL = 0;
		public static int GROUP_LIMIT_EXCEED = 1;
		public static int GROUP_CLOSED = 2;
		public static int GROUP_NOT_ACCEPT_REQUESTS = 3;
		public static int NON_HC_LIMIT_REACHED = 4;
		public static int MEMBER_FAIL_JOIN_LIMIT_EXCEED_NON_HC = 5;
		public static int MEMBER_FAIL_JOIN_LIMIT_EXCEED_HC = 6;

		private int code;

		public GroupJoinErrorComposer(int code)
		{
			this.code = code;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GroupJoinErrorMessageComposer);
			message.WriteInteger(this.code);
			return message;
		}
	}
}
