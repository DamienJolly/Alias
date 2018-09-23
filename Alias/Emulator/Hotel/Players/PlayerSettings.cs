using System.Data.Common;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players
{
	internal class PlayerSettings
	{
		public int VolumeSystem { get; set; }
		public int VolumeFurni { get; set; }
		public int VolumeTrax { get; set; }
		public bool OldChat { get; set; }
		public bool IgnoreInvites { get; set; }
		public bool CameraFollow { get; set; }

		internal PlayerSettings(DbDataReader reader)
		{
			VolumeSystem  = reader.ReadData<int>("volume_system");
			VolumeFurni   = reader.ReadData<int>("volume_furni");
			VolumeTrax    = reader.ReadData<int>("volume_trax");
			OldChat       = reader.ReadBool("old_chat");
			IgnoreInvites = reader.ReadBool("ignore_invited");
			CameraFollow  = reader.ReadBool("camera_follow");
		}
	}
}
