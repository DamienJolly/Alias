using System;

namespace Alias.Emulator.Hotel.Rooms.Users
{
	public class UserPosition
	{
		public int X
		{
			get; set;
		} = 0;

		public int Y
		{
			get; set;
		} = 0;

		public int Rotation
		{
			get; set;
		} = 0;

		public double Z
		{
			get; set;
		} = 0.0;

		public int HeadRotation
		{
			get; set;
		} = 0;

		public void CalculateRotation(int x, int y, bool headOnly = false)
		{
			int rotation = 0;
			if ((this.X > x) && (this.Y > y))
			{
				rotation = 7;
			}
			else if ((this.X < x) && (this.Y < y))
			{
				rotation = 3;
			}
			else if ((this.X > x) && (this.Y < y))
			{
				rotation = 5;
			}
			else if ((this.X < x) && (this.Y > y))
			{
				rotation = 1;
			}
			else if (this.X > x)
			{
				rotation = 6;
			}
			else if (this.X < x)
			{
				rotation = 2;
			}
			else if (this.Y < y)
			{
				rotation = 4;
			}
			else if (this.Y > y)
			{
				rotation = 0;
			}

			if (!headOnly)
			{
				this.Rotation = rotation;
			}

			if (Math.Abs(rotation - this.Rotation) <= 2)
			{
				this.HeadRotation = rotation;
			}
		}
	}
}
