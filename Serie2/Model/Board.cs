using Serie2.controler;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Serie2.Model
{
    /// <summary>
    /// Model class. <br></br>
    /// Stock data for the tick tack toe board <br></br>
    /// Light controller paradigm : Some of the work is done directly in the model (ITerminable)
    /// </summary>
    class Board : ITerminable {
        //Attributes
        private const int BOARD_DIM = 3;
        private Player[,] board = new Player[BOARD_DIM, BOARD_DIM];
        public Player this[int x, int y]
        {
            get { return board[x,y]; } //return player at index [x,y]
        }

        /// <summary>
        /// clear the data on the board
        /// </summary>
        public void Clear()
        {
            Array.Clear(board, 0, board.Length);
        }

        public bool IsFinished()
        {
            return IsWon() || IsFull();
        }

        /// <summary>
        /// Search the board to know wether it is full
        /// </summary>
        /// <returns>True if no empty cases are left</returns>
        public bool IsFull()
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

        /// <summary>
        /// return wether the game is won
        /// </summary>
        /// <returns>True if the game is one by a player</returns>
        public bool IsWon()
        {
            return Winner() != null;
        }

        /// <summary>
        /// Return the player who won the game
        /// </summary>
        /// <returns>The player that has won the current board, or null if no one has won</returns>
        public Player Winner()
        {
            //init diag checking (diag cross the center of the board)
            Player diagPlayer = board[1, 1];
            bool diagLeft, diagRight;
            diagLeft = diagRight = (diagPlayer != null);
            //a bit of automation
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

        /// <summary>
        /// Play the move at given coordinate, from given player
        /// </summary>
        /// <param name="x">the X coordinate (line)</param>
        /// <param name="y">the Y coordinate (column)</param>
        /// <param name="player"></param>
        /// <returns>wether the move has been registered (is legal)</returns>
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
