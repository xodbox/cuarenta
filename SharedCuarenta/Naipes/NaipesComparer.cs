using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCuarenta.Naipes
{
    class NaipesComparer:Comparer<Naipe>
    {
        //compares by rank value
        public override int Compare(Naipe x, Naipe y)
        {
            return (int)x.Rank - (int)y.Rank;
        }
    }
}
