using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListHad
{
    internal struct Souradnice
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Souradnice(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
