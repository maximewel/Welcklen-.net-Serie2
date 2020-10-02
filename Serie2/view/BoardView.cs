using Serie2.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serie2.view
{
    /// <summary>
    /// View class - Display informations from Board and Player models
    /// </summary>
    class BoardView
    {
        /// <summary>
        /// Display a board, human formatted, on standard input
        /// </summary>
        /// <param name="board">Board to display</param>
        public void DisplayBoard(Board board)
        {
            Console.WriteLine($"| {board[0,0]?.Symbol ?? ' '} | {board[0, 1]?.Symbol ?? ' '} | {board[0, 2]?.Symbol ?? ' '} |");
            Console.WriteLine($"|---+---+---|");
            Console.WriteLine($"| {board[1, 0]?.Symbol ?? ' '} | {board[1, 1]?.Symbol ?? ' '} | {board[1, 2]?.Symbol ?? ' '} |" );
            Console.WriteLine($"|---+---+---|");
            Console.WriteLine($"| {board[2, 0]?.Symbol ?? ' '} | {board[2, 1]?.Symbol ?? ' '} | {board[2, 2]?.Symbol ?? ' '} |");
        }

        /// <summary>
        /// Display a little phrase detailing the score of each player, and who is winning
        /// </summary>
        /// <param name="p1">First player</param>
        /// <param name="p2">Second player</param>
        public void DisplayScore(Player p1, Player p2) {
            Player winning, loosing;
            if (p1.Score > p2.Score)
            {
                winning = p1; loosing = p2;
            } else if (p1.Score < p2.Score)
            {
                winning = p2; loosing = p1;
            }
            else
            {
                Console.WriteLine($"It's a tie between {p1.Name} and {p2.Name} ! Score : {p1.Score}");
                return;
            }
            Console.WriteLine($"{winning.Name} is winning with a score of {winning.Score} against {loosing.Name} and his pathetic {loosing.Score}");
        }

        /// <summary>
        /// Display a simple query on the view
        /// </summary>
        /// <param name="query">the query to display</param>
        public void Display(String query)
        {
            Console.WriteLine(query);
        }
    }
}
