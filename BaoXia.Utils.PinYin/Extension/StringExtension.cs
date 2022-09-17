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
                public static List<List<string>>? ToPinYinsList(
                        this string str)
                {
                        if (str.Length < 1)
                        {
                                return null;
                        }


                        var pinYinInfoLists = ChinesePinYinInfo.GetPinYinInfesOfString(str);
                        if (pinYinInfoLists == null
                                || pinYinInfoLists.Count < 1)
                        {
                                return null;
                        }

                        // 记录每个字符的全拼
                        List<List<string>> pinYinsList = new();
                        foreach (var pinYinInfoList in pinYinInfoLists)
                        {
                                if (pinYinInfoList == null)
                                {
                                        continue;
                                }

                                var pinYins = new List<string>();
                                foreach (var pinYinInfo in pinYinInfoList)
                                {
                                        if (pinYinInfo.PinYin?.Length > 0)
                                        {
                                                pinYins.Add(pinYinInfo.PinYin);
                                        }
                                }
                                pinYinsList.Add(pinYins);

                        }
                        return pinYinsList;
                }

                /// <summary>
                /// 将当前字符串，转为对应的拼音字符串。
                /// </summary>
                /// <param name="str">当前字符串。</param>
                /// <param name="isNeedUpperCase">是否返回大写字母的拼音字符串。</param>
                /// <returns>当前字符串对应的拼音字符串。</returns>
                public static string? ToPinYin(
                        this string str,
                        bool isNeedUpperCase = true)
                {
                        var pinYin = ChinesePinYinInfo.GetPinYinOfString(str);
                        if (isNeedUpperCase
                                && pinYin != null)
                        {
                                pinYin = pinYin.ToUpper();
                        }
                        return pinYin;
                }

                /// <summary>
                /// 将当前字符串，转为对应的首字母拼音字符串。
                /// </summary>
                /// <param name="str">当前字符串。</param>
                /// <param name="isNeedUpperCase">是否返回大写字母的拼音字符串。</param>
                /// <returns>当前字符串对应的首字母拼音字符串。</returns>
                public static string? ToPinYinWithFirstCharacter(
                        this string str,
                        bool isNeedUpperCase = true)
                {
                        var pinYin = ChinesePinYinInfo.GetPinYinOfString(
                                str,
                                true);
                        if (isNeedUpperCase
                                && pinYin != null)
                        {
                                pinYin = pinYin.ToUpper();
                        }
                        return pinYin;
                }
        }
}
