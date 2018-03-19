using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Composers
{
	public class AddFloorItemComposer : IMessageComposer
	{
		private RoomItem item;

		public AddFloorItemComposer(RoomItem item)
		{
			this.item = item;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.AddFloorItemMessageComposer);
			result.Int(item.Id);
			result.Int(item.ItemData.SpriteId);
			result.Int(item.Position.X);
			result.Int(item.Position.Y);
			result.Int(item.Position.Rotation);
			result.String(item.Position.Z.ToString());
			result.String(item.ItemData.Height.ToString());
			result.Int(1);
			item.GetInteractor().Serialize(result, item);
			if (item.IsLimited)
			{
				result.Int(item.LimitedNumber);
				result.Int(item.LimitedStack);
			}
			result.Int(-1); // item Rent time
			result.Int((item.ItemData.Modes > 1) ? 1 : 0);
			result.Int(item.Owner); // Borrowed = -12345678
			result.String(item.Username);
			return result;
		}
	}
}
