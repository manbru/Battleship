using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Model
{
    public class Ship
    {
        private LinkedList<ShipSquare> internalList = new LinkedList<ShipSquare>();
        
        private int hits = 0;
        private int maxHits;
        public int id;
        static int count = 0;
        private List<SeaSquare> squareList= new List<SeaSquare>();

        public Ship(int length) 
        {
            this.id = count++;
            this.maxHits = length;

        }
        public int Hits { get; set; }

        public void AddSquare(SeaSquare square)
        {
            squareList.Add(square);
        }
        public void Remove()
        {
            foreach (SeaSquare square in squareList)
            {
                square.Ship = null;
            }
            this.squareList.Clear();
            count--;
        }

    }
}
