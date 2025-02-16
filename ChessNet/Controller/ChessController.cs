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
                // Show the board
                view.DisplayBoard(board);
                // Determine whose turn it is
                if (board.IsWhiteTurn || !againstComputer)
                {
                    // Ask the user for their move
                    Console.Write(board.IsWhiteTurn ? "White's move: " : "Black's move: ");
                    string move = Console.ReadLine();
                    // If the user wants to exit, break out of the loop
                    if (move.ToLower() == "exit") break;

                    // Attempt to make the move
                    if (!board.MovePiece(move))
                    {
                        // If the move was invalid, show an error message and continue
                        view.ShowMessage("Invalid move. Try again.");
                        continue;
                    }
                }
                else
                {
                    // If we are playing against the computer, have the computer make its move
                    view.ShowMessage("Computer is thinking...");
                    // Pause for a second to make it look like the computer is thinking
                    System.Threading.Thread.Sleep(1000);
                    // Get the computer's move
                    string computerMove = ai.GetRandomMove(board);
                    // Attempt to make the move
                    if (computerMove != null)
                    {
                        board.MovePiece(computerMove);
                        // Show the move that the computer made
                        view.ShowMessage($"Computer moved: {computerMove}");
                    }
                }
            }
        }
    }
}
