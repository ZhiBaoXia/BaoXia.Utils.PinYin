
// See https://aka.ms/new-console-template for more information

using BaoXia.Utils.PinYin;

var testText = "“你好, 世界!”的拼音是：";
Console.WriteLine(testText);
var testTextPinYin
	= ChinesePinYinInfo.GetPinYinOfString(testText);
Console.WriteLine(testTextPinYin);
var testTextPinYinFirstChar
	= ChinesePinYinInfo.GetPinYinOfString(testText, true);
Console.WriteLine(testTextPinYinFirstChar);

Console.ReadLine();

