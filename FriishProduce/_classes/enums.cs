using System.Collections.Generic;

namespace FriishProduce
{
    public enum Platforms
    {
        NES = 0,
        SNES = 1,
        N64 = 2,
        Flash = 3,
        SMD = 4,
        PCE = 5,
        NeoGeo = 6,
        C64 = 7,
        MSX = 8,
        SMS = 9,

        /* FORWARDERS */
        Forwarders = 10
    }

    public enum InjectionMethod
    {
        VC = 0,
        FCEGX = 1,
        Snes9xGX = 2,
        VBAGX = 3,
        GenPlusGX = 4,
        LibRetro = 5
    }
}
