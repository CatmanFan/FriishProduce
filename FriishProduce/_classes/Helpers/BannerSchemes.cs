using System.Collections.Generic;
using System.Drawing;

namespace FriishProduce
{
    public static class BannerSchemes
    {
        public static List<((int R, int G, int B) bg, (int R, int G, int B) bgLogo, (int R, int G, int B) bgBottom, (int R, int G, int B) lines, (int R, int G, int B) topBorder, (int R, int G, int B) topBG, (int R, int G, int B) topText)> List
        {
            get => new List<((int R, int G, int B) bg, (int R, int G, int B) bgLogo, (int R, int G, int B) bgBottom, (int R, int G, int B) lines, (int R, int G, int B) topBorder, (int R, int G, int B) topBG, (int R, int G, int B) topText)>
            {
                // ****************

                /* NES */           (
                                        (250, 250, 250),  // Main Background Color
                                        (255, 230, 230),  // Background Logo(s)
                                        (100, 100, 100),  // Bottom Section

                                        (155, 155, 155),  // Left Lines

                                        (144, 144, 144),  // PFLine Border
                                        (210, 210, 210),  // PFLine BG Color
                                        (230, 40, 40)     // Text_PF
                                    ),

                /* FC */            (
                                        (250, 250, 250),
                                        (240, 240, 240),
                                        (200, 200, 200),

                                        (155, 155, 155),

                                        (122, 48, 48),
                                        (217, 31, 31),
                                        (230, 230, 40)
                                    ),

                /* SNES */          (
                                        (209, 209, 209),
                                        (200, 200, 200),
                                        (60, 40, 70),

                                        (120, 120, 120),

                                        (127, 88, 149),
                                        (190, 146, 255),
                                        (100, 60, 120)
                                    ),

                /* SFC */           (
                                        (209, 209, 209),
                                        (200, 200, 200),
                                        (0, 0, 0),

                                        (120, 120, 120),

                                        (153, 153, 153),
                                        (255, 255, 255),
                                        (145, 145, 145)
                                    ),

                /* N64 */           (
                                        (254, 254, 254),
                                        (240, 242, 255),
                                        (0, 0, 120),

                                        (80, 80, 255),

                                        (30, 30, 120),
                                        (80, 80, 255),
                                        (255, 255, 255)
                                    ),

                /* SMS */           (
                                        (32, 32, 32),
                                        (44, 44, 44),
                                        (191, 191, 191),

                                        (185, 28, 34),

                                        (0, 0, 0),
                                        (241, 84, 90),
                                        (255, 255, 255)
                                    ),

                /* SMD */           (
                                        (40, 40, 40),
                                        (47, 47, 47),
                                        (180, 180, 180),

                                        (157, 63, 105),

                                        (0, 0, 0),
                                        (157, 63, 105),
                                        (255, 255, 255)
                                    ),

                /* TG-16 */         (
                                        (78, 78, 78),
                                        (95, 95, 95),
                                        (0, 0, 0),

                                        (180, 180, 180),

                                        (255, 80, 0),
                                        (64, 64, 64),
                                        (255, 80, 0)
                                    ),

                /* PCE */           (
                                        (255, 255, 255),
                                        (255, 232, 218),
                                        (40, 40, 50),

                                        (138, 138, 138),

                                        (255, 80, 0),
                                        (255, 255, 255),
                                        (255, 80, 0)
                                    ),

                /* NEO-GEO */       (
                                        (223, 223, 223),
                                        (220, 212, 198),
                                        (255, 248, 152),

                                        (191, 139, 0),

                                        (184, 31, 24),
                                        (255, 227, 29),
                                        (160, 107, 0)
                                    ),

                /* NEO-GEO MVS */   (
                                        (223, 223, 223),
                                        (198, 198, 198),
                                        (100, 100, 100),

                                        (191, 191, 191),

                                        (0, 0, 0),
                                        (255, 51, 51),
                                        (255, 255, 255)
                                    ),

                /* C64 */           (
                                        (209, 209, 209),
                                        (201, 201, 201),
                                        (37, 72, 186),

                                        (79, 114, 228),

                                        (63, 98, 212),
                                        (166, 196, 255),
                                        (28, 63, 186)
                                    ),

                /* MSX */           (
                                        (55, 55, 255),
                                        (3, 0, 120),
                                        (0, 0, 120),

                                        (255, 255, 255),

                                        (60, 60, 60),
                                        (255, 255, 255),
                                        (90, 90, 90)
                                    ),

                /* MSX MOD */       (
                                        (55, 55, 255),
                                        (0, 0, 247),
                                        (0, 0, 120),

                                        (255, 255, 255),

                                        (60, 60, 60),
                                        (255, 255, 255),
                                        (90, 90, 90)
                                    ),

                /* Flash */         (
                                        (40, 40, 40),
                                        (47, 47, 47),
                                        (180, 180, 180),

                                        (94, 49, 52),

                                        (0, 0, 0),
                                        (174, 30, 37),
                                        (255, 255, 255)
                                    ),

                /* PlayStation */   (
                                        (234, 234, 234),
                                        (223, 223, 223),
                                        (100, 100, 100),

                                        (100, 100, 100),

                                        (0, 0, 0),
                                        (0, 0, 0),
                                        (255, 255, 255)
                                    ),

                /* RPG Maker */     (
                                        (239, 242, 233),
                                        (229, 231, 221),
                                        (165, 190, 150),

                                        (83, 118, 61),

                                        (46, 61, 37),
                                        (86, 140, 58),
                                        (255, 255, 255)
                                    )
            };
        }

        public static (int R, int G, int B) TextColor(int target)
        {
            var limit = target == 15 || target == 16 ? 0.5 : 0.75;
            return GetBrightness(target, 2) < limit || target == 4 || target == 2 ? (255, 255, 255) : (0, 0, 0);
        }

        public static float GetBrightness(int target, int type)
        {
            var (R, G, B) = type switch
            {
                1 => List[target].bgLogo,
                2 => List[target].bgBottom,
                3 => List[target].lines,
                4 => List[target].topBorder,
                5 => List[target].topBG,
                6 => List[target].topText,
                7 => TextColor(target),
                _ => List[target].bg
            };

            return ((R + G + B) / 3f) / 255f;
        }

        /// <summary>
        /// 0 = BG, 1 = BG's logo, 2 = Bottom gradient, 3 = Left lines, 4 = Top header border, 5 = Top header BG, 6 = Top header text
        /// </summary>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Color GetColor(int target, int type)
        {
            var (R, G, B) = type switch
            {
                1 => List[target].bgLogo,
                2 => List[target].bgBottom,
                3 => List[target].lines,
                4 => List[target].topBorder,
                5 => List[target].topBG,
                6 => List[target].topText,
                7 => TextColor(target),
                _ => List[target].bg
            };

            return Color.FromArgb(R, G, B);
        }
    }
}
