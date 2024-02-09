using libWiiSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce
{
    public class DatabaseEntry
    {
        protected string tID;
        protected string nName;
        protected string dbName;
        protected int version;

        public string TitleID
        {
            get { return tID; }
            set { tID = value; }
        }

        public string NativeName
        {
            get { return nName; }
            set { nName = value; }
        }

        public string Name
        {
            get { return dbName; }
            set { dbName = value; }
        }

        public int emuRev
        {
            get { return version; }
            set { version = value; }
        }

        public DatabaseEntry(string TitleID, string NativeName, string Name, int EmuRev = -1) { tID = TitleID; nName = NativeName; dbName = Name; emuRev = EmuRev; }
    }

    public class Database
    {
        private Console Console { get; set; }
        internal DatabaseEntry[] List { get; set; }
        public enum Region
        {
            USA,
            PAL50,
            PAL60,
            JPN,
            KOR_Ja,
            KOR_En
        }

        public Database(Console c)
        {
            Console = c;
            switch (Console)
            {
                //                          -------------------------------------------------------------------------------------------------------------
                //                          | TitleID | Display name                                   | Name on MarioCube
                //                          -------------------------------------------------------------------------------------------------------------
                // NES
                // ********
                case Console.NES:
                    List = new DatabaseEntry[]
                    {
                            new DatabaseEntry("FA8E",   "Kirby's Adventure",                             "Kirby's Adventure"),
                            new DatabaseEntry("FA8P",   "Kirby's Adventure",                             "Kirby's Adventure"),
                            new DatabaseEntry("FA8J",   "星のカービィ 夢の泉の物語",                       "Hoshi no Kirby - Yume no Izumi no Monogatari"),
                            new DatabaseEntry("FA8T",   "별의 커비 꿈의 샘 이야기",                        "Kirby's Adventure"),

                            // new DatabaseEntry("FCWE",   "Super Mario Bros. 3",                           "Super Mario Bros. 3"),
                            // new DatabaseEntry("FCWP",   "Super Mario Bros. 3",                           "Super Mario Bros. 3"),
                            // new DatabaseEntry("FCWJ",   "スーパーマリオブラザーズ3",                       "Super Mario Bros. 3"),
                            // new DatabaseEntry("FCWQ",   "슈퍼 마리오브라더스 3",                           "Super Mario Bros. 3"),

                            new DatabaseEntry("FBNM",   "Ninja Gaiden",                                  "Ninja Gaiden"), // PAL60
                    };
                    break;

                // SNES
                // ********
                case Console.SNES:
                    List = new DatabaseEntry[]
                    {
                            new DatabaseEntry("JBDE",   "Donkey Kong Country 2",                         "Donkey Kong Country 2 - Diddy's Kong Quest"),
                            new DatabaseEntry("JBDP",   "Donkey Kong Country 2",                         "Donkey Kong Country 2 - Diddy's Kong Quest"),
                            new DatabaseEntry("JBDJ",   "スーパードンキーコング2",                         "Super Donkey Kong 2 - Dixie & Diddy"),
                            new DatabaseEntry("JBDT",   "동키콩 컨트리 2",                                "Donkey Kong Country 2 - Diddy's Kong Quest"),

                            new DatabaseEntry("JABL",   "Mario's Super Picross",                         "Mario's Super Picross") // PAL60
                    };
                    break;

                // N64
                // ********
                case Console.N64:
                    List = new DatabaseEntry[]
                    {
                            new DatabaseEntry("NAAE",   "Super Mario 64",                                "Super Mario 64", 0),
                            new DatabaseEntry("NAAP",   "Super Mario 64",                                "Super Mario 64", 0),
                            new DatabaseEntry("NAAJ",   "スーパーマリオ64",                                "Super Mario 64", 0),

                            new DatabaseEntry("NAFE",   "F-Zero X",                                      "F-Zero X", 0),
                            new DatabaseEntry("NAFP",   "F-Zero X",                                      "F-Zero X", 0),
                            new DatabaseEntry("NAFJ",   "F-ZERO X",                                      "F-Zero X", 0),

                            new DatabaseEntry("NADE",   "Star Fox 64",                                   "Star Fox 64", 1),
                            new DatabaseEntry("NADP",   "Lylat Wars",                                    "Lylat Wars", 1),
                            new DatabaseEntry("NADJ",   "スターフォックス64",                              "Star Fox 64", 1),
                         // new DatabaseEntry("NADT",   "스타폭스 64",                                    "Star Fox 64", 3),
                         
                            new DatabaseEntry("NABE",   "Mario Kart 64",                                 "Mario Kart 64", 1),
                            new DatabaseEntry("NABP",   "Mario Kart 64",                                 "Mario Kart 64", 1),
                            new DatabaseEntry("NABJ",   "マリオカート64",                                 "Mario Kart 64", 1),
                            new DatabaseEntry("NABT",   "마리오 카트 64",                                 "Mario Kart 64", 3),

                            new DatabaseEntry("NACE",   "The Legend of Zelda: Ocarina of Time",          "Legend of Zelda, The - Ocarina of Time", 1),
                            new DatabaseEntry("NACP",   "The Legend of Zelda: Ocarina of Time",          "Legend of Zelda, The - Ocarina of Time", 1),
                            new DatabaseEntry("NACJ",   "ゼルダの伝説 時のオカリナ",                       "Zelda no Densetsu - Toki no Okarina", 1),

                            new DatabaseEntry("NAJN",   "Sin and Punishment",                            "Sin & Punishment", 2),
                            new DatabaseEntry("NAJL",   "Sin and Punishment",                            "Sin & Punishment", 2), // PAL60
                            new DatabaseEntry("NAJJ",   "罪と罰 〜地球の継承者〜",                         "Tsumi to Batsu - Hoshi no Keishousha", 2),

                            new DatabaseEntry("NAUE",   "Mario Golf",                                    "Mario Golf", 3),
                            new DatabaseEntry("NAUP",   "Mario Golf",                                    "Mario Golf", 3),
                            new DatabaseEntry("NAUJ",   "マリオゴルフ64",                                 "Mario Golf 64", 3),
                    };
                    break;

                // SEGA
                // ****************
                // NOTE FROM THE AUTHOR: Only Revision 2 and Revision 3 SEGA WADs are accepted currently, because CCF modification does not work with Revision 1 WADs (such as Comix Zone).
                //                       They also have more customizable options anyway, such as brightness (which can be adjusted to 100%), sound, 6-button, etc.
                // ****************

                case Console.SMS:
                    List = new DatabaseEntry[]
                    {
                            new DatabaseEntry("LAGE",   "Sonic the Hedgehog",                            "Sonic The Hedgehog", 2),
                            new DatabaseEntry("LAGP",   "Sonic the Hedgehog",                            "Sonic The Hedgehog", 2),
                            new DatabaseEntry("LAGJ",   "ソニック・ザ・ヘッジホッグ",                       "Sonic The Hedgehog", 2),

                            new DatabaseEntry("LADE",   "Phantasy Star",                                 "Phantasy Star", 3),
                            new DatabaseEntry("LADP",   "Phantasy Star",                                 "Phantasy Star", 3),
                            new DatabaseEntry("LADJ",   "ファンタシースター",                              "Phantasy Star", 2)
                    };
                    break;

                case Console.SMDGEN:
                    List = new DatabaseEntry[]
                    {
                            new DatabaseEntry("MA6E",   "Streets of Rage 2",                             "Streets of Rage 2", 2),
                            new DatabaseEntry("MA6P",   "Streets of Rage 2",                             "Streets of Rage 2", 2),
                            new DatabaseEntry("MA6J",   "ベア・ナックルII 死闘への鎮魂歌",                  "Bare Knuckle II - Shitou he no Requiem", 2),

                            new DatabaseEntry("MBAN",   "Pulseman",                                      "Pulseman", 3),
                            new DatabaseEntry("MBAL",   "Pulseman",                                      "Pulseman", 3),
                            new DatabaseEntry("MBAJ",   "パルスマン",                                     "Pulseman", 2)
                    };
                    break;

                // Other
                // ****************

                default:
                case Console.PCE:
                case Console.NeoGeo:
                case Console.MSX:
                case Console.Flash:
                    throw new NotImplementedException();
            }
        }

        public Region GetRegion(int i)
        {
            char RegionCode = GetRegionCode(i);

            switch (RegionCode)
            {
                default:
                case 'E': // USA
                case 'N':
                    return Region.USA;

                case 'P':
                    return Region.PAL50;

                case 'L': // Japanese import
                case 'M': // American import
                    return Region.PAL60;

                case 'J': // Japan
                    return Region.JPN;

                case 'Q': // Korea with Japanese language
                    return Region.KOR_Ja;

                case 'T': // Korea with English language
                    return Region.KOR_En;
            }
        }

        public char GetRegionCode(int i) => List[i].TitleID.ToUpper()[3];

        /// <summary>
        /// Download WAD and load to memory
        /// </summary>
        /// <param name="i">Database index which contains the title ID</param>
        /// <returns>libWiiSharp WAD variable with loaded WAD data</returns>
        public WAD Load(int i)
        {
            string Region = null;

            switch (GetRegionCode(i))
            {
                case 'E':
                case 'N':
                    Region = " (USA)";
                    break;

                case 'P':
                case 'L': // Japanese import
                case 'M': // American import
                    Region = " (Europe)";
                    break;

                case 'J':
                    Region = " (Japan)";
                    break;

                case 'Q':
                    Region = " (Korea) (Ja,Ko)";
                    break;

                case 'T':
                    Region = " (Korea) (En,Ko)";
                    break;
            }

            string ConsoleType = Console == Console.NES ? " (NES)"
                               : Console == Console.SNES ? " (SNES)"
                               : Console == Console.N64 ? " (N64)"
                               : Console == Console.SMS ? " (SMS)"
                               : Console == Console.SMDGEN ? " (SMD)"
                               : null;

            if (Region == null || ConsoleType == null) throw new ArgumentException();

            // Load WAD from MarioCube
            // (no direct links!! unmodified WAD is loaded to application memory and is not downloaded to disk!!)
            // (Not sure if this is copyright-friendly..)
            // ****************
            string name = List[i].Name + Region + ConsoleType;
            string URL = "https://repo.mariocube.com/WADs/_WiiWare,%20VC,%20DLC,%20Channels%20&%20IOS/" + name[0].ToString().ToUpper() + "/" + Uri.EscapeDataString(name + " (Virtual Console)") + ".wad";

            Web.InternetTest();;
            byte[] y = new byte[1];

            using (WebClient x = new WebClient())
            using (System.IO.Stream webS = x.OpenRead(URL))
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                webS.CopyTo(ms);
                y = ms.ToArray();
            }

            WAD w = new WAD();
            w.LoadFile(y);

            // Title ID check
            // ****************
            if (w.UpperTitleID.ToUpper() != List[i].TitleID.ToUpper()) throw new ArgumentException();
            return w;
        }

        /// <summary>
        /// Download WAD and load to memory
        /// </summary>
        /// <param name="i">Title ID</param>
        /// <returns>libWiiSharp WAD variable with loaded WAD data</returns>
        public WAD Load(string i)
        {
            for (int x = 0; x < List.Length; x++)
                if (List[x].TitleID.ToUpper() == i.ToUpper()) return Load(x);

            return null;
        }
    }
}
