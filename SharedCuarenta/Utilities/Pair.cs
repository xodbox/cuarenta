using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCuarenta.Utilities
{
    public class Pair<T, U>
    {
        public T First { get; set; }
        public U Second { get; set; }

        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }
    }
}
