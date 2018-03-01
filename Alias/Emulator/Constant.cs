using System.Text;

namespace Alias.Emulator
{
	/// <summary>
	/// All Constant Variables belong here!
	/// </summary>
	public class Constant
	{
		public static readonly string ExceptionFile = "exceptions.alias";
		public static readonly string ConfigurationFile = "configuration.alias";
		public static readonly byte[] PolicyFile = Encoding.UTF8.GetBytes("<?xml version=\"1.0\"?>\r\n<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n<cross-domain-policy>\r\n<allow-access-from domain=\"*\" to-ports=\"1-31111\" />\r\n</cross-domain-policy>\0");
		public static readonly string ProductionVersion = "PRODUCTION-201802201205-141713395";
		public static readonly int MaximalFriends = 1000;
		public static readonly string FilterWord = "bobba";
	}
}
