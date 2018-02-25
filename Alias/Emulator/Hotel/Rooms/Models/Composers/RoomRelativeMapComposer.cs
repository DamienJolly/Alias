using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Models.Composers
{
	public class RoomRelativeMapComposer : MessageComposer
	{
		private Room room;

		public RoomRelativeMapComposer(Room r)
		{
			this.room = r;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomRelativeMapMessageComposer);
			string[] split = this.room.Model.Map.Replace("\n", "").Split('\r');

			result.Int(split[0].Length);
			result.Int((split.Length) * split[0].Length);
			for (int y = 0; y < split.Length; y++)
			{
				for (int x = 0; x < split[0].Length; x++)
				{
					char position = split[y][x];
					if (position == 'x')
					{
						result.Short(-1);
					}
					else
					{
						result.Short((int)AliasEnvironment.ParseChar(position) * 256);
					}
				}
			}
			return result;
		}
	}
}
