using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	public class NewsListComposer : IPacketComposer
	{
		private List<LandingArticle> articles;

		public NewsListComposer(List<LandingArticle> articles)
		{
			this.articles = articles;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.NewsListMessageComposer);
			message.WriteInteger(this.articles.Count);
			this.articles.ForEach(article =>
			{
				message.WriteInteger(article.Id);
				message.WriteString(article.Title);
				message.WriteString(article.Message);
				message.WriteString(article.Caption);
				message.WriteInteger(article.Type);
				message.WriteString(article.Link);
				message.WriteString(article.Image);
			});
			return message;
		}
	}
}
