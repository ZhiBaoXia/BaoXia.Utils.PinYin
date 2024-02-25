using BaoXia.Utils.PinYin.ChinesePinYinInfesCodeCreator;

namespace BaoXia.Utils.PinYin.Test;

[TestClass]
public class ChinesePinYinOriginalInfoTest
{
	[TestMethod]
	public void GetPinYinInfoWithChineseCharacterTest()
	{
		////////////////////////////////////////////////
		// 1/2������ASCII���е������ַ���
		////////////////////////////////////////////////
		for (var asciiCharIndex = 0;
			asciiCharIndex < 256;
			asciiCharIndex++)
		{
			var asciiChar = (char)asciiCharIndex;
			var asciiCharacter = new string(asciiChar, 1);
			var asciiCharPinYinInfo = BaoXia.Utils.PinYin.ChinesePinYinInfo
				.GetPinYinInfoWithChineseCharacter(asciiCharacter);

			// 0~31��127(��33��)�ǿ����ַ���ͨ��ר���ַ�
			if (asciiChar >= 32
				&& asciiChar != 127)
			{
				// !!!
				Assert.IsNotNull(asciiCharPinYinInfo);
				// !!!

				// !!!
				Assert.IsTrue(asciiCharPinYinInfo.Value.PinYin == asciiCharacter);
				// !!!
			}
			else
			{
				// !!!
				Assert.IsNull(asciiCharPinYinInfo);
				// !!! 
			}
		}


		////////////////////////////////////////////////
		// 2/2�������ַ����е����к��֣�
		////////////////////////////////////////////////

		var chinesePinYinOriginalInfes
			= ChinesePinYinOriginalInfes.Infes;
		foreach (var chinesePinYinOriginalInfo in chinesePinYinOriginalInfes)
		{
			var testPinYinInfo = BaoXia.Utils.PinYin.ChinesePinYinInfo
				.GetPinYinInfoWithChineseCharacter(
				chinesePinYinOriginalInfo.ChineseCharacter);
			// !!!
			Assert.IsNotNull(testPinYinInfo);
			// !!!


			var originalPinYins = chinesePinYinOriginalInfo.PinYin.Split(
				',',
				StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

			// !!!
			Assert.IsTrue(testPinYinInfo.Value.PinYin == originalPinYins[0]);
			// !!!


			var originalPinYinWithSounds = chinesePinYinOriginalInfo.PinYinWithSound.Split(
				',',
				StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
			// !!!
			Assert.IsTrue(testPinYinInfo.Value.PinYinWithSound == originalPinYinWithSounds[0]);
			// !!!
		}
	}

	[TestMethod]
	public void GetPinYinInfesWithChineseCharacterTest()
	{
		var chinesePinYinOriginalInfes
			= ChinesePinYinOriginalInfes.Infes;
		foreach (var chinesePinYinOriginalInfo in chinesePinYinOriginalInfes)
		{
			var testPinYinInfes = BaoXia.Utils.PinYin.ChinesePinYinInfo
				.GetPinYinInfesWithChineseCharacter(
				chinesePinYinOriginalInfo.ChineseCharacter);
			// !!!
			Assert.IsNotNull(testPinYinInfes);
			// !!!


			var originalPinYins = chinesePinYinOriginalInfo.PinYin.Split(
				',',
				StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
			// !!!
			Assert.IsTrue(testPinYinInfes.Count == originalPinYins.Length);
			// !!!
			foreach (var testPinYinInfo in testPinYinInfes)
			{
				// !!!
				Assert.IsTrue(originalPinYins.Contains(testPinYinInfo.PinYin) == true);
				// !!!
			}


			var originalPinYinWithSounds = chinesePinYinOriginalInfo.PinYinWithSound.Split(
				',',
				StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
			// !!!
			Assert.IsTrue(testPinYinInfes.Count == originalPinYinWithSounds.Length);
			// !!!
			foreach (var testPinYinInfo in testPinYinInfes)
			{
				// !!!
				Assert.IsTrue(originalPinYinWithSounds.Contains(testPinYinInfo.PinYinWithSound) == true);
				// !!!
			}
		}
	}


	class TestItem
	{
		public string ChineseCharacter { get; set; }
		public string PinYin { get; set; }
		public string PinYinWithCommaSeparator { get; set; }
		public string PinYinWithFirstChar { get; set; }

		public TestItem(
			string chineseCharacter,
			string pinYin,
			string pinYinWithCommaSeparator,
			string pinYinWithFirstChar)
		{
			this.ChineseCharacter = chineseCharacter;
			this.PinYin = pinYin;
			this.PinYinWithCommaSeparator = pinYinWithCommaSeparator;
			this.PinYinWithFirstChar = pinYinWithFirstChar;
		}
	}

	[TestMethod]
	public void GetPinYinOfStringTest()
	{
		var testItems = new List<TestItem>()
		{
			new ("����",
				"HanZi",
				"Han,Zi",
				"HZ"),
			new ("HanZi����",
				"HanZiHanZi",
				"H,a,n,Z,i,Han,Zi",
				"HanZiHZ"),
			new ("����HanZi",
				"HanZiHanZi",
				"Han,Zi,H,a,n,Z,i",
				"HZHanZi"),
			new ("HanZi����HanZi",
				"HanZiHanZiHanZi",
				"H,a,n,Z,i,Han,Zi,H,a,n,Z,i",
				"HanZiHZHanZi"),
			new ("����HanZi����",
				"HanZiHanZiHanZi",
				"Han,Zi,H,a,n,Z,i,Han,Zi",
				"HZHanZiHZ"),
		};
		foreach (var testItem in testItems)
		{
			var pinYin = ChinesePinYinInfo.GetPinYinOfString(testItem.ChineseCharacter);
			// !!!
			Assert.IsTrue(testItem.PinYin.Equals(pinYin, StringComparison.OrdinalIgnoreCase));
			// !!!

			var pinYinWithCommaSeparator = ChinesePinYinInfo.GetPinYinOfString(testItem.ChineseCharacter, false, ",");
			// !!!
			Assert.IsTrue(testItem.PinYinWithCommaSeparator.Equals(pinYinWithCommaSeparator, StringComparison.OrdinalIgnoreCase));
			// !!!

			var pinYinWithFirstChar = ChinesePinYinInfo.GetPinYinOfString(testItem.ChineseCharacter, true);
			// !!!
			Assert.IsTrue(testItem.PinYinWithFirstChar.Equals(pinYinWithFirstChar, StringComparison.OrdinalIgnoreCase));
			// !!!
		}
	}
}