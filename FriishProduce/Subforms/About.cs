using System;
using System.Reflection;
using System.Windows.Forms;

namespace FriishProduce
{
    partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            Text = Program.Lang.Format(("about_app", null), Program.Lang.ApplicationTitle);
            labelProductName.Text = AssemblyProduct;
            labelDescription.Text = string.Format("{0}" + Environment.NewLine + "Current language: {1} ({2})", AssemblyDescription, System.Globalization.CultureInfo.CurrentUICulture.EnglishName, Program.Lang.Author);
            labelVersion.Text = "v" + Updater.VerName;
            labelCopyright.Text = AssemblyCopyright;
            b_close.Text = Program.Lang.String("b_close");

            Theme.ChangeColors(this, false);
            Theme.BtnSizes(b_close);
            Theme.BtnLayout(this, b_close);

            htmlPanel1.BackColor = BackColor;
            htmlPanel1.BaseStylesheet = HTML.BaseStylesheet + "\n" + "div { padding: 4px 6px !important; }";
            htmlPanel1.Text = HTML.MarkdownToHTML(new string[]
            {
                "This application is **not** endorsed by Nintendo in any form.",
                "### Credits",
                null,
                "Third-party components and apps:",
                null,
                "* **libWiiSharp** (orig. author: Leathl), [forked](https://github.com/WiiDatabase/libWiiSharp/) by [WiiDatabase](https://github.com/WiiDatabase).",
                "* [SD/USB forwarder components](https://github.com/modmii/modmii.github.io/tree/master/Support/DOLS) of **[ModMii](https://github.com/modmii/modmii.github.io)** by [XFlak](https://github.com/xflak).",
                "* **WWCXTool** by alpha-0.",
                "* **romc0** and **lzh8_cmpdec** by [hcs64](https://github.com/hcs64).",
                "* **[ROMC VC Compressor](https://www.elotrolado.net/hilo_romc-vc-compressor_1015640)** by Jurai, with additional LZSS code by Haruhiko Okumura.",
                "* [libertyernie](https://github.com/libertyernie)'s **[fork of BrawlLib](https://github.com/libertyernie/brawllib-wit)** by soopercool101.",
                "* **[bincuesplit](https://archive.org/details/bincuesplit)** by Francisco Muñoz (Hermes).",
                "* **[Custom Frodo](http://www.tepetaklak.com/wii/#Custom%20Frodo)** by [Nejat Dilek (WiiCrazy)](http://www.tepetaklak.com/).",
                "* [Static WAD Base](https://github.com/Brawl345/customizemii/blob/master/Base_WADs/StaticBase.wad) from **CustomizeMii**.",
                null,
                "Icons and interface:",
                null,
                "* **[Farm-Fresh Web Icons](https://github.com/gammasoft/fatcow)** by FatCow.",
                "* **[MdiTabCtrl](https://github.com/JacksiroKe/MdiTabCtrl)** by Jack Siro.",
                "* **[SCE-PS3 Rodin LATIN](https://github.com/skrptktty/ps3-firmware-beginners-luck/blob/master/PS3_411/update_files/dev_flash/data/font/SCE-PS3-RD-R-LATIN.TTF) font** from [this repo](https://github.com/skrptktty/ps3-firmware-beginners-luck) (Solar Storm License).",
                null,
                "I would also like to thank the following people:",
                null,
                "* **[SuperrSonic](https://github.com/SuperrSonic)** for reverse-engineering much of Wii software and official emulator code, and in particular, his [SNES VC Booster](https://github.com/SuperrSonic/snes-vc-booster) and [fork of RetroArch Wii](https://github.com/SuperrSonic/RA-SS).",
                "* **[saulfabreg](https://github.com/saulfabregwiivc)** for archiving several tools and aiding in research & documentation.",
                "* **[sr_corsario](https://gbatemp.net/members/sr_corsario.128473/)** for his work in disclosing NEO-GEO ROM injection methods.",
                "* **[FIX94](https://github.com/FIX94)** for his particular contributions to (v)Wii and Wii U homebrew and assistance in forking various homebrew apps.",
                "* **[Larsenv](https://github.com/Larsenv)** for his astounding work in the Wii homebrew community, and for originally disclosing a method for Flash WAD injection ([GBAtemp thread](https://gbatemp.net/threads/how-to-make-flash-game-wad-injects.561406/)).",
                "* And of course, the team at the **0RANGECHiCKEN** release group, including [lolsjoel](https://gbatemp.net/members/lolsjoel.18721/), and the late [G0dLiKe](https://gbatemp.net/members/g0dlike.190457/), without whose work this project would not have been possible.",
                null,
                "#### Emulators",
                null,
                "* **[FCE Ultra GX](https://github.com/dborth/fceugx)** (dborth et al.)",
                "* **[FCE Ultra RX](https://github.com/NiuuS/FCEUltraRX)** (NiuuS et al.)",
                "* **[FCEUX TX / FCEUGX-1UP](https://gbatemp.net/threads/fceugx-1up.558023/)** (Tanooki16)",
                "* **[Snes9x GX](https://github.com/dborth/snes9xgx)** (dborth et al.)",
                "* **[Snes9x RX](https://github.com/NiuuS/Snes9xRX)** (NiuuS et al.)",
                "* **[Snes9x TX / Snes9xGX-Mushroom](https://gbatemp.net/threads/snes9xgx-mushroom.558500/)** (Tanooki16)",
                "* **[Genesis Plus GX](https://github.com/ekeeke/Genesis-Plus-GX)** (eke-eke et al.)",
                "* **[Wii64 1.3 MOD](https://github.com/saulfabregwiivc/Wii64/tree/wii64-wiiflow)** (Wii64 Team, forked by saulfabreg)",
                "* **[Not64](https://github.com/extremscorner/not64)** (Extrems)",
                "* **[Mupen64GC-FIX94](https://github.com/FIX94/mupen64gc-fix94)** (Wii64 Team, forked by FIX94)",
                "* **[WiiSX](https://github.com/emukidid/pcsxgc)** (Wii64 Team)",
                "* **[WiiStation / WiiSXRX_2022](https://github.com/xjsxjs197/WiiSXRX_2022)** (xjsxjs197, forked from NiuuS' WiiSX RX)",
                "* **[EasyRPG Player](https://github.com/EasyRPG/Player)** (EasyRPG Team)",
                "<!-- * **[Visual Boy Advance GX](https://github.com/dborth/vbagx)** (dborth et al.) -->",
                "<!-- * **[mGBA Wii](https://github.com/mgba-emu/mgba)** (endrift et al.) -->",
                "<!-- * **[WiiMednafen](https://github.com/raz0red/wii-mednafen)** (raz0red) -->",
            });
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    }
}
