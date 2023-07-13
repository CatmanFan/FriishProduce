using System.Drawing;

namespace FriishProduce.Themes
{
    public static class Dark
    {
        public static Color BG = Color.FromArgb(46, 46, 46);
        public static Color BG_Secondary = Color.FromArgb(53, 53, 53);
        public static Color FG = Color.White;

        public static Color Button = BG_Secondary;
        public static Color ButtonBorder = Color.FromArgb(60, 60, 60);
        public static Color ButtonDown = Color.FromArgb(65, 65, 65);
        public static Color ComboBox = BG_Secondary;
    }

    public static class Light
    {
        public static Color BG = Color.FromArgb(248, 248, 248);
        public static Color BG_Secondary = Color.FromArgb(232, 232, 232);
        public static Color FG = Color.Black;

        public static Color Button = BG_Secondary;
        public static Color ButtonBorder = Color.LightGray;
        public static Color ButtonDown = Color.Silver;
        public static Color ComboBox = Color.FromArgb(240, 240, 240);
    }

    public static class SystemSettings_3DS
    {
        public static Color BG = Color.FromArgb(240, 232, 156);
        public static Color BG_Secondary = Color.FromArgb(221, 212, 155);
        public static Color FG = Color.Black;

        public static Color Button = Color.FromArgb(242, 240, 227);
        public static Color ButtonBorder = Color.LightGray;
        public static Color ButtonDown = Color.Silver;
        public static Color ComboBox = Color.FromArgb(248, 248, 248);
    }
}
