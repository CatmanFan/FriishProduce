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

    public static class Filters
    {
        public static System.Collections.Generic.List<(Platform Platform, string[] Extensions)> ROM = new()
        {
            (Platform.NES, new string[] { ".nes" }),
            (Platform.SNES, new string[] { ".sfc", ".smc" }),
            (Platform.N64, new string[] { ".n64", ".v64", ".z64" }),
            (Platform.SMS, new string[] { ".sms" }),
            (Platform.S32X, new string[] { ".bin", ".gen", ".md" }),
            (Platform.SMD, new string[] { ".bin", ".gen", ".md" }),
            (Platform.PCE, new string[] { ".pce" }),
            (Platform.NEO, new string[] { ".zip" }),
            (Platform.MSX, new string[] { ".rom", ".mx1", ".mx2" }),
            (Platform.C64, new string[] { ".t64" }),
            (Platform.GB, new string[] { ".gb" }),
            (Platform.GBC, new string[] { ".gbc" }),
            (Platform.GBA, new string[] { ".gba" }),
        };
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
