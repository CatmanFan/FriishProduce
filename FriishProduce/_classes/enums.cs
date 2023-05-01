using System.Collections.Generic;

namespace FriishProduce
{
    public enum Platforms
    {
        NES = 0,
        SNES = 1,
        N64 = 2,
        SMS = 3,
        SMD = 4,
        PCE = 10,
        NeoGeo = 6,
        C64 = 7,
        MSX = 8,
        Flash = 5,
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
