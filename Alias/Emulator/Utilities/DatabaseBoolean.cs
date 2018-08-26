using System;

namespace Alias.Emulator.Utilities
{
	static class DatabaseBoolean
    {
		/// <summary>
		/// Converts a boolean into a string.
		/// </summary>
		/// <param name="x">Value to be converted into string.</param>
		public static string GetStringFromBool(bool x)
		{
			return x ? "1" : "0";
		}

		public static bool GetBoolFromString(string x)
		{
			return x == "1" ? true : false;
		}
	}
}
