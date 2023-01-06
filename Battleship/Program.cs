using Battleship.Model;
using System.Diagnostics;

namespace Battleship
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());

            var playerBoard = new Board(9);
            var computerBoard = new Board(9);
            computerBoard.DeployShips();
            computerBoard.Print();

            
            
            
        }
    }
}