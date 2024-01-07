namespace BaoXia.Utils.PinYin
{
	public struct ChinesePinYinInfo
	{

		////////////////////////////////////////////////
		// @自身属性
		////////////////////////////////////////////////

		#region 自身属性

		public int ChineseCharacterIndex { get; set; }

		public string ChineseCharacter { get; set; }

		public string PinYin { get; set; }

		public string PinYinWithSound { get; set; }

		#endregion


		////////////////////////////////////////////////
		// @类方法
		////////////////////////////////////////////////

		#region 类方法

		/// <summary>
		/// 获取指定汉字的拼音信息。
		/// </summary>
		/// <param name="objectChineseCharacter">指定的汉字字符，生僻字可能占用2个字符。</param>
		/// <param name="charsEndSymbol">字符终止符号。</param>
		/// <param name="allPinYins">指定的全部拼音表。</param>
		/// <param name="pinYinUnitLength">指定的全部拼音表的拼音单元字符长度。</param>
		/// <param name="allPinYinWithSounds">指定的全部拼音（含音标）表。</param>
		/// <param name="pinYinWithSoundUnitLength">指定的全部拼音表（含音标）的拼音单元字符长度。</param>
		/// <param name="allChineseChars">全部的汉字表。</param>
		/// <param name="chineseCharUnitLength">全部汉字表的单元字符长度。</param>
		/// <param name="allChineseCharacterPinYinIndexes">全部汉字拼音索引表。</param>
		/// <param name="allChineseCharacterPinYinWithSoundIndexes">全部汉字拼音（含音标）索引表。</param>
		/// <returns>返回指定的汉字字符对应的拼音信息。</returns>
		public static ChinesePinYinInfo? GetPinYinInfoWithChineseCharacter(
			string objectChineseCharacter,
			//
			char charsEndSymbol,
			//
			string allPinYins,
			int pinYinUnitLength,
			string allPinYinWithSounds,
			int pinYinWithSoundUnitLength,

			//
			string allChineseChars,
			int chineseCharUnitLength,
			//
			short[] allChineseCharacterPinYinIndexes,
			short[] allChineseCharacterPinYinWithSoundIndexes)
		{
			if (string.IsNullOrEmpty(objectChineseCharacter))
			{
				return null;
			}
			var objectChineseCharacterLength = objectChineseCharacter.Length;
			if (objectChineseCharacterLength == 1)
			{
				// “ASCII”码快速返回：
				var objectChineseCharacterFirst = objectChineseCharacter[0];
				if (objectChineseCharacterFirst >= 0
					&& objectChineseCharacterFirst <= 255)
				{
					// 0~31及127(共33个)是控制字符或通信专用字符
					if (objectChineseCharacterFirst >= 32
						&& objectChineseCharacterFirst != 127)
					{
						return new ChinesePinYinInfo(
							-1,
							objectChineseCharacter,
							objectChineseCharacter,
							objectChineseCharacter);
					}
					return null;
				}
			}



			var searchRangeBeginUnitIndex = 0;
			var searchRangeEndUnitIndex = allChineseChars.Length / chineseCharUnitLength;
			var chineseCharUnitIndexMatched = -1;
			while (searchRangeEndUnitIndex > searchRangeBeginUnitIndex)
			{
				var searchRangeLength
					= searchRangeEndUnitIndex - searchRangeBeginUnitIndex;
				var searchShotUnitIndex
					= searchRangeBeginUnitIndex
					+ searchRangeLength / 2;

				var compareResultOfChineseCharacterToObject = 0;
				var chineseCharUnitShotBeginIndex = chineseCharUnitLength * searchShotUnitIndex;
				var chineseCharLength = 0;
				for (var chineseCharIndex = 0;
				       chineseCharIndex < chineseCharUnitLength;
				       chineseCharIndex++)
				{
					var chineseChar = allChineseChars[chineseCharUnitShotBeginIndex + chineseCharIndex];
					if (chineseChar == charsEndSymbol)
					{
						break;
					}
					// !!!
					chineseCharLength++;
					// !!!
				}

				if (chineseCharLength > objectChineseCharacterLength)
				{
					compareResultOfChineseCharacterToObject = 1;
				}
				else if (chineseCharLength < objectChineseCharacterLength)
				{
					compareResultOfChineseCharacterToObject = -1;
				}
				else // if (chineseCharLength == objectChineseCharacterLength)
				{
					for (var chineseCharIndex = 0;
					chineseCharIndex < chineseCharLength;
					chineseCharIndex++)
					{
						var chineseChar = allChineseChars[chineseCharUnitShotBeginIndex + chineseCharIndex];
						var objectChineseCharacterChar = objectChineseCharacter[chineseCharIndex];
						if (chineseChar != objectChineseCharacterChar)
						{
							compareResultOfChineseCharacterToObject
								= chineseChar < objectChineseCharacterChar
								? -1
								: 1;
							break;
						}
					}
				}

				if (compareResultOfChineseCharacterToObject == 0)
				{
					// !!!
					chineseCharUnitIndexMatched = searchShotUnitIndex;
					// !!!
					break;
				}
				else if (searchRangeLength == 1)
				{
					break;
				}
				else if (compareResultOfChineseCharacterToObject < 0)
				{
					searchRangeBeginUnitIndex = searchShotUnitIndex;
					// searchRangeEndUnitIndex = searchRangeEndIndex;
				}
				else if (compareResultOfChineseCharacterToObject > 0)
				{
					// searchRangeBeginUnitIndex = searchRangeBeginIndex;
					searchRangeEndUnitIndex = searchShotUnitIndex;
				}
			}
			if (chineseCharUnitIndexMatched < 0)
			{
				return null;
			}



			string? chineseCharPinYin = null;
			if (chineseCharUnitIndexMatched
				< allChineseCharacterPinYinIndexes.Length)
			{
				var chineseCharPinYinIndex = allChineseCharacterPinYinIndexes[chineseCharUnitIndexMatched];
				if (chineseCharPinYinIndex >= 0)
				{
					var chineseCharPinYinCharIndex = pinYinUnitLength * chineseCharPinYinIndex;
					if (chineseCharPinYinCharIndex < allPinYins.Length)
					{
						chineseCharPinYin = allPinYins.Substring(
							chineseCharPinYinCharIndex,
							pinYinUnitLength)
							.TrimEnd();
					}
				}
			}



			string? chineseCharPinYinWithSound = null;
			if (chineseCharUnitIndexMatched
				< allChineseCharacterPinYinWithSoundIndexes.Length)
			{
				var chineseCharPinYinWithSoundIndex = allChineseCharacterPinYinWithSoundIndexes[chineseCharUnitIndexMatched];
				if (chineseCharPinYinWithSoundIndex >= 0)
				{
					var chineseCharPinYinWithSoundCharIndex = pinYinWithSoundUnitLength * chineseCharPinYinWithSoundIndex;
					if (chineseCharPinYinWithSoundCharIndex < allPinYinWithSounds.Length)
					{
						chineseCharPinYinWithSound = allPinYinWithSounds.Substring(
							chineseCharPinYinWithSoundCharIndex,
							pinYinWithSoundUnitLength)
							.TrimEnd();
					}
				}
			}


			var chinesePinYinInfo = new ChinesePinYinInfo(
				chineseCharUnitIndexMatched,
				objectChineseCharacter,
				chineseCharPinYin!,
				chineseCharPinYinWithSound!);
			{ }
			return chinesePinYinInfo;
		}

		/// <summary>
		/// 获取指定汉字的常用拼音信息。
		/// </summary>
		/// <param name="objectChineseCharacter">指定的汉字字符，生僻字可能占用2个字符。</param>
		/// <returns>获取指定汉字的常用拼音信息。</returns>
		public static ChinesePinYinInfo? GetPinYinInfoWithChineseCharacter(
			string objectChineseCharacter)
		{
			return ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(
				objectChineseCharacter,
				//
				ChinesePinYinInfes_01.CharsEndSymbol,
				//
				ChinesePinYinInfes_01.AllPinYins,
				ChinesePinYinInfes_01.PinYinUnitLength,
				ChinesePinYinInfes_01.AllPinYinWithSounds,
				ChinesePinYinInfes_01.PinYinWithSoundUnitLength,
				//
				ChinesePinYinInfes_01.AllChineseCharacters,
				ChinesePinYinInfes_01.ChineseCharacterUnitLength,
				//
				ChinesePinYinInfes_01.AllChineseCharacterPinYinIndexes,
				ChinesePinYinInfes_01.AllChineseCharacterPinYinWithSoundIndexes);
		}

		/// <summary>
		/// 获取指定汉字的全部拼音信息。
		/// </summary>
		/// <param name="objectChineseCharacter">指定的汉字字符，生僻字可能占用2个字符。</param>
		/// <returns>获取指定汉字的全部拼音信息。</returns>
		public static List<ChinesePinYinInfo>? GetPinYinInfesWithChineseCharacter(
			string objectChineseCharacter)
		{
			List<ChinesePinYinInfo>? pinYinInfes = null;
			{
				var pinYinInfo_01 = ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(
					objectChineseCharacter,
					//
					ChinesePinYinInfes_01.CharsEndSymbol,
					//
					ChinesePinYinInfes_01.AllPinYins,
					ChinesePinYinInfes_01.PinYinUnitLength,
					ChinesePinYinInfes_01.AllPinYinWithSounds,
					ChinesePinYinInfes_01.PinYinWithSoundUnitLength,
					//
					ChinesePinYinInfes_01.AllChineseCharacters,
					ChinesePinYinInfes_01.ChineseCharacterUnitLength,
					//
					ChinesePinYinInfes_01.AllChineseCharacterPinYinIndexes,
					ChinesePinYinInfes_01.AllChineseCharacterPinYinWithSoundIndexes);
				if (pinYinInfo_01 != null)
				{
					if (pinYinInfes == null)
					{
						pinYinInfes = new List<ChinesePinYinInfo>();
					}
					pinYinInfes.Add(pinYinInfo_01.Value);
				}

				var pinYinInfo_02 = ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(
					objectChineseCharacter,
					//
					ChinesePinYinInfes_02.CharsEndSymbol,
					//
					ChinesePinYinInfes_02.AllPinYins,
					ChinesePinYinInfes_02.PinYinUnitLength,
					ChinesePinYinInfes_02.AllPinYinWithSounds,
					ChinesePinYinInfes_02.PinYinWithSoundUnitLength,
					//
					ChinesePinYinInfes_02.AllChineseCharacters,
					ChinesePinYinInfes_02.ChineseCharacterUnitLength,
					//
					ChinesePinYinInfes_02.AllChineseCharacterPinYinIndexes,
					ChinesePinYinInfes_02.AllChineseCharacterPinYinWithSoundIndexes);
				if (pinYinInfo_02 != null)
				{
					if (pinYinInfes == null)
					{
						pinYinInfes = new List<ChinesePinYinInfo>();
					}
					pinYinInfes.Add(pinYinInfo_02.Value);
				}

				var pinYinInfo_03 = ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(
					objectChineseCharacter,
					//
					ChinesePinYinInfes_03.CharsEndSymbol,
					//
					ChinesePinYinInfes_03.AllPinYins,
					ChinesePinYinInfes_03.PinYinUnitLength,
					ChinesePinYinInfes_03.AllPinYinWithSounds,
					ChinesePinYinInfes_03.PinYinWithSoundUnitLength,
					//
					ChinesePinYinInfes_03.AllChineseCharacters,
					ChinesePinYinInfes_03.ChineseCharacterUnitLength,
					//
					ChinesePinYinInfes_03.AllChineseCharacterPinYinIndexes,
					ChinesePinYinInfes_03.AllChineseCharacterPinYinWithSoundIndexes);
				if (pinYinInfo_03 != null)
				{
					if (pinYinInfes == null)
					{
						pinYinInfes = new List<ChinesePinYinInfo>();
					}
					pinYinInfes.Add(pinYinInfo_03.Value);
				}

				var pinYinInfo_04 = ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(
					objectChineseCharacter,
					//
					ChinesePinYinInfes_04.CharsEndSymbol,
					//
					ChinesePinYinInfes_04.AllPinYins,
					ChinesePinYinInfes_04.PinYinUnitLength,
					ChinesePinYinInfes_04.AllPinYinWithSounds,
					ChinesePinYinInfes_04.PinYinWithSoundUnitLength,
					//
					ChinesePinYinInfes_04.AllChineseCharacters,
					ChinesePinYinInfes_04.ChineseCharacterUnitLength,
					//
					ChinesePinYinInfes_04.AllChineseCharacterPinYinIndexes,
					ChinesePinYinInfes_04.AllChineseCharacterPinYinWithSoundIndexes);
				if (pinYinInfo_04 != null)
				{
					if (pinYinInfes == null)
					{
						pinYinInfes = new List<ChinesePinYinInfo>();
					}
					pinYinInfes.Add(pinYinInfo_04.Value);
				}

				var pinYinInfo_05 = ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(
					objectChineseCharacter,
					//
					ChinesePinYinInfes_05.CharsEndSymbol,
					//
					ChinesePinYinInfes_05.AllPinYins,
					ChinesePinYinInfes_05.PinYinUnitLength,
					ChinesePinYinInfes_05.AllPinYinWithSounds,
					ChinesePinYinInfes_05.PinYinWithSoundUnitLength,
					//
					ChinesePinYinInfes_05.AllChineseCharacters,
					ChinesePinYinInfes_05.ChineseCharacterUnitLength,
					//
					ChinesePinYinInfes_05.AllChineseCharacterPinYinIndexes,
					ChinesePinYinInfes_05.AllChineseCharacterPinYinWithSoundIndexes);
				if (pinYinInfo_05 != null)
				{
					if (pinYinInfes == null)
					{
						pinYinInfes = new List<ChinesePinYinInfo>();
					}
					pinYinInfes.Add(pinYinInfo_05.Value);
				}

				var pinYinInfo_06 = ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(
					objectChineseCharacter,
					//
					ChinesePinYinInfes_06.CharsEndSymbol,
					//
					ChinesePinYinInfes_06.AllPinYins,
					ChinesePinYinInfes_06.PinYinUnitLength,
					ChinesePinYinInfes_06.AllPinYinWithSounds,
					ChinesePinYinInfes_06.PinYinWithSoundUnitLength,
					//
					ChinesePinYinInfes_06.AllChineseCharacters,
					ChinesePinYinInfes_06.ChineseCharacterUnitLength,
					//
					ChinesePinYinInfes_06.AllChineseCharacterPinYinIndexes,
					ChinesePinYinInfes_06.AllChineseCharacterPinYinWithSoundIndexes);
				if (pinYinInfo_06 != null)
				{
					if (pinYinInfes == null)
					{
						pinYinInfes = new List<ChinesePinYinInfo>();
					}
					pinYinInfes.Add(pinYinInfo_06.Value);
				}

				var pinYinInfo_07 = ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(
					objectChineseCharacter,
					//
					ChinesePinYinInfes_07.CharsEndSymbol,
					//
					ChinesePinYinInfes_07.AllPinYins,
					ChinesePinYinInfes_07.PinYinUnitLength,
					ChinesePinYinInfes_07.AllPinYinWithSounds,
					ChinesePinYinInfes_07.PinYinWithSoundUnitLength,
					//
					ChinesePinYinInfes_07.AllChineseCharacters,
					ChinesePinYinInfes_07.ChineseCharacterUnitLength,
					//
					ChinesePinYinInfes_07.AllChineseCharacterPinYinIndexes,
					ChinesePinYinInfes_07.AllChineseCharacterPinYinWithSoundIndexes);
				if (pinYinInfo_07 != null)
				{
					if (pinYinInfes == null)
					{
						pinYinInfes = new List<ChinesePinYinInfo>();
					}
					pinYinInfes.Add(pinYinInfo_07.Value);
				}

				var pinYinInfo_08 = ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(
					objectChineseCharacter,
					//
					ChinesePinYinInfes_08.CharsEndSymbol,
					//
					ChinesePinYinInfes_08.AllPinYins,
					ChinesePinYinInfes_08.PinYinUnitLength,
					ChinesePinYinInfes_08.AllPinYinWithSounds,
					ChinesePinYinInfes_08.PinYinWithSoundUnitLength,
					//
					ChinesePinYinInfes_08.AllChineseCharacters,
					ChinesePinYinInfes_08.ChineseCharacterUnitLength,
					//
					ChinesePinYinInfes_08.AllChineseCharacterPinYinIndexes,
					ChinesePinYinInfes_08.AllChineseCharacterPinYinWithSoundIndexes);
				if (pinYinInfo_08 != null)
				{
					if (pinYinInfes == null)
					{
						pinYinInfes = new List<ChinesePinYinInfo>();
					}
					pinYinInfes.Add(pinYinInfo_08.Value);
				}

				var pinYinInfo_09 = ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(
					objectChineseCharacter,
					//
					ChinesePinYinInfes_09.CharsEndSymbol,
					//
					ChinesePinYinInfes_09.AllPinYins,
					ChinesePinYinInfes_09.PinYinUnitLength,
					ChinesePinYinInfes_09.AllPinYinWithSounds,
					ChinesePinYinInfes_09.PinYinWithSoundUnitLength,
					//
					ChinesePinYinInfes_09.AllChineseCharacters,
					ChinesePinYinInfes_09.ChineseCharacterUnitLength,
					//
					ChinesePinYinInfes_09.AllChineseCharacterPinYinIndexes,
					ChinesePinYinInfes_09.AllChineseCharacterPinYinWithSoundIndexes);
				if (pinYinInfo_09 != null)
				{
					if (pinYinInfes == null)
					{
						pinYinInfes = new List<ChinesePinYinInfo>();
					}
					pinYinInfes.Add(pinYinInfo_09.Value);
				}

				var pinYinInfo_10 = ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(
					objectChineseCharacter,
					//
					ChinesePinYinInfes_10.CharsEndSymbol,
					//
					ChinesePinYinInfes_10.AllPinYins,
					ChinesePinYinInfes_10.PinYinUnitLength,
					ChinesePinYinInfes_10.AllPinYinWithSounds,
					ChinesePinYinInfes_10.PinYinWithSoundUnitLength,
					//
					ChinesePinYinInfes_10.AllChineseCharacters,
					ChinesePinYinInfes_10.ChineseCharacterUnitLength,
					//
					ChinesePinYinInfes_10.AllChineseCharacterPinYinIndexes,
					ChinesePinYinInfes_10.AllChineseCharacterPinYinWithSoundIndexes);
				if (pinYinInfo_10 != null)
				{
					if (pinYinInfes == null)
					{
						pinYinInfes = new List<ChinesePinYinInfo>();
					}
					pinYinInfes.Add(pinYinInfo_10.Value);
				}

				var pinYinInfo_11 = ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(
					objectChineseCharacter,
					//
					ChinesePinYinInfes_11.CharsEndSymbol,
					//
					ChinesePinYinInfes_11.AllPinYins,
					ChinesePinYinInfes_11.PinYinUnitLength,
					ChinesePinYinInfes_11.AllPinYinWithSounds,
					ChinesePinYinInfes_11.PinYinWithSoundUnitLength,
					//
					ChinesePinYinInfes_11.AllChineseCharacters,
					ChinesePinYinInfes_11.ChineseCharacterUnitLength,
					//
					ChinesePinYinInfes_11.AllChineseCharacterPinYinIndexes,
					ChinesePinYinInfes_11.AllChineseCharacterPinYinWithSoundIndexes);
				if (pinYinInfo_11 != null)
				{
					if (pinYinInfes == null)
					{
						pinYinInfes = new List<ChinesePinYinInfo>();
					}
					pinYinInfes.Add(pinYinInfo_11.Value);
				}
			}
			return pinYinInfes;
		}

		/// <summary>
		/// 获取在字符串中指定位置上的汉字的拼音信息。
		/// </summary>
		/// <param name="str">指定的字符串。</param>
		/// <param name="charIndex">要获取拼音信息的汉字的索引值。</param>
		/// <param name="chineseCharacterCharLength">返回拼音信息对应的中文字符的长度，某些生僻字需要2个字符。</param>
		/// <returns>返回在字符串中指定位置上的汉字的拼音信息。</returns>
		public static ChinesePinYinInfo? GetPinYinInfoOfChineseCharacterAtIndexInString(
			string? str,
			int charIndex,
			out int chineseCharacterCharLength)
		{
			chineseCharacterCharLength = 0;

			if (string.IsNullOrEmpty(str))
			{
				return null;
			}
			if (charIndex < 0
				|| charIndex >= str.Length)
			{
				return null;
			}

			var chineseCharacterBeginCharIndex = charIndex;
			for (var chineseCharacterCharIndex = 0;
				chineseCharacterCharIndex < ChinesePinYinInfes_01.ChineseCharacterUnitLength;
				chineseCharacterCharIndex++)
			{
				if ((charIndex + chineseCharacterCharIndex) >= str.Length)
				{
					return null;
				}

				var chineseCharacterCharLengthToTryGetPinYinInfo
					= (chineseCharacterCharIndex + 1);
				var chineseCharacter = str.Substring(
					chineseCharacterBeginCharIndex,
					chineseCharacterCharLengthToTryGetPinYinInfo);

				var pinYinYinfo = ChinesePinYinInfo.GetPinYinInfoWithChineseCharacter(chineseCharacter);
				if (pinYinYinfo != null)
				{
					// !!!
					chineseCharacterCharLength = chineseCharacterCharLengthToTryGetPinYinInfo;
					// !!!
					return pinYinYinfo;
				}
			}
			return null;
		}

		/// <summary>
		/// 获取在字符串中指定位置上的汉字的全部拼音信息。
		/// </summary>
		/// <param name="str">指定的字符串。</param>
		/// <param name="charIndex">要获取拼音信息的汉字的索引值。</param>
		/// <param name="chineseCharacterCharLength">返回拼音信息对应的中文字符的长度，某些生僻字需要2个字符。</param>
		/// <returns>返回在字符串中指定位置上的汉字的全部拼音信息。</returns>
		public static List<ChinesePinYinInfo>? GetPinYinInfesOfChineseCharacterAtIndexInString(
			string? str,
			int charIndex,
			out int chineseCharacterCharLength)
		{
			chineseCharacterCharLength = 0;

			if (string.IsNullOrEmpty(str))
			{
				return null;
			}
			if (charIndex < 0
				|| charIndex >= str.Length)
			{
				return null;
			}

			var chineseCharacterBeginCharIndex = charIndex;
			for (var chineseCharacterCharIndex = 0;
				chineseCharacterCharIndex < ChinesePinYinInfes_01.ChineseCharacterUnitLength;
				chineseCharacterCharIndex++)
			{
				if ((charIndex + chineseCharacterCharIndex) >= str.Length)
				{
					return null;
				}

				var chineseCharacterCharLengthToTryGetPinYinInfo
					= (chineseCharacterCharIndex + 1);
				var chineseCharacter = str.Substring(
					chineseCharacterBeginCharIndex,
					chineseCharacterCharLengthToTryGetPinYinInfo);

				var pinYinYinfes = ChinesePinYinInfo.GetPinYinInfesWithChineseCharacter(chineseCharacter);
				if (pinYinYinfes != null)
				{
					// !!!
					chineseCharacterCharLength = chineseCharacterCharLengthToTryGetPinYinInfo;
					// !!!
					return pinYinYinfes;
				}
			}
			return null;
		}

		/// <summary>
		/// 获取指定字符串的拼音信息列表，会自动识别多字符的汉字。
		/// </summary>
		/// <param name="str">指定的字符串。</param>
		/// <returns>返回指定字符串的拼音信息列表，会自动识别多字符的汉字。</returns>
		public static List<List<ChinesePinYinInfo>>? GetPinYinInfesOfString(
			string? str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return null;
			}

			var pinYinInfes = new List<List<ChinesePinYinInfo>>();
			var stringLength = str.Length;
			for (var charIndex = 0;
				charIndex < stringLength;)
			{
				if (ChinesePinYinInfo.GetPinYinInfesOfChineseCharacterAtIndexInString(
					str,
					charIndex,
					out var chineseCharacterCharLength)
					is
					List<ChinesePinYinInfo> chineseCharacterPinYinInfo)
				{
					pinYinInfes.Add(chineseCharacterPinYinInfo);

					charIndex
						+= chineseCharacterCharLength > 0
						? chineseCharacterCharLength
						: 1;
				}
				else
				{
					charIndex++;
				}
			}
			return pinYinInfes;
		}

		/// <summary>
		/// 获取指定字符串的拼音字符串，会自动识别多字符的汉字。
		/// </summary>
		/// <param name="str">指定的字符串。</param>
		/// <param name="isGetPinYinFirstCharOnly">是否只获取每个拼音的首字母。</param>
		/// <param name="pinYinSeparator">每个拼音字符串之间的分隔符，默认为不分隔。</param>
		/// <returns>返回指定字符串的拼音字符串，会自动识别多字符的汉字。</returns>
		public static string? GetPinYinOfString(
			string? str,
			bool isGetPinYinFirstCharOnly = false,
			string? pinYinSeparator = null)
		{
			if (string.IsNullOrEmpty(str))
			{
				return null;
			}

			string? strPinYin = null;
			var stringLength = str.Length;
			for (var charIndex = 0;
				charIndex < stringLength;)
			{
				if (ChinesePinYinInfo.GetPinYinInfoOfChineseCharacterAtIndexInString(
					str,
					charIndex,
					out var chineseCharacterCharLength)
					is
					ChinesePinYinInfo chineseCharacterPinYinInfo)
				{
					var charPinYin = chineseCharacterPinYinInfo.PinYin;
					if (isGetPinYinFirstCharOnly
						&& charPinYin.Length > 0)
					{
						charPinYin = charPinYin.Substring(0, 1);
					}

					if (strPinYin == null
						|| strPinYin.Length < 1)
					{
						strPinYin = charPinYin;
					}
					else
					{
						if (pinYinSeparator?.Length > 0)
						{
							strPinYin += pinYinSeparator;
						}
						strPinYin += charPinYin;
					}
					charIndex
						+= chineseCharacterCharLength > 0
						? chineseCharacterCharLength
						: 1;
				}
				else
				{
					charIndex++;
				}
			}
			return strPinYin;
		}


		#endregion


		////////////////////////////////////////////////
		// @自身实现
		////////////////////////////////////////////////

		#region 自身实现

		public ChinesePinYinInfo(
			int chineseCharacterIndex,
			string chineseCharacter,
			string pinYin,
			string pinYinWithSound)
		{
			this.ChineseCharacterIndex = chineseCharacterIndex;
			this.ChineseCharacter = chineseCharacter;
			this.PinYin = pinYin;
			this.PinYinWithSound = pinYinWithSound;
		}

		#endregion

	}
}
