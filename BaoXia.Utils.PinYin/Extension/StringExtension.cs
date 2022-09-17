using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoXia.Utils.PinYin.Extension
{
        public static class StringExtension
        {

                /// <summary>
                /// 将当前字符串，转为对应的拼音字符串列表。
                /// </summary>
                /// <param name="str">当前字符串。</param>
                /// <returns>当前字符串对应的拼音字符串列表。</returns>
                public static List<List<string>>? ToPinYinsList(this string str)
                {
                        if (str.Length < 1)
                        {
                                return null;
                        }

                        // 记录每个字符的全拼
                        List<List<string>> pinYinsList = new();
                        /*
                        for (int charIndex = 0;
                            charIndex < str.Length;
                            charIndex++)
                        {
                                var character = str[charIndex];
                                var charPinYins = new List<string>();
                                if (ChineseChar.IsValidChar(character))
                                {
                                        var chineseChar = new ChineseChar(character);
                                        if (chineseChar.Pinyins?.Count > 0)
                                        {
                                                foreach (var chineseCharPinYin in chineseChar.Pinyins)
                                                {
                                                        if (!string.IsNullOrWhiteSpace(chineseCharPinYin))
                                                        {
                                                                var charPinYin = chineseCharPinYin;
                                                                while (charPinYin.Length > 0)
                                                                {
                                                                        var lastPinYinChar = charPinYin[^1];
                                                                        // ⚠ 去除声调标记 ⚠
                                                                        if (lastPinYinChar >= '0'
                                                                            && lastPinYinChar <= '9')
                                                                        {
                                                                                charPinYin = charPinYin[0..^1];
                                                                        }
                                                                        else
                                                                        {
                                                                                break;
                                                                        }
                                                                }
                                                                // !!!
                                                                if (!charPinYins.Contains(charPinYin))
                                                                {
                                                                        charPinYins.Add(charPinYin);
                                                                }
                                                                // !!!
                                                        }
                                                }
                                        }
                                }
                                else
                                {
                                        // !!!
                                        charPinYins.Add(character.ToString());
                                        // !!!
                                }
                                // !!!
                                pinYinsList.Add(charPinYins);
                                // !!!
                        }
                        */
                        return pinYinsList;
                }


                /// <summary>
                /// 将当前字符串，转为对应的拼音字符串。
                /// </summary>
                /// <param name="str">当前字符串。</param>
                /// <returns>当前字符串对应的拼音字符串。</returns>
                public static string? ToPinYin(this string str)
                {
                        string? pinYin = null;
                        var pinYinsList = str.ToPinYinsList();
                        if (pinYinsList?.Count > 0)
                        {
                                foreach (var pinYins in pinYinsList)
                                {
                                        if (pinYins.Count > 0)
                                        {
                                                var pinYinChars = pinYins[0];
                                                var finalPinYinChars = new char[pinYinChars.Length];
                                                for (var pinYinCharIndex = 0;
                                                    pinYinCharIndex < pinYinChars.Length;
                                                    pinYinCharIndex++)
                                                {
                                                        var pinYinChar = pinYinChars[pinYinCharIndex];
                                                        if (pinYinCharIndex == 0)
                                                        {
                                                                if (pinYinChar >= 'a'
                                                                    && pinYinChar <= 'z')
                                                                {
                                                                        pinYinChar = (char)(pinYinChar
                                                                            + ('A' - 'a'));
                                                                }
                                                        }
                                                        else
                                                        {
                                                                if (pinYinChar >= 'A'
                                                                    && pinYinChar <= 'Z')
                                                                {
                                                                        pinYinChar = (char)(pinYinChar
                                                                            + ('a' - 'A'));
                                                                }
                                                        }
                                                        //
                                                        finalPinYinChars[pinYinCharIndex] = pinYinChar;
                                                        //
                                                }
                                                // !!!
                                                pinYin += new string(finalPinYinChars);
                                                // !!!
                                        }
                                }
                        }
                        return pinYin;
                }

                /// <summary>
                /// 将当前字符串，转为对应的首字母拼音字符串。
                /// </summary>
                /// <param name="str">当前字符串。</param>
                /// <param name="isToUpperCase">是否返回大写字母的拼音字符串。</param>
                /// <returns>当前字符串对应的首字母拼音字符串。</returns>
                public static string? ToPinYinWithFirstCharacter(
                    this string str,
                    bool isToUpperCase = true)
                {
                        string? pinYin = null;
                        var pinYinsList = str.ToPinYinsList();
                        if (pinYinsList?.Count > 0)
                        {
                                foreach (var pinYins in pinYinsList)
                                {
                                        if (pinYins.Count > 0)
                                        {
                                                var pinYinChars = pinYins[0];
                                                if (pinYinChars.Length > 0)
                                                {
                                                        var pinYinChar = pinYinChars[0];
                                                        if (isToUpperCase)
                                                        {
                                                                if (pinYinChar >= 'a'
                                                                    && pinYinChar <= 'z')
                                                                {
                                                                        pinYinChar = (char)(pinYinChar
                                                                            + ('A' - 'a'));
                                                                }
                                                        }
                                                        else
                                                        {
                                                                if (pinYinChar >= 'A'
                                                                    && pinYinChar <= 'Z')
                                                                {
                                                                        pinYinChar = (char)(pinYinChar
                                                                            + ('a' - 'A'));
                                                                }
                                                        }
                                                        // !!!
                                                        pinYin += pinYinChar;
                                                        // !!!
                                                }
                                        }
                                }
                        }
                        return pinYin;
                }

        }
}
