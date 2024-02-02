using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Options_VCNES : Form
    {
        // Options
        // *******
        public IDictionary<int, string> Settings = new Dictionary<int, string>
        {
            /* Palette */                   { 0, "0" },
            /* Use also for banner image */ { 1, "0" }
        };

        /* FOR COSMETIC DISPLAY ONLY */ public int ImgPaletteIndex = -1;
        // *******

        public Options_VCNES()
        {
            InitializeComponent();
            Language.AutoSetForm(this);

            PaletteList.Items[0] = Language.Get("ByDefault");
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Options
            // *******
            PaletteList.SelectedIndex = int.Parse(Settings[0]);
            checkBox1.Checked = Settings[1] == "1";
            // *******
        }

        private void OK_Click(object sender, EventArgs e)
        {
            // Options
            // *******
            Settings[0] = PaletteList.SelectedIndex.ToString();
            Settings[1] = checkBox1.Checked ? "1" : "0";
            // *******

            DialogResult = DialogResult.OK;
        }

        /////////////////////////
        // Section for options //
        /////////////////////////
        // *******

        public readonly string[][] Palettes = new string[][]
        {
            // WIIVC
            new string[]
            {
                "494949", "00006A", "090063", "290059", "42004A", "490000", "420000", "291100", "182700", "003010", "003000", "002910", "012043", "000000", "000000", "000000",
                "747174", "003084", "3101AC", "4B0194", "64007B", "6B0039", "6B2101", "5A2F00", "424900", "185901", "105901", "015932", "01495A", "101010", "000000", "000000",
                "ADADAD", "4A71B6", "6458D5", "8450E6", "A451AD", "AD4984", "B5624A", "947132", "7B722A", "5A8601", "388E31", "318E5A", "398E8D", "383838", "000000", "000000",
                "B6B6B6", "8C9DB5", "8D8EAE", "9C8EBC", "A687BC", "AD8D9D", "AE968C", "9C8F7C", "9C9E72", "94A67C", "84A77B", "7C9D84", "73968D", "DEDEDE", "000000", "000000"
            },

            // WIIVC_NEW
            new string[]
            {
                "696969", "000098", "0D008E", "3B007F", "5F006A", "680000", "5F0000", "3B1900", "233800", "004517", "004500", "003B17", "012E60", "000000", "000000", "000000",
                "A6A2A6", "0045BD", "4601F6", "6B01D4", "8F00B0", "990051", "9A2F01", "814300", "5F6900", "228001", "177F01", "017F48", "016981", "171717", "000000", "000000",
                "F8F8F8", "6AA2FF", "8F7EFF", "BD73FF", "EB74F8", "F868BD", "FF8D6A", "D4A248", "B0A33C", "81C001", "50CC46", "46CB81", "51CBCA", "505050", "000000", "000000",
                "FFFFFF", "C8E1FF", "CACBF9", "DFCCFF", "EEC2FF", "F8CAE1", "F9D7C8", "DFCDB1", "DFE2A3", "D4EEB1", "BDEFB0", "B1E1BD", "A5D7CA", "DEDEDE", "000000", "000000"
            },

            // 3DSVC
            new string[]
            {
                "737373", "21188C", "0000AD", "42009C", "8C0073", "AD0010", "A50000", "7B0800", "422900", "004200", "005200", "003910", "18395A", "000000", "000000", "000000",
                "BDBDBD", "0073EF", "2139EF", "8400F7", "BD00BD", "E7005A", "DE2900", "CE4A08", "8C7300", "009400", "00AD00", "009439", "00848C", "101010", "000000", "000000",
                "FFFFFF", "39BDFF", "5A94FF", "A58CFF", "F77BFF", "FF73B5", "FF7363", "FF9C39", "F7BD39", "84D610", "4ADE4A", "5AFF9C", "00EFDE", "393939", "000000", "000000",
                "FFFFFF", "ADE7FF", "C6D6FF", "D6CEFF", "FFC6FF", "FFC6DE", "FFBDB5", "FFDEAD", "FFE7A5", "E7FFA5", "ADF7BD", "B5FFCE", "9CFFF7", "8C8C8C", "000000", "000000"
            },

            // NESClassic_FBX
            new string[]
            {
                "616161", "000088", "1F0D99", "371379", "561260", "5D0010", "520E00", "3A2308", "21350C", "0D410E", "174417", "003A1F", "002F57", "000000", "000000", "000000",
                "AAAAAA", "0D4DC4", "4B24DE", "6912CF", "9014AD", "9D1C48", "923404", "735005", "5D6913", "167A11", "138008", "127649", "1C6691", "000000", "000000", "000000",
                "FCFCFC", "639AFC", "8A7EFC", "B06AFC", "DD6DF2", "E771AB", "E38658", "CC9E22", "A8B100", "72C100", "5ACD4E", "34C28E", "4FBECE", "424242", "000000", "000000",
                "FCFCFC", "BED4FC", "CACAFC", "D9C4FC", "ECC1FC", "FAC3E7", "F7CEC3", "E2CDA7", "DADB9C", "C8E39E", "BFE5B8", "B2EBC8", "B7E5EB", "ACACAC", "000000", "000000"
            },

            // NESRemixU
            new string[]
            {
                "6E6E6E", "2A159C", "2A0EAB", "5C1C95", "8E2372", "9C1C31", "95310E", "724007", "475507", "39630E", "3B650D", "315C40", "314E72", "000000", "000000", "000000",
                "A2A2A2", "3955D5", "3131DC", "7931CE", "B931A3", "C03155", "C04600", "A35C00", "797900", "478E00", "40950E", "408E5C", "317995", "6E6E6E", "000000", "000000",
                "DCDCDC", "5C8EDC", "4772DC", "8E63DC", "CE55DC", "DC5595", "DC6A31", "DC8E07", "C7B21C", "8AC707", "63CE39", "5CC780", "55B2B9", "A2A2A2", "000000", "000000",
                "DCDCDC", "87B9DC", "8EA3DC", "AB9CDC", "C79CDC", "DC9CC7", "DCAB9C", "DCC087", "DCCE79", "B2D572", "8ED58E", "8EDCB2", "8EDCCE", "DCDCDC", "000000", "000000"
            },

            // YUV
            new string[]
            {
                "666666", "002A88", "1412A7", "3B00A4", "5C007E", "6E0040", "6C0700", "561D00", "333500", "0C4800", "005200", "004F08", "00404D", "000000", "000000", "000000",
                "ADADAD", "155FD9", "4240FF", "7527FE", "A01ACC", "B71E7B", "B53120", "994E00", "6B6D00", "388700", "0D9300", "008F32", "007C8D", "000000", "000000", "000000",
                "FFFFFF", "64B0FF", "9290FF", "C676FF", "F26AFF", "FF6ECC", "FF8170", "EA9E22", "BCBE00", "88D800", "5CE430", "45E082", "48CDDE", "4F4F4F", "000000", "000000",
                "FFFFFF", "C0DFFF", "D3D2FF", "E8C8FF", "FAC2FF", "FFC4EA", "FFCCC5", "F7D8A5", "E4E594", "CFEF96", "BDF4AB", "B3F3CC", "B5EBF2", "B8B8B8", "000000", "000000"
            },

            // RGB
            new string[]
            {
                "6D6D6D", "002492", "0000DB", "6D49DB", "92006D", "B6006D", "B62400", "924900", "6D4900", "244900", "006D24", "009200", "004949", "000000", "000000", "000000",
                "B6B6B6", "006DDB", "0049FF", "9200FF", "B600FF", "FF0092", "FF0000", "DB6D00", "926D00", "249200", "009200", "00B66D", "009292", "242424", "000000", "000000",
                "FFFFFF", "6DB6FF", "9292FF", "DB6DFF", "FF00FF", "FF6DFF", "FF9200", "FFB600", "DBDB00", "6DDB00", "00FF00", "49FFDB", "00FFFF", "494949", "000000", "000000",
                "FFFFFF", "B6DBFF", "DBB6FF", "FFB6FF", "FF92FF", "FFB6B6", "FFDB92", "FFFF49", "FFFF6D", "B6FF49", "92FF6D", "49FFDB", "92DBFF", "929292", "000000", "000000"
            },

            // FCEUX
            new string[]
            {
                "747474", "24188C", "0000A8", "44009C", "8C0074", "A80010", "A40000", "7C0800", "402C00", "004400", "005000", "003C14", "183C5C", "000000", "000000", "000000",
                "BCBCBC", "0070EC", "2038EC", "8000F0", "BC00BC", "E40058", "D82800", "C84C0C", "887000", "009400", "00A800", "009038", "008088", "000000", "000000", "000000",
                "FCFCFC", "3CBCFC", "5C94FC", "CC88FC", "F478FC", "FC74B4", "FC7460", "FC9838", "F0BC3C", "80D010", "4CDC48", "58F898", "00E8D8", "787878", "000000", "000000",
                "FCFCFC", "A8E4FC", "C4D4FC", "D4C8FC", "FCC4FC", "FCC4D8", "FCBCB0", "FCD8A8", "FCE4A0", "E0FCA0", "A8F0BC", "B0FCCC", "9CFCF0", "C4C4C4", "000000", "000000"
            },

            // Wavebeam
            new string[]
            {
                "6B6B6B", "001B88", "21009A", "40008C", "600067", "64001E", "590800", "481600", "283600", "004500", "004908", "00421D", "003659", "000000", "000000", "000000",
                "B4B4B4", "1555D3", "4337EF", "7425DF", "9C19B9", "AC0F64", "AA2C00", "8A4B00", "666B00", "218300", "008A00", "008144", "007691", "000000", "000000", "000000",
                "FFFFFF", "63B2FF", "7C9CFF", "C07DFE", "E977FF", "F572CD", "F4886B", "DDA029", "BDBD0A", "89D20E", "5CDE3E", "4BD886", "4DCFD2", "525252", "000000", "000000",
                "FFFFFF", "BCDFFF", "D2D2FF", "E1C8FF", "EFC7FF", "FFC3E1", "FFCAC6", "F2DAAD", "EBE3A0", "D2EDA2", "BCF4B4", "B5F1CE", "B6ECF1", "BFBFBF", "000000", "000000"
            },

            // CompositeDirect
            new string[]
            {
                "656565", "00127D", "18008E", "360082", "56005D", "5A0018", "4F0500", "381900", "1D3100", "003D00", "004100", "003B17", "002E55", "000000", "000000", "000000",
                "AFAFAF", "194EC8", "472FE3", "6B1FD7", "931BAE", "9E1A5E", "993200", "7B4B00", "5B6700", "267A00", "008200", "007A3E", "006E8A", "000000", "000000", "000000",
                "FFFFFF", "64A9FF", "8E89FF", "B676FF", "E06FFF", "EF6CC4", "F0806A", "D8982C", "B9B40A", "83CB0C", "5BD63F", "4AD17E", "4DC7CB", "4C4C4C", "000000", "000000",
                "FFFFFF", "C7E5FF", "D9D9FF", "E9D1FF", "F9CEFF", "FFCCF1", "FFD4CB", "F8DFB1", "EDEAA4", "D6F4A4", "C5F8B8", "BEF6D3", "BFF1F1", "B9B9B9", "000000", "000000"
            }
        };

        public unsafe int CheckPalette(Bitmap bmp)
        {
            try
            {
                using (Bitmap newBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppRgb))
                {
                    using (Graphics g = Graphics.FromImage(newBmp))
                    {
                        g.DrawImage(bmp, 0, 0);
                        g.Dispose();
                    }

                    BitmapData data = newBmp.LockBits(new Rectangle(Point.Empty, newBmp.Size), ImageLockMode.ReadWrite, newBmp.PixelFormat);

                    int[] CheckedPixels = new int[Palettes.Length];

                    for (int i = 0; i < Palettes.Length; i++)
                    {
                        CheckedPixels[i] = 0;

                        for (int j = 0; j < 64; j++)
                        {
                            var color = ColorTranslator.FromHtml($"#{Palettes[i][j]}".ToUpper()).ToArgb();

                            byte* line = (byte*)data.Scan0;
                            for (int y = 0; y < data.Height; y++)
                            {
                                for (int x = 0; x < data.Width; x++)
                                {
                                    int c32 = *((int*)line + x);
                                    if (c32 == color && c32 != Color.Black.ToArgb() && c32 != Color.White.ToArgb())
                                        CheckedPixels[i]++;
                                }
                                line += data.Stride;
                            }
                        }
                    }

                    newBmp.UnlockBits(data);
                    newBmp.Dispose();
                    return CheckedPixels.ToList().IndexOf(CheckedPixels.Max());
                }
            }
            catch
            {
                return -1;
            }
        }

        public unsafe Bitmap SwapColors(Bitmap bmp, string[] oldColors, string[] newColors)
        {
            try
            {
                if (oldColors.Length != newColors.Length) return bmp;

                Bitmap newBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppRgb);

                using (Graphics g = Graphics.FromImage(newBmp))
                {
                    g.DrawImage(bmp, 0, 0);
                    g.Dispose();
                }

                BitmapData data = newBmp.LockBits(new Rectangle(Point.Empty, newBmp.Size), ImageLockMode.ReadWrite, newBmp.PixelFormat);

                for (int i = 0; i < oldColors.Length; i++)
                {
                    int rawFrom = ColorTranslator.FromHtml($"#{oldColors[i]}".ToUpper()).ToArgb();
                    int rawTo = ColorTranslator.FromHtml($"#{newColors[i]}".ToUpper()).ToArgb();

                    if (rawFrom != Color.Black.ToArgb() && rawFrom != Color.White.ToArgb())
                    {
                        byte* line = (byte*)data.Scan0;
                        for (int y = 0; y < data.Height; y++)
                        {
                            for (int x = 0; x < data.Width; x++)
                            {
                                // Method for 32bpp RGB/ARGB
                                // ********
                                int c32 = *((int*)line + x);
                                if (c32 == rawFrom)
                                    *((int*)line + x) = rawTo;
                            }

                            line += data.Stride;
                        }
                    }
                }

                newBmp.UnlockBits(data);
                bmp.Dispose();
                return newBmp;
            }
            catch
            {
                return bmp;
            }
        }

        private void PaletteChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.screen_nes;

            try   { if (PaletteList.SelectedIndex > 0) pictureBox1.Image = SwapColors(Properties.Resources.screen_nes, Palettes[0], Palettes[PaletteList.SelectedIndex]); }
            catch { }

            switch (PaletteList.SelectedIndex)
            {
                case 0:
                    label2.Text = string.Format(Language.Get("Author"), "Nintendo");
                    break;
                case 1:
                case 2:
                    label2.Text = string.Format(Language.Get("Author"), "Nintendo / SuperrSonic");
                    break;
                case 3:
                    label2.Text = string.Format(Language.Get("Author"), "Nintendo / FirebrandX");
                    break;
                case 4:
                    label2.Text = string.Format(Language.Get("Author"), "Nintendo / N-Mario");
                    break;
                case 5:
                case 6:
                    label2.Text = string.Format(Language.Get("Author"), "Nestopia");
                    break;
                case 7:
                    label2.Text = string.Format(Language.Get("Author"), "FCEUX");
                    break;
                case 8:
                case 9:
                    label2.Text = string.Format(Language.Get("Author"), "FirebrandX");
                    break;
            }
        }
    }
}
