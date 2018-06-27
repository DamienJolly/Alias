namespace Alias.Emulator.Hotel.Users.Wardrobe
{
	static class FigureValidation
    {
		public static bool Validate(string figure, string gender)
		{
			System.Console.WriteLine(figure);
			if (figure.Length < 18 || figure.Length > 150)
			{
				return false;
			}

			string[] sets = figure.Split('.');

			if (sets.Length > 13 || sets.Length < 2)
			{
				return false;
			}

			foreach (string set in sets)
			{
				string[] parts = set.Split('-');

				if (parts.Length > 4 || parts.Length < 3)
				{
					return false;
				}

				if (!CheckTag(parts[0]) || !CheckType(parts[1]) || !CheckColour(parts[2]))
				{
					return false;
				}

				// second colour
				if (parts.Length == 4 && !CheckColour(parts[3]))
				{
					return false;
				}
			}

			if (!figure.Contains("hd") || !figure.Contains("lg") || (!figure.Contains("ch") && gender == "F"))
			{
				return false;
			}

			return true;
		}

		private static bool CheckTag(string tag)
		{
			if (tag.Length != 2)
			{
				return false;
			}

			System.Console.WriteLine(tag);

			switch (tag)
			{
				case "hd":
				case "hr":
				case "lg":
				case "ch":
				case "wa":
				case "cc":
				case "fa":
				case "ca":
				case "he":
				case "ea":
				case "cp":
				case "ha":
				case "sh":
					break;

				default:
					return false;
			}

			return true;
		}

		private static bool CheckType(string part)
		{
			System.Console.WriteLine(part);
			if (int.TryParse(part, out int type))
			{
				if (type < 1)
				{
					return false;
				}

				// todo:
			}
			else
			{
				return false;
			}

			return true;
		}

		private static bool CheckColour(string part)
		{
			System.Console.WriteLine(part);
			if (int.TryParse(part, out int colour))
			{
				if (colour < 1)
				{
					return false;
				}

				// todo:
			}
			else
			{
				return false;
			}

			return true;
		}
	}
}
