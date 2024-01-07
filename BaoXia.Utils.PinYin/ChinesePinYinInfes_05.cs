namespace BaoXia.Utils.PinYin
{
	public class ChinesePinYinInfes_05
	{

		public const char CharsEndSymbol = ' ';

		public const int PinYinUnitLength = 5;
		public static readonly string AllPinYins = "ya   ye   bu   yao  xia  sen  da   zhan zhu  huai biao mao  kou  ban  gun  yi   can  ne   gou  ke   hu   lie  lo   hua  wo   duo  wa   ha   sao  mai  na   bo   n    hao  diao a    dui  chuaiti   bai  zan  o    chan qie  ǹg   miu  chu  dan  juan wei  ga   zun  gu   jiangci   zong suo  la   bei  pin  shi  tiao shao di   tuan chengnao  sui  liao zuo  gua  ruan dun  wang piao zhuo pei  peng huangjiao ei   zu   ning cui  ying jian jin  nian po   fan  nuo  xuan ding pan  pai  zi   bi   qi   tuo  miao ba   xionger   pang nuan jie  zhao pi   xu   li   heng tie  xi   ế    fei  zhen tang zou  pao  zhi  die  qiu  chuo cun  ju   she  lai  ta   cha  kai  yin  kui  zhai ";

		public const int PinYinWithSoundUnitLength = 5;
		public static readonly string AllPinYinWithSounds = "yà   yè   bú   yáo  xiá  sēn  dá   zhǎn zhū  huai biāo mào  kòu  bān  gǔn  yì   càn  ne   gōu  kē   xià  hú   lie  lo   huà  wō   duò  wá   hǎ   sào  mái  nà   bó   ń    háo  diào ǎ    duǐ  chuàitì   bài  zan  ò    chǎn qiè  ǹg   miù  chū  dǎn  juǎn wéi  gā   zūn  gū   jiǎngcī   zǒng hū   suō  la   bèi  pīn  shì  tiáo shào dī   tuán chēngnáo  suī  liào zuǒ  guā  ruán dùn  wǎng piáo zhuó pèi  bēi  péng liáo huángjiǎo éi   zǔ   nìng cuǐ  yìng zàn  jiàn jìn  jiāo nián pò   fān  liǎo nuò  shī  xuàn wēi  dīng pān  pái  zī   bì   liè  qì   tuǒ  cǎn  miào ba   xióngér   pǎng nuǎn jiē  zhāo pì   xū   lì   héng tiē  xī   ế    jiè  yí   féi  zhěn tāng zōu  páo  zhī  diē  qiù  chuò cǔn  jú   jiān nǎ   shé  lài  tā   chá  yào  kǎi  yǐn  à    gǔ   kuí  zhāi ";

		public const int ChineseCharacterUnitLength = 1;
		public static readonly string AllChineseCharacters = "䀹䅖不佻假傪僤儃兪划剽務區卑卷厭參呐呴呵呼和咧咯咶咼哆哇哈哨哩哪哱哽唔唬啁啊啍啐啑啡喒喔單嗛嗯嘐嘔嘽圈堤夾奠家將差從惡戲拉拔拚挈挑捎揄提揣搶摎摻撋撣撩撮擖擩敦方朴杓杝柭棓棱榜樂横橋欸殺沮泥洒浧淺渫湔湛湫溓溥潘潦濡灑煇猗甸町番箄純紕累絜綏縿繆罷能腝膀臑苴茀著薜藇蠡行訑詀詑誒諎譺賁趁趟趣趵跂跕跙踆踔踦踶蹲蹻軒載轑那邪釐鉈鉏銚閡闇阿離霅鮭齊";
		public static readonly short[] AllChineseCharacterPinYinIndexes = new short[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 4, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 20, 56, 57, 58, 59, 60, 61, 62, 3, 63, 64, 65, 66, 5, 67, 7, 68, 69, 70, 71, 72, 73, 74, 75, 25, 76, 58, 65, 77, 68, 78, 79, 80, 60, 81, 82, 83, 84, 40, 43, 85, 86, 79, 87, 88, 89, 68, 90, 60, 91, 49, 84, 92, 93, 94, 95, 96, 21, 97, 98, 16, 99, 100, 101, 102, 103, 104, 105, 96, 106, 107, 108, 109, 110, 98, 111, 112, 113, 105, 15, 114, 115, 116, 117, 118, 119, 120, 43, 121, 122, 15, 60, 123, 124, 85, 95, 68, 30, 125, 126, 127, 128, 3, 129, 130, 35, 52, 15, 131, 132 };
		public static readonly short[] AllChineseCharacterPinYinWithSoundIndexes = new short[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 3, 65, 66, 67, 68, 5, 69, 7, 70, 71, 72, 73, 74, 75, 76, 77, 26, 78, 79, 67, 80, 81, 82, 83, 84, 62, 85, 86, 87, 88, 89, 44, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 88, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 105, 117, 118, 119, 120, 121, 108, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 44, 134, 135, 15, 62, 136, 137, 138, 104, 70, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 15, 149, 150 };


	}
}