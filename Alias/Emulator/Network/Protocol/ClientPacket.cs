using System.Text;
using DotNetty.Buffers;

namespace Alias.Emulator.Network.Protocol
{
	public class ClientPacket
	{
		private IByteBuffer buffer;
		public short Header { get; }

		public ClientPacket(IByteBuffer buf)
		{
			buffer = buf;
			Header = buffer.ReadShort();
		}

		public IByteBuffer GetBuffer() =>
			buffer;

		public string PopString()
		{
			int length = buffer.ReadShort();
			IByteBuffer data = buffer.ReadBytes(length);
			return Encoding.Default.GetString(data.Array);
		}

		public int PopInt() =>
			buffer.ReadInt();

		public bool PopBoolean() =>
			buffer.ReadByte() == 1;

		public int BytesAvailable() =>
			buffer.ReadableBytes;
	}
}
