using System.Collections.Generic;

namespace FriishProduce
{
    public enum Platforms
    {
        NES = 0,
        SNES = 1,
        N64 = 2,
        SMS = 4,
        SMD = 5,
        PCE = 10,
        NeoGeo = 6,
        C64 = 7,
        MSX = 8,
        Flash = 3,
    }

    public enum InjectionMethod
    {
        VC = 0,
        FCEUGX = 1,
        Snes9xGX = 2,
        VBAGX = 3,
        GenPlusGX = 4,
        LibRetro = 5
    }
}
