using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using ChessNet.Model;
using ChessNet.View;

namespace ChessNet.Controller
{
    public class ChessController
    {
        private ChessBoard board;
        private ChessAI ai;
        private ChessView view;
        private bool againstComputer;

        public ChessController()
        {
            board = new ChessBoard();
            ai = new ChessAI();
            view = new ChessView();
        }

        /// <summary>
        /// Starts the game. Shows the start menu, and then starts the game loop.
        /// </summary>
        public void StartGame()
        {
            // Show the start menu
            view.ShowStartMenu();
            // Get the user's choice
            string choice = Console.ReadLine();
            // Set the flag to determine if we are playing against the computer
            againstComputer = (choice == "2");

            while (true)
            {
                view.DisplayBoard(board);

                if (board.IsWhiteTurn || !againstComputer)
                {
                    Console.Write(board.IsWhiteTurn ? "White's move (or type 'hint'): " : "Black's move (or type 'hint'): ");
                    string move = Console.ReadLine();

                    if (move.ToLower() == "exit") break;
                    if (move.ToLower() == "hint")
                    {
                        string hintMove = ai.GetHintMove(board);
                        view.ShowMessage($"Hint: Try {hintMove}");
                        continue; // Let the player decide to use it or not
                    }

                    if (!board.MovePiece(move))
                    {
                        view.ShowMessage("Invalid move. Try again.");
                        continue;
                    }
                }
                else
                {
                    view.ShowMessage("Computer is thinking...");
                    System.Threading.Thread.Sleep(1000);
                    string computerMove = ai.GetBestMove(board);
                    if (computerMove != null)
                    {
                        board.MovePiece(computerMove);
                        view.ShowMessage($"Computer moved: {computerMove}");
                    }
                }
            }

        }
    }
}
