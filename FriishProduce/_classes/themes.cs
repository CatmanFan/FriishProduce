using System.Drawing;

namespace FriishProduce.Themes
{
    public static class Dark
    {
        public static Color BG = Color.FromArgb(55, 55, 55);
        public static Color BG_Secondary = Color.FromArgb(75, 75, 75);
        public static Color BG_Image = Color.FromArgb(100, 100, 100);
        public static Color FG = Color.White;

        public static Color ButtonBorder = Color.FromArgb(64, 64, 64);
        public static Color ButtonDown = Color.FromArgb(50, 50, 50);
    }

    public static class Light
    {
        public static Color BG = Color.FromArgb(248, 248, 248);
        public static Color BG_Secondary = Color.FromArgb(232, 232, 232);
        public static Color BG_Image = Color.Gainsboro;
        public static Color FG = Color.Black;

        public static Color ButtonBorder = Color.LightGray;
        public static Color ButtonDown = Color.Silver;
    }
}
