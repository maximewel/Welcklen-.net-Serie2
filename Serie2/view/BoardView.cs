using Serie2.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serie2.view
{
    class BoardView
    {
        public void DisplayBoard(Board board)
        {
            Console.WriteLine($"| {board[0,0]?.Symbol ?? ' '} | {board[0, 1]?.Symbol ?? ' '} | {board[0, 2]?.Symbol ?? ' '} |");
            Console.WriteLine($"|---+---+---|");
            Console.WriteLine($"| {board[1, 0]?.Symbol ?? ' '} | {board[1, 1]?.Symbol ?? ' '} | {board[1, 2]?.Symbol ?? ' '} |" );
            Console.WriteLine($"|---+---+---|");
            Console.WriteLine($"| {board[2, 0]?.Symbol ?? ' '} | {board[2, 1]?.Symbol ?? ' '} | {board[2, 2]?.Symbol ?? ' '} |");
        }

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

        public void Display(String query)
        {
            Console.WriteLine(query);
        }
    }
}
