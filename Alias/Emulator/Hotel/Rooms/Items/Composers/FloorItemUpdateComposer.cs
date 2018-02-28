using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Composers
{
	public class FloorItemUpdateComposer : IMessageComposer
	{
		private RoomItem Item;

		public FloorItemUpdateComposer(RoomItem item)
		{
			this.Item = item;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.FloorItemUpdateMessageComposer);
			result.Int(this.Item.Id);
			result.Int(this.Item.ItemData.Id);
			result.Int(this.Item.Position.X);
			result.Int(this.Item.Position.Y);
			result.Int(this.Item.Position.Rotation);
			result.String(this.Item.Position.Z.ToString());
			result.String(this.Item.ItemData.Height.ToString());
			result.Int(1);
			this.Item.GetInteractor().Serialize(result, this.Item);
			//if (item.LimitedNo > 0)
			{
				//result.Int(item.LimitedNo);
				//result.Int(item.LimitedTot);
			}
			result.Int(-1); // item Rent time
			result.Int((this.Item.ItemData.Modes > 1) ? 1 : 0);
			result.Int(this.Item.Owner); // Borrowed = -12345678
			return result;
		}
	}
}
