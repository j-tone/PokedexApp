using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexNamespace
{
    public enum MoveType
    {
        Physical, Special, Status
    }
    class Move
    {
        private string name;
        private string description;
        private MoveType type;

        private int basePower;
        private int baseAccuracy;
        private string secondaryEffectDesc;
        private string secondaryEffectAcc;


    }
}
