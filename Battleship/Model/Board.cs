using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Model
{
    public class Board
    {
        public SeaSquare[,] InternalBoard { get; private set; }
        private int size = 0;
        private LinkedList<int> lengthList = new LinkedList<int> (new[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });
        private Ship[] ShipArray = new Ship[10];
        Random rnd = new Random();

        public Board(int size)
        {
            this.InternalBoard = new SeaSquare[size, size];
            this.size = size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    InternalBoard[i,j] = new SeaSquare();   //fill board with seasquares
                }
            }
        }
        public bool DeployShips()
        {
            if(lengthList.Count == 0)   //check if all ships are placed
            {
                Debug.WriteLine("c");
                return true;    //process successfully completed
                
            }

            int index = rnd.Next(lengthList.Count);
            int length = lengthList.ElementAt(index);
            lengthList.Remove(lengthList.ElementAt(index)); //select random ship and remove it form input list

            LinkedList<int[]> allowed = new LinkedList<int[]>();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        
                        if(!DetectCollision(i, j, k, length))   //check if placement is legal for all placements on board
                        {
                            //Debug.WriteLine("d");
                            allowed.AddLast(new int[] { i, j, k }); //list of all possible placements
                        }
                    }
                }
            }
            //if (allowed.Count == 0) 
            //{
                
            //    Debug.WriteLine("b");
            //    return false; 
            //}
            while (allowed.Count > 0)
            {
                Ship cShip = new Ship(length);
                int[] chosen = allowed.ElementAt(rnd.Next(allowed.Count));
                allowed.Remove(chosen);     //chose and remove a possible placement
                if (chosen[0] == 0)         //if horizontal
                {

                    for (int i = 0; i < length; i++)
                    {
                        SeaSquare cSquare = this.InternalBoard[chosen[1] + i, chosen[2]];   //place ship
                        cShip.AddSquare(cSquare);
                        cSquare.Ship = cShip;

                    }
                }
                else
                {
                    for (int i = 0; i < length; i++)
                    {
                        SeaSquare cSquare = this.InternalBoard[chosen[1], chosen[2] + i];
                        cShip.AddSquare(cSquare);
                        cSquare.Ship = cShip;

                    }
                }
                Print();        // print board
                if (DeployShips()) { return true; }
                cShip.Remove();                 //remove ship
                lengthList.AddLast(length);     //add ship to list
                Debug.WriteLine("a");
            }
            
            return false;
        }
            
            
            
            //bool done = false;
            //while (!done)
            //{
            //    for (int i = 0; i < LengthArray.Length; i++)
            //    {
            //        bool vertical = rnd.Next(0, 1) == 1; // generate random position and orientation
            //        int length = LengthArray[i];
            //        int x = rnd.Next(0, size - 1);
            //        int y = rnd.Next(0, size - length - 1);
            //        if (vertical)
            //        {

            //            for (int j = 0; j < length; j++)
            //            {
            //                InternalBoard[x, y + j].ships++; // mark as blocked, more than 1 is illegal
            //                if (x != 0)
            //                {
            //                    InternalBoard[x - 1, y + j].neighboring = true; // mark as neighboring, neighboring + blocked = illegal
            //                }
            //                if (x != size - 1)
            //                {
            //                    InternalBoard[x + 1, y + j].neighboring = true;
            //                }

            //            }
            //            if (y > 0)
            //            {
            //                InternalBoard[x, y - 1].neighboring = true;
            //            }
            //            if (y + length < size)
            //            {
            //                InternalBoard[x, y + length].neighboring = true;
            //            }
            //            if (y > 0 && x > 0)
            //            {
            //                InternalBoard[x - 1, y - 1].neighboring = true;
            //            }
            //            if (y + length < size && x > 0)
            //            {
            //                InternalBoard[x - 1, y + length].neighboring = true;
            //            }
            //            if (y > 0 && x < size - 1)
            //            {
            //                InternalBoard[x + 1, y - 1].neighboring = true;
            //            }
            //            if (y + length < size && x < size - 1)
            //            {
            //                InternalBoard[x + 1, y + length].neighboring = true;
            //            }



            //        }
            //        else
            //        {

            //            for (int j = 0; j < length; j++)
            //            {
            //                InternalBoard[x + j, y].ships++;
            //                if (y != 0)
            //                {
            //                    InternalBoard[x + j, y - 1].neighboring = true;
            //                }
            //                if (y != size - 1)
            //                {
            //                    InternalBoard[x + j, y + 1].neighboring = true;
            //                }
            //            }
            //            if (x > 0)
            //            {
            //                InternalBoard[x - 1, y].neighboring = true;
            //            }
            //            if (x + length < size)
            //            {
            //                InternalBoard[x + length, y].neighboring = true;
            //            }
            //            if (y > 0 && x > 0)
            //            {
            //                InternalBoard[x - 1, y - 1].neighboring = true;
            //            }
            //            if (x + length < size && y > 0)
            //            {
            //                InternalBoard[x + length, y - 1].neighboring = true;
            //            }
            //            if (x > 0 && y < size - 1)
            //            {
            //                InternalBoard[x - 1, y + 1].neighboring = true;
            //            }
            //            if (x + length < size && y < size - 1)
            //            {
            //                InternalBoard[x + length, y + 1].neighboring = true;
            //            }

            //        }



            //        //for (int j = 0; j < length; j++)
            //        //{
            //        //    /this.internalList.AddLast(new SeaSquare());
            //        //}
            //        done = true;
            //        foreach (SeaSquare square in InternalBoard) 
            //        {
            //            if (square.ships > 1 || square.ships == 1 && square.neighboring == true)
            //            {
            //                done = false;
            //            }
            //        }
            //    }
            //}
            
            
        
        private bool DetectCollision(int i, int j, int k, int length)
        {
            if (i == 0 && j + length > size || i == 1 && k + length > size) //collision with border
            {
                return true;
            }
            //Debug.WriteLine("f");
            for (int l = j - 1; l < j + 2 + (1 - i) * length; l++)
            {
                for (int m = k - 1; m < k + 2 + i * length; m++)
                {
                    if (l > -1 && l < size && m > -1 && m < size)
                    {
                        if (this.InternalBoard[l, m].Ship != null)  // collision with other ship
                        {
                            //Debug.WriteLine("g");
                            return true;
                        }
                    }
                }
            }
            //Debug.WriteLine("e");
            return false;
        }
        public void Print()
        {
            for (int i =0; i<size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (this.InternalBoard[i, j].Ship == null)
                    {
                        Debug.Write("-");
                    } 
                    else
                    {
                        Debug.Write(this.InternalBoard[i, j].Ship.id);
                    }
                    
                }
                Debug.Write('\n');
            }
            Debug.Write('\n');
        }
    }
}
