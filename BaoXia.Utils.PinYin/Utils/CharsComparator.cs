namespace BaoXia.Utils.PinYin.Utils
{
	public class CharsComparator
	{
		/// <summary>
		/// 字符串比较规则：首先，按长度由短到长，其次，按字符数值由小到大。
		/// </summary>
		/// <param name="charsA">指定的字符数组A。</param>
		/// <param name="charsB">指定的字符数组B。</param>
		/// <returns>返回值小于“0”时，指定的字符数组A应在指定的字符数组B之前，
		/// 返回值大于“0”时，指定的字符数组A应在指定的字符数组B之后，
		/// 返回值等于“0”时，指定的字符数组A和指定的字符数组B应当在同一未知。</returns>
		public static int CompareChars(char[] charsA, char[] charsB)
		{
			if (charsA == charsB)
			{
				return 0;
			}

			var charsALength = charsA?.Length ?? 0;
			var charsBLength = charsB?.Length ?? 0;

			if (charsALength > charsBLength)
			{
				return 1;
			}
			else if (charsALength < charsBLength)
			{
				return -1;
			}
			else if (charsA != null
				&& charsB != null)
			{
				for (var charIndex = 0;
					charIndex < charsALength;
					charIndex++)
				{
					var charA = charsA[charIndex];
					var charB = charsB[charIndex];
					if (charA > charB)
					{
						return 1;
					}
					else if (charA < charB)
					{
						return -1;
					}
				}
			}
			return 0;
		}
	}
}
