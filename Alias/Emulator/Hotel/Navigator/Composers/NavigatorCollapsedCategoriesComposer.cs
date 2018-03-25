using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class NavigatorCollapsedCategoriesComposer : IPacketComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.NavigatorCollapsedCategoriesMessageComposer);
			result.Int(46);
			result.String("new_ads");
			result.String("friend_finding");
			result.String("staffpicks");
			result.String("with_friends");
			result.String("with_rights");
			result.String("query");
			result.String("recommended");
			result.String("my_groups");
			result.String("favorites");
			result.String("history");
			result.String("top_promotions");
			result.String("campaign_target");
			result.String("friends_rooms");
			result.String("groups");
			result.String("metadata");
			result.String("history_freq");
			result.String("highest_score");
			result.String("competition");
			result.String("category__Agencies");
			result.String("category__Role Playing");
			result.String("category__Global Chat & Discussi");
			result.String("category__GLOBAL BUILDING AND DE");
			result.String("category__global party");
			result.String("category__global games");
			result.String("category__global fansite");
			result.String("category__global help");
			result.String("category__Trading");
			result.String("category__global personal space");
			result.String("category__Habbo Life");
			result.String("category__TRADING");
			result.String("category__global official");
			result.String("category__global trade");
			result.String("category__global reviews");
			result.String("category__global bc");
			result.String("category__global personal space");
			result.String("eventcategory__Hottest Events");
			result.String("eventcategory__Parties & Music");
			result.String("eventcategory__Role Play");
			result.String("eventcategory__Help Desk");
			result.String("eventcategory__Trading");
			result.String("eventcategory__Games");
			result.String("eventcategory__Debates & Discuss");
			result.String("eventcategory__Grand Openings");
			result.String("eventcategory__Friending");
			result.String("eventcategory__Jobs");
			result.String("eventcategory__Group Events");
			return result;
		}
	}
}
