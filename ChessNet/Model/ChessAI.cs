
namespace ChessNet.Model
{
    public class ChessAI
    {
        /// <summary>
        /// Random number generator for selecting moves.
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Selects a random move from the list of all possible moves.
        /// </summary>
        /// <param name="board">The current state of the chessboard.</param>
        /// <returns>A random move in standard chess notation (e.g., "e2e4"), or null if no moves are available.</returns>
        public string? GetRandomMove(ChessBoard board)
        {
            //? Get all possible moves for the AI player
            var moves = GetAllPossibleMoves(board);

            // Return null if no moves are available
            if (moves.Count == 0) return null;

            // Select and return a random move from the list
            return moves[random.Next(moves.Count)];
        }

        /// <summary>
        /// Generates a list of all possible moves for the AI player based on the current state of the chessboard.
        /// </summary>
        /// <param name="board">The current state of the chessboard.</param>
        /// <returns>A list of strings representing all possible moves the AI player can make. Each move is represented in standard chess notation (e.g., "e2e4").</returns>
        private List<string> GetAllPossibleMoves(ChessBoard board)
        {
            List<string> possibleMoves = new List<string>();
            char[,] currentBoard = board.GetBoard();

            // Iterate over all squares on the board
            for (int fromRow = 0; fromRow < 8; fromRow++)
            {
                for (int fromCol = 0; fromCol < 8; fromCol++)
                {
                    // Get the piece on the current square
                    char piece = currentBoard[fromRow, fromCol];

                    // If the piece belongs to the AI player, generate all possible moves
                    if (char.IsLower(piece) == !board.IsWhiteTurn) // AI plays black
                    {
                        // Iterate over all squares on the board again
                        for (int toRow = 0; toRow < 8; toRow++)
                        {
                            for (int toCol = 0; toCol < 8; toCol++)
                            {
                                // Create the move string in standard chess notation
                                string move = $"{(char)('a' + fromCol)}{(8 - fromRow)}{(char)('a' + toCol)}{(8 - toRow)}";

                                // Add the move to the list of possible moves
                                possibleMoves.Add(move);
                            }
                        }
                    }
                }
            }

            // Return the list of all possible moves
            return possibleMoves;
        }
    }
}
