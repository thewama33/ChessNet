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
            Console.WriteLine("\nType 'hint' anytime for a suggested move!");
        }

        public void DisplayBoard(ChessBoard board, string lastMove = "")
        {
            char[,] currentBoard = board.GetBoard();

            // Move cursor to top-left without clearing
            Console.SetCursorPosition(0, 0);

            Console.WriteLine("    a   b   c   d   e   f   g   h  ");
            Console.WriteLine("  +---+---+---+---+---+---+---+---+");

            for (int i = 0; i < 8; i++)
            {
                Console.Write((8 - i) + " | ");
                for (int j = 0; j < 8; j++)
                    Console.Write(TranslatePiece(currentBoard[i, j]) + " | ");
                Console.WriteLine(8 - i);
                Console.WriteLine("  +---+---+---+---+---+---+---+---+");
            }

            Console.WriteLine("    a   b   c   d   e   f   g   h  ");

            if (!string.IsNullOrEmpty(lastMove))
            {
                Console.WriteLine($"Last Move: {lastMove}");
            }
        }


        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        static char TranslatePiece(char piece)
        {
            // Unicode Chess Pieces (Optional)
            switch (piece)
            {
                case 'P': return '♙';
                case 'p': return '♟';
                case 'R': return '♖';
                case 'r': return '♜';
                case 'N': return '♘';
                case 'n': return '♞';
                case 'B': return '♗';
                case 'b': return '♝';
                case 'Q': return '♕';
                case 'q': return '♛';
                case 'K': return '♔';
                case 'k': return '♚';
                default: return ' '; // Empty square
            }
        }
    }
}
