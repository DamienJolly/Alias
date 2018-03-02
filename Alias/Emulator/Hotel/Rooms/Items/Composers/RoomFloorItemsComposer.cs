using System.Collections.Generic;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Composers
{
	public class RoomFloorItemsComposer : IMessageComposer
	{
		private List<RoomItem> Items;

		public RoomFloorItemsComposer(List<RoomItem> items)
		{
			this.Items = items;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomFloorItemsMessageComposer);
			result.Int(0); //TODO
						   //result.Int(0); //userId
						   //result.String(""); //username
			result.Int(this.Items.Count);
			this.Items.ForEach(item =>
			{
				result.Int(item.Id);
				result.Int(item.ItemData.Id);
				result.Int(item.Position.X);
				result.Int(item.Position.Y);
				result.Int(item.Position.Rotation);
				result.String(item.Position.Z.ToString());
				result.String(item.ItemData.Height.ToString());
				result.Int(1);
				item.GetInteractor().Serialize(result, item);
				//if (item.LimitedNo > 0)
				{
					//result.Int(item.LimitedNo);
					//result.Int(item.LimitedTot);
				}
				result.Int(-1); // item Rent time
				result.Int((item.ItemData.Modes > 1) ? 1 : 0);
				result.Int(item.Owner); // Borrowed = -12345678
			});
			return result;
		}
	}
}