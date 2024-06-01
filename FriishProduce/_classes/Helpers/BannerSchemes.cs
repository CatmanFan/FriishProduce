using System.Collections.Generic;
using System.Drawing;

namespace FriishProduce
{
    public static class BannerSchemes
    {
        public static List<(Color bg, Color bgLogo, Color bgBottom, Color lines, Color topBorder, Color topBG, Color topText)> List
        {
            get => new List<(Color bg, Color bgLogo, Color bgBottom, Color lines, Color topBorder, Color topBG, Color topText)>
            {
                // ****************

                /* NES */           (
                                        Color.FromArgb(250, 250, 250),  // Main Background Color
                                        Color.FromArgb(255, 230, 230),  // Background Logo(s)
                                        Color.FromArgb(100, 100, 100),  // Bottom Section

                                        Color.FromArgb(155, 155, 155),  // Left Lines

                                        Color.FromArgb(144, 144, 144),  // PFLine Border
                                        Color.FromArgb(210, 210, 210),  // PFLine BG Color
                                        Color.FromArgb(230, 40, 40)     // Text_PF
                                    ),

                /* FC */            (
                                        Color.FromArgb(250, 250, 250),
                                        Color.FromArgb(240, 240, 240),
                                        Color.FromArgb(200, 200, 200),

                                        Color.FromArgb(155, 155, 155),

                                        Color.FromArgb(122, 48, 48),
                                        Color.FromArgb(217, 31, 31),
                                        Color.FromArgb(230, 230, 40)
                                    ),

                /* SNES */          (
                                        Color.FromArgb(209, 209, 209),
                                        Color.FromArgb(200, 200, 200),
                                        Color.FromArgb(60, 40, 70),

                                        Color.FromArgb(120, 120, 120),

                                        Color.FromArgb(127, 88, 149),
                                        Color.FromArgb(190, 146, 255),
                                        Color.FromArgb(100, 60, 120)
                                    ),

                /* SFC */           (
                                        Color.FromArgb(209, 209, 209),
                                        Color.FromArgb(200, 200, 200),
                                        Color.FromArgb(0, 0, 0),

                                        Color.FromArgb(120, 120, 120),

                                        Color.FromArgb(153, 153, 153),
                                        Color.FromArgb(255, 255, 255),
                                        Color.FromArgb(145, 145, 145)
                                    ),

                /* N64 */           (
                                        Color.FromArgb(254, 254, 254),
                                        Color.FromArgb(240, 242, 255),
                                        Color.FromArgb(0, 0, 120),

                                        Color.FromArgb(80, 80, 255),

                                        Color.FromArgb(30, 30, 120),
                                        Color.FromArgb(80, 80, 255),
                                        Color.FromArgb(255, 255, 255)
                                    ),

                /* SMS */           (
                                        Color.FromArgb(32, 32, 32),
                                        Color.FromArgb(44, 44, 44),
                                        Color.FromArgb(191, 191, 191),

                                        Color.FromArgb(185, 28, 34),

                                        Color.FromArgb(0, 0, 0),
                                        Color.FromArgb(241, 84, 90),
                                        Color.FromArgb(255, 255, 255)
                                    ),

                /* SMD */           (
                                        Color.FromArgb(40, 40, 40),
                                        Color.FromArgb(47, 47, 47),
                                        Color.FromArgb(180, 180, 180),

                                        Color.FromArgb(157, 63, 105),

                                        Color.FromArgb(0, 0, 0),
                                        Color.FromArgb(157, 63, 105),
                                        Color.FromArgb(255, 255, 255)
                                    ),

                /* TG-16 */         (
                                        Color.FromArgb(78, 78, 78),
                                        Color.FromArgb(95, 95, 95),
                                        Color.FromArgb(0, 0, 0),

                                        Color.FromArgb(180, 180, 180),

                                        Color.FromArgb(255, 80, 0),
                                        Color.FromArgb(64, 64, 64),
                                        Color.FromArgb(255, 80, 0)
                                    ),

                /* PCE */           (
                                        Color.FromArgb(255, 255, 255),
                                        Color.FromArgb(255, 232, 218),
                                        Color.FromArgb(40, 40, 50),

                                        Color.FromArgb(138, 138, 138),

                                        Color.FromArgb(255, 80, 0),
                                        Color.FromArgb(255, 255, 255),
                                        Color.FromArgb(255, 80, 0)
                                    ),

                /* NEO-GEO */       (
                                        Color.FromArgb(223, 223, 223),
                                        Color.FromArgb(220, 212, 198),
                                        Color.FromArgb(255, 248, 152),

                                        Color.FromArgb(191, 139, 0),

                                        Color.FromArgb(184, 31, 24),
                                        Color.FromArgb(255, 227, 29),
                                        Color.FromArgb(160, 107, 0)
                                    ),

                /* NEO-GEO MVS */   (
                                        Color.FromArgb(223, 223, 223),
                                        Color.FromArgb(198, 198, 198),
                                        Color.FromArgb(100, 100, 100),

                                        Color.FromArgb(191, 191, 191),

                                        Color.FromArgb(0, 0, 0),
                                        Color.FromArgb(255, 51, 51),
                                        Color.FromArgb(255, 255, 255)
                                    ),

                /* C64 */           (
                                        Color.FromArgb(209, 209, 209),
                                        Color.FromArgb(201, 201, 201),
                                        Color.FromArgb(37, 72, 186),

                                        Color.FromArgb(79, 114, 228),

                                        Color.FromArgb(63, 98, 212),
                                        Color.FromArgb(166, 196, 255),
                                        Color.FromArgb(28, 63, 186)
                                    ),

                /* MSX */           (
                                        Color.FromArgb(55, 55, 255),
                                        Color.FromArgb(3, 0, 120),
                                        Color.FromArgb(0, 0, 120),

                                        Color.FromArgb(255, 255, 255),

                                        Color.FromArgb(60, 60, 60),
                                        Color.FromArgb(255, 255, 255),
                                        Color.FromArgb(90, 90, 90)
                                    ),

                /* MSX MOD */       (
                                        Color.FromArgb(55, 55, 255),
                                        Color.FromArgb(0, 0, 247),
                                        Color.FromArgb(0, 0, 120),

                                        Color.FromArgb(255, 255, 255),

                                        Color.FromArgb(60, 60, 60),
                                        Color.FromArgb(255, 255, 255),
                                        Color.FromArgb(90, 90, 90)
                                    ),

                /* Flash */         (
                                        Color.FromArgb(40, 40, 40),
                                        Color.FromArgb(47, 47, 47),
                                        Color.FromArgb(180, 180, 180),

                                        Color.FromArgb(94, 49, 52),

                                        Color.FromArgb(0, 0, 0),
                                        Color.FromArgb(174, 30, 37),
                                        Color.FromArgb(255, 255, 255)
                                    ),

                /* PlayStation */   (
                                        Color.FromArgb(234, 234, 234),
                                        Color.FromArgb(223, 223, 223),
                                        Color.FromArgb(100, 100, 100),

                                        Color.FromArgb(100, 100, 100),

                                        Color.FromArgb(0, 0, 0),
                                        Color.FromArgb(0, 0, 0),
                                        Color.FromArgb(255, 255, 255)
                                    ),

                /* RPG Maker */     (
                                        Color.FromArgb(6, 239, 230),
                                        Color.FromArgb(230, 234, 222),
                                        Color.FromArgb(165, 190, 150),

                                        Color.FromArgb(83, 118, 61),

                                        Color.FromArgb(46, 61, 37),
                                        Color.FromArgb(86, 140, 58),
                                        Color.FromArgb(255, 255, 255)
                                    )
            };
        }

        public static Color TextColor(int target)
        {
            var limit = target == 15 || target == 16 ? 0.5 : 0.75;
            return List[target].bgBottom.GetBrightness() < limit || target == 4 || target == 2 ? Color.White : Color.Black;
        }
    }
}
