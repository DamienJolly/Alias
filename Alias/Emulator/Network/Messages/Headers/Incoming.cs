namespace Alias.Emulator.Network.Messages.Headers
{
	public class Incoming
	{
		// Users

			// Handshake
			public const int ReleaseVersionMessageEvent = 4000;
			public const int SecureLoginMessageEvent = 577;
			public const int MachineIDMessageEvent = 2503;
			public const int RequestUserDataMessageEvent = 3916;
			public const int InitCryptoMessageEvent = 2609; //unused
			public const int GenerateSecretKeyMessageEvent = 36; //unused
			public const int VersionCheckMessageEvent = 2721;
	}
}
