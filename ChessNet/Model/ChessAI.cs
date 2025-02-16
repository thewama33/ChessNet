namespace ChessNet.Model
{
    public class ChessAI
    {
        private Random random = new Random();

        /// <summary>
        /// Gets the best move for the AI by prioritizing captures.
        /// </summary>
        public string? GetBestMove(ChessBoard board)
        {
            List<string> moves = GetAllPossibleMoves(board);
            if (moves.Count == 0) return null; // No moves available

            string bestMove = moves[0];
            int highestScore = int.MinValue;

            foreach (string move in moves)
            {
                int fromRow = 8 - (move[1] - '0');
                int fromCol = move[0] - 'a';
                int toRow = 8 - (move[3] - '0');
                int toCol = move[2] - 'a';

                int score = EvaluateMove(board, fromRow, fromCol, toRow, toCol);

                if (score > highestScore)
                {
                    highestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }


        /// <summary>
        /// Generates a list of all possible LEGAL moves for the AI player.
        /// </summary>
        private List<string> GetAllPossibleMoves(ChessBoard board)
        {
            List<string> possibleMoves = new List<string>();
            char[,] currentBoard = board.GetBoard();

            for (int fromRow = 0; fromRow < 8; fromRow++)
            {
                for (int fromCol = 0; fromCol < 8; fromCol++)
                {
                    char piece = currentBoard[fromRow, fromCol];

                    if (char.IsLower(piece) == !board.IsWhiteTurn) // AI plays as black
                    {
                        for (int toRow = 0; toRow < 8; toRow++)
                        {
                            for (int toCol = 0; toCol < 8; toCol++)
                            {
                                if (board.IsMoveLegal(fromRow, fromCol, toRow, toCol))
                                {
                                    // Simulate the move
                                    char temp = currentBoard[toRow, toCol];
                                    currentBoard[toRow, toCol] = currentBoard[fromRow, fromCol];
                                    currentBoard[fromRow, fromCol] = '.';

                                    // Ensure AI does not move into check
                                    if (!board.IsKingInCheck(false, currentBoard))
                                    {
                                        string move = $"{(char)('a' + fromCol)}{(8 - fromRow)}{(char)('a' + toCol)}{(8 - toRow)}";
                                        possibleMoves.Add(move);
                                    }

                                    // Undo the simulated move
                                    currentBoard[fromRow, fromCol] = currentBoard[toRow, toCol];
                                    currentBoard[toRow, toCol] = temp;
                                }
                            }
                        }
                    }
                }
            }
            return possibleMoves;
        }


        /// <summary>
        /// Provides a hint move for the player.
        /// </summary>
        public string GetHintMove(ChessBoard board)
        {
            List<string> legalMoves = board.GetLegalMoves();
            if (legalMoves.Count == 0) return "No available moves!";

            // Try to find a capturing move first
            foreach (string move in legalMoves)
            {
                int fromRow = 8 - (move[1] - '0');
                int fromCol = move[0] - 'a';
                int toRow = 8 - (move[3] - '0');
                int toCol = move[2] - 'a';

                if (board.IsCaptureMove(fromRow, fromCol, toRow, toCol))
                {
                    return $"Hint: Try capturing with {move}";
                }
            }

            // Otherwise, suggest any legal move
            return $"Hint: Consider {legalMoves[random.Next(legalMoves.Count)]}";
        }


        private int EvaluateMove(ChessBoard board, int fromRow, int fromCol, int toRow, int toCol)
        {
            char[,] boardState = board.GetBoard();
            char targetPiece = boardState[toRow, toCol];

            // Piece value system
            Dictionary<char, int> pieceValues = new Dictionary<char, int>
    {
        { 'p', 1 }, { 'n', 3 }, { 'b', 3 }, { 'r', 5 }, { 'q', 9 }, { 'k', 100 },
        { 'P', 1 }, { 'N', 3 }, { 'B', 3 }, { 'R', 5 }, { 'Q', 9 }, { 'K', 100 }
    };

            int score = 0;

            // If capturing an enemy piece, add its value to score
            if (targetPiece != '.' && (char.IsUpper(targetPiece) != char.IsUpper(boardState[fromRow, fromCol])))
            {
                score += pieceValues[targetPiece];
            }

            // Bonus for moving toward the center (encourages better positioning)
            int centerBonus = (3 - Math.Abs(3 - toRow)) + (3 - Math.Abs(3 - toCol));
            score += centerBonus;

            return score;
        }

    }
}
