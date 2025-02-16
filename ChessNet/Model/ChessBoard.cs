using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace ChessNet.Model
{
    public class ChessBoard
    {
        private char[,] board;
        public bool IsWhiteTurn { get; private set; } = true;

        public ChessBoard()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            string[] setup = {
                "rnbqkbnr",
                "pppppppp",
                "........",
                "........",
                "........",
                "........",
                "PPPPPPPP",
                "RNBQKBNR"
            };

            board = new char[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    board[i, j] = setup[i][j];
        }

        public char[,] GetBoard() => board;

        public bool MovePiece(string move)
        {
            if (move.Length != 4) return false;

            int fromRow = 8 - (move[1] - '0');
            int fromCol = move[0] - 'a';
            int toRow = 8 - (move[3] - '0');
            int toCol = move[2] - 'a';

            if (IsValidMove(fromRow, fromCol, toRow, toCol))
            {
                board[toRow, toCol] = board[fromRow, fromCol];
                board[fromRow, fromCol] = '.';
                IsWhiteTurn = !IsWhiteTurn;
                return true;
            }
            return false;
        }

        private bool IsValidMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            char piece = board[fromRow, fromCol];
            if (piece == '.') return false; // No piece to move

            bool isWhite = char.IsUpper(piece);
            if ((IsWhiteTurn && !isWhite) || (!IsWhiteTurn && isWhite)) return false; // Wrong turn

            return true; // Placeholder: Implement real movement rules
        }
    }
}

