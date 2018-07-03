namespace Alias.Emulator.Hotel.Groups
{
    class GroupMember
    {
		public int UserId
		{
			get; set;
		}

		public string Username
		{
			get; set;
		}

		public string Look
		{
			get; set;
		}

		public int JoinData
		{
			get; set;
		}

		public GroupRank Rank
		{
			get; set;
		}

		public GroupMember(int userId, string username, string look, int joinDate, int rank)
		{
			this.UserId = userId;
			this.Username = username;
			this.Look = look;
			this.JoinData = joinDate;
			this.Rank = (GroupRank)rank;
		}
	}
}
