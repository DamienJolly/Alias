using System.IO;
using System.IO.Compression;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Camera.Events
{
	class CameraRoomPictureEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			message.GetBuffer().ReadFloat();

			byte[] data = message.GetBuffer().ReadBytes(message.BytesAvailable()).Array;
			string content = Deflate(data);

			System.Console.WriteLine(content);
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
