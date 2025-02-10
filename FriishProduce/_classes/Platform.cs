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
                        Platform.NES, FileDatas.Icons.nes
                        // Program.Lang.GetRegion() is Language.Region.Japan or Language.Region.Korea
                        // ? FileDatas.Icons.fc : FileDatas.Icons.nes
                    },

                    {
                        Platform.SNES, FileDatas.Icons.snes
                        // Program.Lang.GetRegion() is Language.Region.Americas or Language.Region.International
                        // ? FileDatas.Icons.snes : FileDatas.Icons.sfc
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
                        Program.Lang.GetRegion() is Language.Region.Americas or Language.Region.International
                        ? FileDatas.Icons.gen : FileDatas.Icons.smd
                    },

                    {
                        Platform.PCE, FileDatas.Icons.tg16
                        // Program.Lang.GetRegion() is Language.Region.Japan
                        // ? FileDatas.Icons.pce : FileDatas.Icons.tg16
                    },

                    {
                        Platform.PCECD, FileDatas.Icons.tg16
                        // Program.Lang.GetRegion() is Language.Region.Japan
                        // ? FileDatas.Icons.pcecd : FileDatas.Icons.tg16
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
                    resized.Add(item.Key, new System.Drawing.Icon(item.Value, 16, 16).ToBitmap());

                resized.Add(Platform.C64, FileDatas.Icons.c64);
                resized.Add(Platform.MSX, FileDatas.Icons.msx);
                resized.Add(Platform.Flash, FileDatas.Icons.flash);

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
