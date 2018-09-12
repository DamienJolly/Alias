using System.IO;
using System.IO.Compression;
using Alias.Emulator.Hotel.Camera.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Camera.Events
{
	class CameraRoomPictureEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			message.GetBuffer().ReadFloat();

			byte[] data = message.GetBuffer().ReadBytes(message.BytesAvailable()).Array;
			string content = Deflate(data);

			if (Alias.Server.CameraAPI.SendData(content, out string path))
			{
				//session.LastPhoto = path;
				session.Send(new CameraURLComposer(path + ".png"));
			}
		}

		private static string Deflate(byte[] bytes)
		{
			using (var stream = new MemoryStream(bytes, 2, bytes.Length - 2))
			using (var inflater = new DeflateStream(stream, CompressionMode.Decompress))
			using (var streamReader = new StreamReader(inflater))
			{
				return streamReader.ReadToEnd();
			}
		}
	}
}
