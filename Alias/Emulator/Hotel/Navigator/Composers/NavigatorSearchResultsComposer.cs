using System.Collections.Generic;
using Alias.Emulator.Hotel.Navigator.Views;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class NavigatorSearchResultsComposer : IPacketComposer
	{
		private string Category;
		private string Search;
		private List<INavigatorCategory> Categories;
		private Session session;

		public NavigatorSearchResultsComposer(string category, string search, Session s)
		{
			this.Category = category;
			this.Categories = Alias.GetServer().GetNavigatorManager().GetCategories(category);
			this.Search = search;
			this.session = s;
		}

		private bool IsParent(string category)
		{
			return (category == "official_view" || category == "hotel_view" || category == "roomads_view" || category == "myworld_view");
		}

		public ServerMessage Compose()
		{
			List<INavigatorCategory> Temp = new List<INavigatorCategory>();
			foreach (INavigatorCategory cat in Categories)
			{
				List<RoomData> rooms = cat.Search(this.Search.ToLower(), this.session, !IsParent(this.Category) ? 100 : 13);
				if (rooms.Count > 0)
					Temp.Add(cat);
			}

			ServerMessage message = new ServerMessage(Outgoing.NavigatorSearchResultsMessageComposer);
			message.String(this.Category);
			message.String(this.Search);
			message.Int(Temp.Count);
			Temp.ForEach(cat =>
			{
				List<RoomData> rooms = cat.Search(this.Search.ToLower(), this.session, !IsParent(this.Category) ? 100 : 13);
				message.String((cat.Id == "popular" && !string.IsNullOrEmpty(this.Search)) ? "query" : cat.Id);
				message.String(cat.Title);
				message.Int(!IsParent(this.Category) ? 2 : (rooms.Count > 12) ? 1 : 0);
				message.Boolean(cat.ShowCollapsed);
				message.Int(cat.ShowThumbnail ? 1 : 0);

				//Remove the extra roomdata
				if (rooms.Count > 12)
					rooms.RemoveAt(rooms.Count - 1);

				message.Int(rooms.Count);
				rooms.ForEach(room =>
				{
					message.Int(room.Id);
					message.String(room.Name);
					message.Int(room.OwnerId);
					message.String(room.OwnerName);
					message.Int(Alias.GetServer().GetRoomManager().DoorToInt(room.DoorState));
					message.Int(room.UsersNow);
					message.Int(room.MaxUsers);
					message.String(room.Description);
					message.Int(Alias.GetServer().GetRoomManager().TradeToInt(room.TradeState));
					message.Int(room.Likes.Count);
					message.Int(room.Rankings);
					message.Int(room.Category);
					message.Int(room.Tags.Count);
					room.Tags.ForEach(tag => { message.String(tag); });
					message.Int(room.EnumType);
					if (room.Image.Length > 0)
					{
						message.String(room.Image);
					}
				});
			});
			return message;
		}
	}
}
