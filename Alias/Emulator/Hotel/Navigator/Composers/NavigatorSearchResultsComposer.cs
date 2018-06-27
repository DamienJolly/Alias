using System.Collections.Generic;
using Alias.Emulator.Hotel.Navigator.Views;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	class NavigatorSearchResultsComposer : IPacketComposer
	{
		private string Category;
		private string Search;
		private List<INavigatorCategory> Categories;
		private Session session;

		public NavigatorSearchResultsComposer(string category, string search, Session s)
		{
			this.Category = category;
			this.Categories = Alias.Server.NavigatorManager.GetCategories(category);
			this.Search = search;
			this.session = s;
		}

		private bool IsParent(string category)
		{
			return (category == "official_view" || category == "hotel_view" || category == "roomads_view" || category == "myworld_view");
		}

		public ServerPacket Compose()
		{
			List<INavigatorCategory> Temp = new List<INavigatorCategory>();
			foreach (INavigatorCategory cat in Categories)
			{
				List<RoomData> rooms = cat.Search(this.Search.ToLower(), this.session, !IsParent(this.Category) ? 100 : 13);
				if (rooms.Count > 0)
					Temp.Add(cat);
			}

			ServerPacket message = new ServerPacket(Outgoing.NavigatorSearchResultsMessageComposer);
			message.WriteString(this.Category);
			message.WriteString(this.Search);
			message.WriteInteger(Temp.Count);
			Temp.ForEach(cat =>
			{
				List<RoomData> rooms = cat.Search(this.Search.ToLower(), this.session, !IsParent(this.Category) ? 100 : 13);
				message.WriteString((cat.Id == "popular" && !string.IsNullOrEmpty(this.Search)) ? "query" : cat.Id);
				message.WriteString(cat.Title);
				message.WriteInteger(!IsParent(this.Category) ? 2 : (rooms.Count > 12) ? 1 : 0);
				message.WriteBoolean(cat.ShowCollapsed);
				message.WriteInteger(cat.ShowThumbnail ? 1 : 0);

				//Remove the extra roomdata
				if (rooms.Count > 12)
					rooms.RemoveAt(rooms.Count - 1);

				message.WriteInteger(rooms.Count);
				rooms.ForEach(room =>
				{
					message.WriteInteger(room.Id);
					message.WriteString(room.Name);
					message.WriteInteger(room.OwnerId);
					message.WriteString(room.OwnerName);
					message.WriteInteger(Alias.Server.RoomManager.DoorToInt(room.DoorState));
					message.WriteInteger(room.UsersNow);
					message.WriteInteger(room.MaxUsers);
					message.WriteString(room.Description);
					message.WriteInteger(Alias.Server.RoomManager.TradeToInt(room.TradeState));
					message.WriteInteger(room.Likes.Count);
					message.WriteInteger(room.Rankings);
					message.WriteInteger(room.Category);
					message.WriteInteger(room.Tags.Count);
					room.Tags.ForEach(tag => { message.WriteString(tag); });
					message.WriteInteger(room.EnumType);
					if (room.Image.Length > 0)
					{
						message.WriteString(room.Image);
					}
				});
			});
			return message;
		}
	}
}
