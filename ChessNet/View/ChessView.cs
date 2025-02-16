using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using ChessNet.Model;

namespace ChessNet.View
{
    public class ChessView
    {
        public void ShowStartMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Console Chess!");
            Console.WriteLine("1 - Play with a Friend");
            Console.WriteLine("2 - Play against the Computer");
            Console.Write("Choose an option: ");
        }

        public void DisplayBoard(ChessBoard board)
        {
            char[,] currentBoard = board.GetBoard();
            Console.Clear();
            Console.WriteLine("    a   b   c   d   e   f   g   h  ");
            Console.WriteLine("  +---+---+---+---+---+---+---+---+");

            for (int i = 0; i < 8; i++)
            {
                Console.Write((8 - i) + " | ");
                for (int j = 0; j < 8; j++)
                    Console.Write(currentBoard[i, j] + " | ");
                Console.WriteLine(8 - i);
                Console.WriteLine("  +---+---+---+---+---+---+---+---+");
            }

            Console.WriteLine("    a   b   c   d   e   f   g   h  \n");
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
