using System.Text;
using DotNetty.Buffers;

namespace Alias.Emulator.Network.Protocol
{
	public class ServerPacket
	{
		public IByteBuffer Buffer { get; }
		public short Id { get; private set; }

		public ServerPacket(short id)
		{
			Buffer = Unpooled.Buffer();
			Id = id;
			Buffer.WriteInt(-1);
			Buffer.WriteShort(id);
		}

		public bool HasLength =>
			Buffer.GetInt(0) > -1;

		public void WriteByte(byte b) =>
			Buffer.WriteByte(b);

		public void WriteByte(int b) =>
			Buffer.WriteByte((byte)b);

		public void WriteDouble(double d) =>
			WriteString(d.ToString());

		public void WriteString(string s) // d
		{
			Buffer.WriteShort(s.Length);
			Buffer.WriteBytes(Encoding.Default.GetBytes(s));
		}

		public void WriteShort(int s) =>
			Buffer.WriteShort(s);

		public void WriteInteger(int i) =>
			Buffer.WriteInt(i);

		public void WriteInteger(uint i) =>
			Buffer.WriteInt((int)i);

		public void WriteBoolean(bool b) =>
			Buffer.WriteByte(b ? 1 : 0);
	}
}
