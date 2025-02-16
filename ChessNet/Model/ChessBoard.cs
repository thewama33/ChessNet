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

        public bool IsValidMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            char piece = board[fromRow, fromCol];
            char target = board[toRow, toCol];

            bool isWhite = char.IsUpper(piece);
            bool isMovingOwnPiece = (IsWhiteTurn && isWhite) || (!IsWhiteTurn && !isWhite);
            bool isCapturingOwnPiece = char.IsLetter(target) && (char.IsUpper(target) == char.IsUpper(piece));

            if (!isMovingOwnPiece || isCapturingOwnPiece) return false;

            int rowDiff = toRow - fromRow;
            int colDiff = toCol - fromCol;

            switch (char.ToLower(piece))
            {
                case 'p': // **Pawn Movement**
                    return IsValidPawnMove(fromRow, fromCol, toRow, toCol);
                case 'n': // **Knight Movement (L-shape)**
                    return (Math.Abs(rowDiff) == 2 && Math.Abs(colDiff) == 1) || (Math.Abs(rowDiff) == 1 && Math.Abs(colDiff) == 2);
                case 'b': // **Bishop Movement (Diagonal)**
                    return Math.Abs(rowDiff) == Math.Abs(colDiff) && IsPathClear(fromRow, fromCol, toRow, toCol);
                case 'r': // **Rook Movement (Straight)**
                    return (fromRow == toRow || fromCol == toCol) && IsPathClear(fromRow, fromCol, toRow, toCol);
                case 'q': // **Queen Movement (Bishop + Rook)**
                    return (Math.Abs(rowDiff) == Math.Abs(colDiff) || fromRow == toRow || fromCol == toCol) && IsPathClear(fromRow, fromCol, toRow, toCol);
                case 'k': // **King Movement (1 Step Any Direction)**
                    return Math.Abs(rowDiff) <= 1 && Math.Abs(colDiff) <= 1;
                default:
                    return false;
            }
        }

        private bool IsValidPawnMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            char piece = board[fromRow, fromCol];
            int direction = (char.IsUpper(piece)) ? -1 : 1; // White moves up (-1), Black moves down (+1)

            // Normal Move (1 square forward)
            if (fromCol == toCol && toRow == fromRow + direction && board[toRow, toCol] == '.')
                return true;

            // Double Move (2 squares forward from starting position)
            if (fromCol == toCol && toRow == fromRow + (2 * direction) && board[toRow, toCol] == '.' &&
                ((char.IsUpper(piece) && fromRow == 6) || (!char.IsUpper(piece) && fromRow == 1)) &&
                board[fromRow + direction, fromCol] == '.')
                return true;

            // Capture (Diagonal)
            if (Math.Abs(toCol - fromCol) == 1 && toRow == fromRow + direction && board[toRow, toCol] != '.' &&
                char.IsUpper(board[toRow, toCol]) != char.IsUpper(piece))
                return true;

            return false;
        }

        private bool IsPathClear(int fromRow, int fromCol, int toRow, int toCol)
        {
            int rowStep = (toRow > fromRow) ? 1 : (toRow < fromRow) ? -1 : 0;
            int colStep = (toCol > fromCol) ? 1 : (toCol < fromCol) ? -1 : 0;

            int row = fromRow + rowStep, col = fromCol + colStep;
            while (row != toRow || col != toCol)
            {
                if (board[row, col] != '.') return false;
                row += rowStep;
                col += colStep;
            }
            return true;
        }




        public List<string> GetLegalMoves()
        {
            List<string> possibleMoves = new List<string>();
            for (int fromRow = 0; fromRow < 8; fromRow++)
            {
                for (int fromCol = 0; fromCol < 8; fromCol++)
                {
                    char piece = board[fromRow, fromCol];
                    if (char.IsUpper(piece) == IsWhiteTurn) // Check if piece belongs to the current player
                    {
                        for (int toRow = 0; toRow < 8; toRow++)
                        {
                            for (int toCol = 0; toCol < 8; toCol++)
                            {
                                if (IsValidMove(fromRow, fromCol, toRow, toCol))
                                {
                                    string move = $"{(char)('a' + fromCol)}{(8 - fromRow)}{(char)('a' + toCol)}{(8 - toRow)}";
                                    possibleMoves.Add(move);
                                }
                            }
                        }
                    }
                }
            }
            return possibleMoves;
        }

        public bool IsCaptureMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            char[,] boardState = GetBoard();
            char piece = boardState[fromRow, fromCol];
            char target = boardState[toRow, toCol];

            // Ensure target is an enemy piece
            return target != '.' && (char.IsUpper(target) != char.IsUpper(piece));
        }

        public bool IsMoveLegal(int fromRow, int fromCol, int toRow, int toCol)
        {
            // 1. Check basic move validity
            if (!IsValidMove(fromRow, fromCol, toRow, toCol)) return false;

            // 2. Simulate the move
            char[,] tempBoard = (char[,])board.Clone();
            tempBoard[toRow, toCol] = tempBoard[fromRow, fromCol];
            tempBoard[fromRow, fromCol] = '.';

            // 3. Check if the king would be in check after this move
            return !IsKingInCheck(IsWhiteTurn, tempBoard);
        }

        public bool IsKingInCheck(bool isWhite, char[,] tempBoard)
        {
            int kingRow = -1, kingCol = -1;

            // Find the king's position
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    char piece = tempBoard[row, col];

                    if ((isWhite && piece == 'K') || (!isWhite && piece == 'k'))
                    {
                        kingRow = row;
                        kingCol = col;
                        break;
                    }
                }
            }

            if (kingRow == -1 || kingCol == -1) return false; // King not found (shouldn't happen)

            // Check if any opponent piece can attack the king
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    char piece = tempBoard[row, col];

                    // Skip empty squares and same-color pieces
                    if (piece == '.' || (char.IsUpper(piece) == isWhite)) continue;

                    // If the opponent can move to the king’s square, the king is in check
                    if (IsValidMove(row, col, kingRow, kingCol)) return true;
                }
            }

            return false;
        }



    }
}

