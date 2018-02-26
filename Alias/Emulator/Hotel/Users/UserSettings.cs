namespace Alias.Emulator.Hotel.Users
{
	public class UserSettings
	{
		public int VolumeSystem
		{
			get; set;
		} = 100;

		public int VolumeFurni
		{
			get; set;
		} = 100;

		public int VolumeTrax
		{
			get; set;
		} = 100;

		public bool OldChat
		{
			get; set;
		} = false;

		public bool IgnoreInvites
		{
			get; set;
		} = false;

		public bool CameraFollow
		{
			get; set;
		} = false;
	}
}
