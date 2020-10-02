using Serie2.controler;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Serie2.Model
{
    class Board : ITerminable {
        private const int BOARD_DIM = 3;

        //Attributes
        private Player[,] board = new Player[BOARD_DIM, BOARD_DIM];
        public Player this[int x, int y]
        {
            get { return board[x,y]; }
        }

        public void Clear()
        {
            Array.Clear(board, 0, board.Length);
        }

        public bool isFinished()
        {
            return isWon() || isFull();
        }

        public bool isFull()
        {
            foreach(Player player in board)
            {
                if (player == null)
                {
                    return false;
                }
            }
            return true;
        }

        public bool isWon()
        {
            return Winner() != null;
        }

        public Player Winner()
        {
            Player diagPlayer = board[1, 1];
            bool diagLeft, diagRight;
            diagLeft = diagRight = (diagPlayer != null);
            for(int i=0; i<BOARD_DIM; i++)
            {
                // check horiz lines
                if (board[i, 0] != null && (board[i,0] == board[i,1] && board[i, 1] == board[i,2])){
                    return board[i, 0];
                }
                // check verti lines
                if (board[0, i] != null && (board[0, i] == board[1, i] && board[1, i] == board[2, i]))
                {
                    return board[0, i];
                }
                //diag
                //verify left
                if (diagLeft && (board[i,i] != diagPlayer))
                {
                    diagLeft = false;
                }
                //verify right
                if(diagRight && (board[BOARD_DIM-1-i, i] != diagPlayer))
                {
                    diagRight = false;
                }
            }
            if(diagLeft || diagRight)
            {
                return diagPlayer;
            }
            return null;

        }

        public bool Play(int x, int y, Player player)
        {
            if(board[x,y] != null)
            {
                return false;
            }
            board[x, y] = player;
            return true;
        }
    }
}
