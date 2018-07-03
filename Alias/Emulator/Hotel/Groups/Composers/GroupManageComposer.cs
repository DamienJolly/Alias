using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Groups.Composers
{
    class GroupManageComposer : IPacketComposer
	{
		private Group group;

		public GroupManageComposer(Group group)
		{
			this.group = group;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GroupManageMessageComposer);
			message.WriteInteger(0);
			message.WriteBoolean(true);
			message.WriteInteger(this.group.Id);
			message.WriteString(this.group.Name);
			message.WriteString(this.group.Description);
			message.WriteInteger(this.group.RoomId);
			message.WriteInteger(this.group.ColourOne);
			message.WriteInteger(this.group.ColourTwo);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteBoolean(false);
			message.WriteString("");
			message.WriteInteger(5);

			string badge = this.group.Badge;
			badge = badge.Replace("b", "");
			string[] data = badge.Split("s");

			foreach (string part in data)
			{
				message.WriteInteger((part.Length >= 6 ? int.Parse(part.Substring(0, 3)) : int.Parse(part.Substring(0, 2))));
				message.WriteInteger((part.Length >= 6 ? int.Parse(part.Substring(3, 2)) : int.Parse(part.Substring(2, 2))));
				message.WriteInteger(part.Length < 5 ? 0 : part.Length >= 6 ? int.Parse(part.Substring(5, 1)) : int.Parse(part.Substring(4, 1)));
			}

			int i = 0;
			while (i < (5 - data.Length))
			{
				message.WriteInteger(0);
				message.WriteInteger(0);
				message.WriteInteger(0);
				i++;
			}

			message.WriteString(this.group.Badge);
			message.WriteInteger(this.group.GetMembers);
			return message;
		}
	}
}
