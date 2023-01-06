using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Model
{
    public class SeaSquare
    {
        int x;
        int y;
        //public int ships  = 0;
        private bool hit = false;
        //public bool neighboring = false;
        public Ship ship = null;

        public SeaSquare()
        {

        }
        public Ship Ship { get;set; }


    }
    
}
