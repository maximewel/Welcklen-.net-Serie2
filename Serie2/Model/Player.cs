using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Sources;

namespace Serie2.Model
{
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
        public void IncScore() { score++; }

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