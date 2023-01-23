namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
			switch (count / 10 % 10 == 1 ? 0 : count % 10)
			{
				case 1: return "рубль"; break;

				case 2: return "рубля"; break;

				case 3: return "рубля"; break;

				case 4: return "рубля"; break;

				default: return "рублей"; break;
			}
		}
	}
}