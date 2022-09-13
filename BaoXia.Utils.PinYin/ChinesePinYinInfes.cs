using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoXia.Utils.PinYin
{
        public class ChinesePinYinInfes
        {
                ////////////////////////////////////////////////
                // @类方法
                ////////////////////////////////////////////////

                #region 类方法

                public static void CreateChinesePinYinInfesCodesFileWithAllPinYinList(
                        string fileName,
                        //
                        int objectPinYinIndex,
                        //
                        List<string> allPinYinList,
                        int pinYinLengthMax,
                        //
                        List<string> allPinYinWithSoundList,
                        int pinYinWithSoundLengthMax,
                        //
                        List<string> allChineseCharList,
                        int chineseCharLengthMax,
                        //
                        Dictionary<string, int> allChineseCharPinYinIndex,
                        Dictionary<string, int> allChineseCharPinYinWithSoundIndex)
                {
                        const char CharsEndSymbol = ' ';



                        var allChineseCharCodes = string.Empty;
                        var allChineseCharPinYinIndexCodes = string.Empty;
                        var allChineseCharPinYinWithSoundIndexCodes = string.Empty;


                        ////////////////////////////////////////////////
                        // CharsEndSymbolCodes
                        ////////////////////////////////////////////////
                        var charsEndSymbolCodes
                                = "\t\tpublic const char CharsEndSymbol = '" + CharsEndSymbol + "';";


                        ////////////////////////////////////////////////
                        // allPinYinCodes
                        ////////////////////////////////////////////////
                        var allPinYinCodes = string.Empty;
                        var blankPinYinWithLengthMax = new string(CharsEndSymbol, pinYinLengthMax);
                        var progressIndex = 0.0;
                        Console.WriteLine($"取汉字的第 {objectPinYinIndex + 1} 个拼音，开始生成【拼音】代码……");
                        foreach (var pinYin in allPinYinList)
                        {
                                allPinYinCodes
                                        += string.Concat(
                                                pinYin,
                                                blankPinYinWithLengthMax.AsSpan(pinYin.Length));

                                progressIndex++;
                                Console.WriteLine("取汉字的第 {0:D} 个拼音，生成【拼音】代码：{1:F2}%",
                                        objectPinYinIndex + 1,
                                        100.0 * progressIndex / allPinYinList.Count);
                        }
                        var pinYinUnitLength = pinYinLengthMax;
                        allPinYinCodes
                                = "\t\tpublic const int PinYinUnitLength = " + pinYinUnitLength + ";\r\n"
                                + "\t\tpublic static readonly string AllPinYins = \"" + allPinYinCodes + "\";";


                        ////////////////////////////////////////////////
                        // allPinYinWithSoundCodes
                        ////////////////////////////////////////////////
                        var allPinYinWithSoundCodes = string.Empty;
                        var blankPinYinWithSoundWithLengthMax = new string(CharsEndSymbol, pinYinWithSoundLengthMax);
                        progressIndex = 0.0;
                        Console.WriteLine($"取汉字的第 {objectPinYinIndex + 1} 个拼音，开始生成【拼音含声调】代码……");
                        foreach (var pinYinWithSound in allPinYinWithSoundList)
                        {
                                allPinYinWithSoundCodes
                                        += string.Concat(
                                                pinYinWithSound,
                                                blankPinYinWithSoundWithLengthMax.AsSpan(pinYinWithSound.Length));

                                progressIndex++;
                                Console.WriteLine(
                                        "取汉字的第 {0:D2} 个拼音，生成【拼音含声调】代码：{1:F2}%",
                                        objectPinYinIndex + 1,
                                        100.0 * progressIndex / allPinYinWithSoundList.Count);
                        }
                        var pinYinWithSoundUnitLength = pinYinWithSoundLengthMax;
                        allPinYinWithSoundCodes
                                = "\t\tpublic const int PinYinWithSoundUnitLength = " + pinYinWithSoundUnitLength + ";\r\n"
                                + "\t\tpublic static readonly string AllPinYinWithSounds = \"" + allPinYinWithSoundCodes + "\";";



                        progressIndex = 0.0;
                        Console.WriteLine($"取汉字的第 {objectPinYinIndex + 1} 个拼音，开始生成【中文字符】代码……");
                        var blankChineseCharWithLengthMax = new string(CharsEndSymbol, chineseCharLengthMax);
                        foreach (var chineseChar in allChineseCharList)
                        {
                                var chineseCharCodes
                                        = string.Concat(
                                                chineseChar,
                                                blankChineseCharWithLengthMax.AsSpan(chineseChar.Length));
                                allChineseCharCodes
                                        += chineseCharCodes;


                                var chinesePinYinInfoIndex
                                        = allChineseCharPinYinIndex.GetValueOrDefault(chineseChar, -1);
                                if (allChineseCharPinYinIndexCodes.Length > 0)
                                {
                                        allChineseCharPinYinIndexCodes += ", ";
                                }
                                allChineseCharPinYinIndexCodes
                                = string.Concat(
                                                allChineseCharPinYinIndexCodes,
                                                chinesePinYinInfoIndex.ToString());

                                var chinesePinYinWithSoundInfoIndex
                                        = allChineseCharPinYinWithSoundIndex.GetValueOrDefault(chineseChar, -1);
                                if (allChineseCharPinYinWithSoundIndexCodes.Length > 0)
                                {
                                        allChineseCharPinYinWithSoundIndexCodes += ", ";
                                }
                                allChineseCharPinYinWithSoundIndexCodes
                                = string.Concat(
                                                allChineseCharPinYinWithSoundIndexCodes,
                                                chinesePinYinWithSoundInfoIndex.ToString());

                                progressIndex++;
                                Console.WriteLine(
                                        "取汉字的第 {0:D2} 个拼音，生成【中文字符】代码：{1:F2}%",
                                        objectPinYinIndex + 1,
                                        100.0 * progressIndex / allChineseCharList.Count);
                        }
                        var chineseCharUnitLength = chineseCharLengthMax;
                        allChineseCharCodes
                                = "\t\tpublic const int ChineseCharacterUnitLength = " + chineseCharUnitLength + ";\r\n"
                                + "\t\tpublic static readonly string AllChineseCharacters = \"" + allChineseCharCodes + "\";";
                        allChineseCharPinYinIndexCodes
                                = "\t\tpublic static readonly short[] AllChineseCharacterPinYinIndexes = new short[]{" + allChineseCharPinYinIndexCodes + "};";
                        allChineseCharPinYinWithSoundIndexCodes
                                = "\t\tpublic static readonly short[] AllChineseCharacterPinYinWithSoundIndexes = new short[]{" + allChineseCharPinYinWithSoundIndexCodes + "};";


                        ////////////////////////////////////////////////
                        // !!! 写入文件 !!!
                        ////////////////////////////////////////////////
                        var chinesePinYinInfesCodes
                                = "using System;\r\n"
                                + "using System.Collections.Generic;\r\n"
                                + "using System.Linq;\r\n"
                                + "using System.Text;\r\n"
                                + "using System.Threading.Tasks;\r\n"
                                + "\r\n"
                                + "namespace BaoXia.Utils.PinYin\r\n"
                                + "{\r\n"
                                + "\tpublic class ChinesePinYinInfes_" + (objectPinYinIndex + 1).ToString("D2") + "\r\n"
                                + "\t{\r\n"
                                + "\r\n"
                                + charsEndSymbolCodes + "\r\n"
                                + "\r\n"
                                + allPinYinCodes + "\r\n"
                                + "\r\n"
                                + allPinYinWithSoundCodes + "\r\n"
                                + "\r\n"
                                + allChineseCharCodes + "\r\n"
                                + allChineseCharPinYinIndexCodes + "\r\n"
                                + allChineseCharPinYinWithSoundIndexCodes + "\r\n"
                                + "\r\n"
                                + "\r\n"
                                + "\t}\r\n"
                                + "}";

                        Console.WriteLine("编码结束，正在写入文件。。。");
                        System.IO.File.WriteAllText(
                                "d:\\" + fileName,
                                chinesePinYinInfesCodes);

                        Console.WriteLine("文件已更新。");
                        ////////////////////////////////////////////////
                }

                public static void CreateChinesePinYinInfoFiles()
                {
                        var chinesePinYinInfes = ChinesePinYinOriginalInfes.Infes;

                        for (var objectPinYinIndex = 0;
                                ;
                                objectPinYinIndex++)
                        {
                                var allChineseCharList = new List<string>();
                                var allChineseCharPinYinIndex = new Dictionary<string, int>();

                                var allChineseCharPinYinWithSoundIndex = new Dictionary<string, int>();
                                var chineseCharLengthMax = 0;
                                var chineseCharWithMultiplePinYinsCount = 0;
                                var chineseCharWithMultiplePinYinsCountInfes = new Dictionary<int, int>();
                                var allPinYinList = new List<string>();
                                var pinYinLengthMax = 0;
                                var allPinYinWithSoundList = new List<string>();
                                var pinYinWithSoundLengthMax = 0;
                                var progressIndex = 0.0;
                                Console.WriteLine($"取汉字的第 {objectPinYinIndex + 1} 个拼音，开始收集原始数据信息……");
                                foreach (var chinesePinYinInfo in chinesePinYinInfes)
                                {
                                        var pinYinStrings
                                                = chinesePinYinInfo.PinYin.Split(
                                                        ',',
                                                        StringSplitOptions.RemoveEmptyEntries
                                                        | StringSplitOptions.RemoveEmptyEntries);
                                        if (objectPinYinIndex >= pinYinStrings.Length)
                                        {
                                                // !!!
                                                continue;
                                                // !!!
                                        }
                                        var pinYinWithSoundStrings
                                                = chinesePinYinInfo.PinYinWithSound.Split(
                                                        ',',
                                                        StringSplitOptions.RemoveEmptyEntries
                                                        | StringSplitOptions.RemoveEmptyEntries);


                                        var chineseChar = chinesePinYinInfo.ChineseCharacter;
                                        {
                                                allChineseCharList.Add(chineseChar);
                                                //
                                                if (chineseCharLengthMax < chineseChar.Length)
                                                {
                                                        chineseCharLengthMax = chineseChar.Length;
                                                }
                                        }


                                        string pinYinString = pinYinStrings[objectPinYinIndex];
                                        {
                                                var pinYinIndex = allPinYinList.IndexOf(pinYinString);
                                                if (pinYinIndex < 0)
                                                {
                                                        pinYinIndex = allPinYinList.Count;
                                                        allPinYinList.Add(pinYinString);
                                                        //
                                                        if (pinYinLengthMax < pinYinString.Length)
                                                        {
                                                                pinYinLengthMax = pinYinString.Length;
                                                        }
                                                }
                                                //
                                                allChineseCharPinYinIndex.Add(chineseChar, pinYinIndex);
                                                //
                                        }
                                        if (pinYinStrings.Length > 1)
                                        {
                                                chineseCharWithMultiplePinYinsCount++;

                                                chineseCharWithMultiplePinYinsCountInfes.TryGetValue(
                                                        pinYinStrings.Length,
                                                        out var chineseCharsCountWithSameMultiplePinYinsCount);
                                                {
                                                        chineseCharsCountWithSameMultiplePinYinsCount++;
                                                }
                                                chineseCharWithMultiplePinYinsCountInfes.Remove(
                                                        pinYinStrings.Length);
                                                chineseCharWithMultiplePinYinsCountInfes.Add(
                                                        pinYinStrings.Length,
                                                        chineseCharsCountWithSameMultiplePinYinsCount);
                                        }


                                        string pinYinWithSoundString = pinYinWithSoundStrings[objectPinYinIndex];
                                        {
                                                var pinYinWithSoundIndex = allPinYinWithSoundList.IndexOf(pinYinWithSoundString);
                                                if (pinYinWithSoundIndex < 0)
                                                {
                                                        pinYinWithSoundIndex = allPinYinWithSoundList.Count;
                                                        allPinYinWithSoundList.Add(pinYinWithSoundString);
                                                        //
                                                        if (pinYinWithSoundLengthMax < pinYinWithSoundString.Length)
                                                        {
                                                                pinYinWithSoundLengthMax = pinYinWithSoundString.Length;
                                                        }
                                                }
                                                allChineseCharPinYinWithSoundIndex.Add(chineseChar, pinYinWithSoundIndex);
                                        }


                                        progressIndex++;
                                        Console.WriteLine(
                                                "取汉字的第 {0:D2} 个拼音，开始收集原始数据信息的【中文字符，拼音，拼音含声调】信息：{1:F2}%",
                                                objectPinYinIndex + 1,
                                                100.0 * progressIndex / (double)chinesePinYinInfes.Length);
                                }

                                foreach (var keyValue in chineseCharWithMultiplePinYinsCountInfes)
                                {
                                        var percentCaption = (100.0 * keyValue.Value / chinesePinYinInfes.Length).ToString("F2");
                                        Console.WriteLine($"取汉字的第 {objectPinYinIndex + 1} 个拼音，拼音数量为 {keyValue.Key} 的多音字，一共 {keyValue.Value} 个，占比 {percentCaption}%");
                                }

                                Console.WriteLine($"取汉字的第 {objectPinYinIndex + 1} 个拼音，开始排序【中文】信息……");
                                allChineseCharList.Sort((chineseCharA, chineseCharB) =>
                                {
                                        var compareValue = ChinesePinYinInfo.CompareChars(
                                                chineseCharA.ToCharArray(),
                                                chineseCharB.ToCharArray());
                                        {
                                                // Console.WriteLine($"取汉字的第 {objectPinYinIndex + 1} 个拼音，排序【中文】的排序值：" + compareValue);
                                        }
                                        return compareValue;
                                });

                                ////////////////////////////////////////////////
                                ////////////////////////////////////////////////
                                ////////////////////////////////////////////////
                                // ⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠
                                // !!!⚠ 不要对【拼音】信息进行排序，因为已经收集了索引信息 ⚠
                                // ⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠
                                // Console.WriteLine("开始排序【拼音】信息……");
                                // allPinYinList.Sort((pinYinA, pinYinB) =>
                                // {
                                //         return ChinesePinYinInfo_CharPosition.CompareChars(
                                //                 pinYinA.ToCharArray(),
                                //                 pinYinB.ToCharArray());
                                // });
                                // Console.WriteLine("开始排序【拼音含声调】信息……");
                                // allPinYinWithSoundList.Sort((pinYinA, pinYinB) =>
                                // {
                                //         return ChinesePinYinInfo_CharPosition.CompareChars(
                                //                 pinYinA.ToCharArray(),
                                //                 pinYinB.ToCharArray());
                                // });
                                ////////////////////////////////////////////////
                                ////////////////////////////////////////////////
                                ////////////////////////////////////////////////


                                ////////////////////////////////////////////////
                                // 汉字拼音信息：
                                ////////////////////////////////////////////////
                                if (allChineseCharList.Count > 0)
                                {
                                        CreateChinesePinYinInfesCodesFileWithAllPinYinList(
                                                "ChinesePinYinInfes_" + (objectPinYinIndex + 1).ToString("D2") + ".cs",
                                                //
                                                objectPinYinIndex,
                                                //
                                                allPinYinList,
                                                pinYinLengthMax,
                                                //
                                                allPinYinWithSoundList,
                                                pinYinWithSoundLengthMax,
                                                //
                                                allChineseCharList,
                                                chineseCharLengthMax,
                                                //
                                                allChineseCharPinYinIndex,
                                                allChineseCharPinYinWithSoundIndex);
                                }
                                else
                                {
                                        break;
                                }
                        }
                }

                #endregion
        }
}
