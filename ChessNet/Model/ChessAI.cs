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

            // Prioritize capturing moves
            foreach (string move in moves)
            {
                int fromRow = 8 - (move[1] - '0');
                int fromCol = move[0] - 'a';
                int toRow = 8 - (move[3] - '0');
                int toCol = move[2] - 'a';

                if (board.IsCaptureMove(fromRow, fromCol, toRow, toCol))
                {
                    return move; // Pick the capturing move
                }
            }

            // If no captures, pick a random move
            return moves[random.Next(moves.Count)];
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

                    // AI plays as black (lowercase pieces)
                    if (char.IsLower(piece) == !board.IsWhiteTurn)
                    {
                        for (int toRow = 0; toRow < 8; toRow++)
                        {
                            for (int toCol = 0; toCol < 8; toCol++)
                            {
                                if (board.IsMoveLegal(fromRow, fromCol, toRow, toCol)) // ✅ Corrected!
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
    }
}
