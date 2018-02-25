namespace Alias.Emulator.Hotel.Rooms.Models
{
	public class RoomModel
	{
		public string Name
		{
			get; set;
		} = "model_";

		public int MaxUsers
		{
			get; set;
		} = 25;

		public string Map
		{
			get; set;
		} = "x00000\nx00000\n000000\nx00000\nx00000";

		public int MinRank
		{
			get; set;
		} = 1;

		public Door Door
		{
			get; set;
		} = Door.Parse("0,2,2");

		public bool ClubOnly
		{
			get; set;
		} = false;

		public void Initialize()
		{

		}
	}
}
