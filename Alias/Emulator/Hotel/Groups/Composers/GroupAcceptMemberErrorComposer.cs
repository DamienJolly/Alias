using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Groups.Composers
{
    class GroupAcceptMemberErrorComposer : IPacketComposer
	{
		public static int NO_LONGER_MEMBER = 0;
		public static int ALREADY_REJECTED = 1;
		public static int ALREADY_ACCEPTED = 2;

		private int groupId;
		private int code;

		public GroupAcceptMemberErrorComposer(int groupId, int code)
		{
			this.groupId = groupId;
			this.code = code;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GroupAcceptMemberErrorMessageComposer);
			message.WriteInteger(this.groupId);
			message.WriteInteger(this.code);
			return message;
		}
	}
}
