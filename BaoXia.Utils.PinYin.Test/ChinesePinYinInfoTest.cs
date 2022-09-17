using BaoXia.Utils.PinYin.ChinesePinYinInfesCodeCreator;

namespace BaoXia.Utils.PinYin.Test
{
        [TestClass]
        public class ChinesePinYinOriginalInfoTest
        {
                [TestMethod]
                public void GetPinYinInfoWithChineseCharacterTest()
                {
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

        }
}