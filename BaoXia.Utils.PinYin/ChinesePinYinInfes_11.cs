using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoXia.Utils.PinYin
{
	public class ChinesePinYinInfes_11
	{

		public const char CharsEndSymbol = ' ';

		public const int PinYinUnitLength = 2;
		public static readonly string AllPinYins = "ei";

		public const int PinYinWithSoundUnitLength = 2;
		public static readonly string AllPinYinWithSounds = "ēi";

		public const int ChineseCharacterUnitLength = 1;
		public static readonly string AllChineseCharacters = "欸";
		public static readonly short[] AllChineseCharacterPinYinIndexes = new short[]{0};
		public static readonly short[] AllChineseCharacterPinYinWithSoundIndexes = new short[]{0};


	}
}