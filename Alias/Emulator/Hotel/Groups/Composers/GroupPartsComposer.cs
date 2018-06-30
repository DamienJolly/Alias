using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Groups.Composers
{
    class GroupPartsComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GuildPartsMessageComposer);
			message.WriteInteger(Alias.Server.GroupManager.GetBases.Count);
			Alias.Server.GroupManager.GetBases.ForEach(part =>
			{
				message.WriteInteger(part.Id);
				message.WriteString(part.AssetOne);
				message.WriteString(part.AssetTwo);
			});

			message.WriteInteger(Alias.Server.GroupManager.GetSymbols.Count);
			Alias.Server.GroupManager.GetSymbols.ForEach(part =>
			{
				message.WriteInteger(part.Id);
				message.WriteString(part.AssetOne);
				message.WriteString(part.AssetTwo);
			});

			message.WriteInteger(Alias.Server.GroupManager.GetBaseColours.Count);
			Alias.Server.GroupManager.GetBaseColours.ForEach(part =>
			{
				message.WriteInteger(part.Id);
				message.WriteString(part.AssetOne);
			});

			message.WriteInteger(Alias.Server.GroupManager.GetSymbolColours.Count);
			Alias.Server.GroupManager.GetSymbolColours.ForEach(part =>
			{
				message.WriteInteger(part.Id);
				message.WriteString(part.AssetOne);
			});

			message.WriteInteger(Alias.Server.GroupManager.GetBackgroundColours.Count);
			Alias.Server.GroupManager.GetBackgroundColours.ForEach(part =>
			{
				message.WriteInteger(part.Id);
				message.WriteString(part.AssetOne);
			});
			return message;
		}
	}
}
