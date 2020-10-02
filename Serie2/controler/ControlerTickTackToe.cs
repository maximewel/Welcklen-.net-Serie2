using Serie2.Model;
using Serie2.view;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Serie2.controler
{
    /// <summary>
    /// Controller class.<br></br>
    /// Talk with a board and a boardview<br></br>
    /// Parse and control user input before communicating with the board and display on the view
    /// </summary>
    class ControlerTickTackToe
    {
        //attributes
        //model and view
        private BoardView boardView;
        private Board board;
        private Player player1, player2;
        //game control
        private Player currentPlayer;
        private bool isPlayer1Starting;
        //state machine
        enum State { start, player1, player2, game, end  }
        private State state = State.start;
        public ControlerTickTackToe()
        {
            Console.WriteLine("Welcome to Tick Tack Toe ! Press any key to enter");
            boardView = new BoardView();
            board = new Board();
        }

        /// <summary>
        /// Parse input from user in the big state machine
        /// </summary>
        /// <param name="input">string input from user</param>
        public void ParseInput(String input)
        {
            switch (state)
            {
                case State.start:
                    boardView.Display("Player 1, enter your identifier <name:symbol>");
                    state = State.player1;
                    break;
                case State.player1:
                    Player1(input);
                    break;
                case State.player2:
                    Player2(input);
                    break;
                case State.game:
                    ParseMove(input);
                    break;
                case State.end:
                    End(input);
                    break;
            }
        }

        /// <summary>
        /// "Player1" state <br></br>
        /// Create first player
        /// </summary>
        /// <param name="input">string input from user, expect name:symbol</param>
        private void Player1(string input)
        {
            try
            {
                player1 = ParsePlayer(input); //can throw if not parsed
                boardView.Display("Player 2, enter your identifier <name:symbol>");
                state = State.player2;
            } catch
            {
                boardView.Display("Player 1, enter your identifier again <name:symbol>");
                return;
            }
        }

        /// <summary>
        /// "Player2" state <br></br>
        /// Create second player
        /// </summary>
        /// <param name="input">string input from user, expect name:symbol</param>
        private void Player2(string input)
        {

            try
            {
                player2 = ParsePlayer(input); //can throw if could not parse
                //verify that the players are distinguishable
                if (player1.Symbol == player2.Symbol)
                {
                    boardView.Display("You can't take the same symbols ! Enter player2 again as <name:symbol>");
                }
                else
                {
                    isPlayer1Starting = true; //player 1 always starts the first game of the match
                    boardView.Display("Let's play !");
                    StartGame();
                }
            }
            catch
            {
                boardView.Display("Player 2, enter your identifier again <name:symbol>");
            }
        }

        /// <summary>
        /// parse a move order from user
        /// </summary>
        /// <param name="input">string input as symbol x y</param>
        private void ParseMove(string input)
        {
            //separate string, perform integrity test on resulting partial strings
            string[] parsed = input.Trim('<', '>').Split(" ");
            if (parsed.Length != 3)
            {
                boardView.Display("Please enter your move as <symbole x y> please !");
                return;
            }
            string symb = parsed[0];
            if (parsed[0].Length > 1 || symb[0] != currentPlayer.Symbol)
            {
                boardView.Display($"The symbol is incorrect ! Expected : {currentPlayer.Symbol}, you proposed {symb}");
                return;
            }

            try
            {
                int x = int.Parse(parsed[1]), y = int.Parse(parsed[2]); //can fail parsing here
                if (board.Play(x,y, currentPlayer))
                {
                    //move OK
                    boardView.DisplayBoard(board);
                    if (board.IsFinished()) {
                        //end game
                        board.Winner()?.IncScore();
                        boardView.DisplayScore(player1, player2);
                        Console.WriteLine("Press [Y] to play again, [R] to change player, [stop] to end");
                        state = State.end;
                    }
                    else
                    {
                        //switch player
                        currentPlayer = (currentPlayer == player1 ? player2 : player1);
                        boardView.Display($"turn : {currentPlayer}");
                    }
                }
                else
                {
                    //move failed
                    boardView.Display($"You cannot do this move !");
                    return;
                }
            } catch
            {
                boardView.Display($"Impossible to read your coordinates");
                return;
            }
        }

        /// <summary>
        /// General game starting routine
        /// </summary>
        private void StartGame()
        {
            currentPlayer = isPlayer1Starting ? player1 : player2;
            boardView.DisplayBoard(board);
            boardView.Display("To play, enter your next move as : <symbole x y>");
            boardView.Display($"{currentPlayer} starts");
            state = State.game;
        }

        private void End(string input)
        {
            board.Clear();
            switch (input.Trim().ToLower())
            {
                case "y" :
                    //alternate starting players each game
                    isPlayer1Starting = !isPlayer1Starting;
                    StartGame();
                    break;
                case "r":
                    //restart all routine
                    boardView.Display("Entering new game, press any key to continue");
                    state = State.start;
                    break;
                default:
                    boardView.Display($"Didnt understand {input}, please try again");
                    break;
            }
        }

        /// <summary>
        /// Parse a player string identifier into a player object, must be in form name:symbole <br></br>
        /// Truncate symboles to 1 char if too long
        /// </summary>
        /// <param name="identifier">the identifier string as name:symbole to parse</param>
        /// <returns>The player created from the string</returns>
        public Player ParsePlayer(String identifier)
        {
            //verify input
            string[] parsed = identifier.Trim('<','>').Split(":");
            if(parsed.Length != 2)
            {
                throw new Exception("Could not parse");
            }

            //verify/truncate symbole
            string symbolString = parsed[1];
            char symbol = symbolString[0];
            if (symbolString.Length > 1) {
                boardView.Display($"symbole too long, truncated to {symbol}");
            }

            //create player
            return new Player(parsed[0], parsed[1][0]);
        }
    }
}
