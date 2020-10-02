using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Sources;

namespace Serie2.Model
{
    /// <summary>
    /// Model class. <br></br>
    /// Stock data for a player
    /// </summary>
    class Player
    {

        private char symbol;
        public char Symbol { get { return symbol; } set { symbol = value; } }

        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private int score;
        public int Score { get { return score; } set { score = value; } }
        /// <summary>
        /// Increment the player score by 1 point
        /// </summary>
        public void IncScore() { score++; }

        /// <summary>
        /// Create a player
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <param name="symbol">Symbol of the player (char)</param>
        public Player(string name, char symbol)
        {
            this.name = name;
            this.symbol = symbol;
            this.score = 0;
        }

        public override string ToString()
        {
            return $"<{name}:{symbol}>";
        }
    }
}