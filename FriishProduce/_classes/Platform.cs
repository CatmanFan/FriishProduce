namespace FriishProduce
{
    public enum Platform
    {
        NES,
        SNES,
        N64,
        SMS,
        SMD,
        PCE,
        PCECD,
        NEO,
        MSX,
        C64,
        Flash,
        GB,
        GBC,
        GBA,
        GCN,
        S32X,
        SMCD,
        PSX,
        RPGM,
    }

    public static class Platforms
    {
        public static System.Collections.Generic.Dictionary<Platform, System.Drawing.Bitmap> Icons
        {
            get
            {
                System.Collections.Generic.Dictionary<Platform, System.Drawing.Icon> orig = new()
                {
                    {
                        Platform.NES,
                        Program.Lang.GetRegion() is Language.Region.Japan or Language.Region.Korea
                        ? FileDatas.Icons.fc : FileDatas.Icons.nes
                    },

                    {
                        Platform.SNES,
                        Program.Lang.GetRegion() is Language.Region.Americas or Language.Region.International
                        ? FileDatas.Icons.snes : FileDatas.Icons.sfc
                    },

                    {
                        Platform.N64,
                        FileDatas.Icons.n64
                    },

                    {
                        Platform.SMS,
                        FileDatas.Icons.sms
                    },

                    {
                        Platform.SMD,
                        FileDatas.Icons.smd
                    },
                    
                    {
                        Platform.PCE, 
                        Program.Lang.GetRegion() is Language.Region.Japan
                        ? FileDatas.Icons.pce : FileDatas.Icons.tg16
                    },

                    {
                        Platform.PCECD,
                        FileDatas.Icons.pcecd
                    },

                    {
                        Platform.NEO,
                        FileDatas.Icons.neo
                    },

                    {
                        Platform.PSX,
                        FileDatas.Icons.psx
                    },

                    {
                        Platform.RPGM,
                        FileDatas.Icons.rpg2000
                    }
                };

                System.Collections.Generic.Dictionary<Platform, System.Drawing.Bitmap> resized = new();

                foreach (var item in orig)
                    if (item.Value != null)
                    {
                        System.Drawing.Bitmap bmp = new(16, 16);
                        using System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
                        using System.Drawing.Bitmap origBmp = item.Value.ToBitmap();

                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        g.DrawImage(origBmp, 0, 0, bmp.Width, bmp.Height);

                        resized.Add(item.Key, bmp);
                    }

                foreach (var missing in new System.Collections.Generic.Dictionary<Platform, System.Drawing.Bitmap>()
                {
                    { Platform.C64, FileDatas.Icons.c64 },
                    { Platform.MSX, FileDatas.Icons.msx },
                    { Platform.Flash, FileDatas.Icons.flash },
                })
                {
                    if (!resized.ContainsKey(missing.Key))
                        resized.Add(missing.Key, missing.Value);
                }

                return resized;
            }
        }

        public static System.Collections.Generic.Dictionary<Platform?, string[]> Filters
        {
            get => new()
            {
                { Platform.NES, new string[] { ".nes" } },
                { Platform.SNES, new string[] { ".sfc", ".smc" } },
                { Platform.N64, new string[] { ".n64", ".v64", ".z64" } },
                { Platform.SMS, new string[] { ".sms" } },
                { Platform.S32X, new string[] { ".bin", ".gen", ".md" } },
                { Platform.SMD, new string[] { ".bin", ".gen", ".md" } },
                { Platform.PCE, new string[] { ".pce" } },
                { Platform.NEO, new string[] { ".zip" } },
                { Platform.MSX, new string[] { ".rom", ".mx1", ".mx2" } },
                { Platform.C64, new string[] { ".d64", /* ".t64" */ } },
                { Platform.GB, new string[] { ".gb" } },
                { Platform.GBC, new string[] { ".gbc" } },
                { Platform.GBA, new string[] { ".gba" } },
            };
        }
    }

    public enum Buttons
    {
        WiiRemote_Up,
        WiiRemote_Left,
        WiiRemote_Right,
        WiiRemote_Down,
        WiiRemote_A,
        WiiRemote_B,
        WiiRemote_1,
        WiiRemote_2,
        WiiRemote_Plus,
        WiiRemote_Minus,
        WiiRemote_Home,
        Nunchuck_Z,
        Nunchuck_C,
        Classic_Up,
        Classic_Left,
        Classic_Right,
        Classic_Down,
        Classic_Up_L,
        Classic_Left_L,
        Classic_Right_L,
        Classic_Down_L,
        Classic_Up_R,
        Classic_Left_R,
        Classic_Right_R,
        Classic_Down_R,
        Classic_A,
        Classic_B,
        Classic_X,
        Classic_Y,
        Classic_L,
        Classic_R,
        Classic_ZL,
        Classic_ZR,
        Classic_Plus,
        Classic_Minus,
        Classic_Home,
        Classic_Reserved,
        GC_Up,
        GC_Left,
        GC_Right,
        GC_Down,
        GC_Up_L,
        GC_Left_L,
        GC_Right_L,
        GC_Down_L,
        GC_C_Up,
        GC_C_Left,
        GC_C_Right,
        GC_C_Down,
        GC_A,
        GC_B,
        GC_X,
        GC_Y,
        GC_L,
        GC_R,
        GC_Z,
        GC_Start
    };
}
