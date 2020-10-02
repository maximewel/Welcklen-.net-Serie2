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
    class ControlerTickTackToe
    {
        private BoardView boardView;
        private Board board;
        private Player player1, player2, currentPlayer;
        private bool isPlayer1Starting;

        enum State { start, player1, player2, game, end  }
        private State state = State.start;
        public ControlerTickTackToe()
        {
            Console.WriteLine("Welcome to Tick Tack Toe ! Press any key to enter");
            boardView = new BoardView();
            board = new Board();
        }

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

        private void Player1(string input)
        {
            try
            {
                player1 = ParsePlayer(input);
                boardView.Display("Player 2, enter your identifier <name:symbol>");
                state = State.player2;
            } catch
            {
                boardView.Display("Player 1, enter your identifier again <name:symbol>");
                return;
            }
        }

        private void Player2(string input)
        {

            try
            {
                player2 = ParsePlayer(input);
            }
            catch
            {
                boardView.Display("Player 2, enter your identifier again <name:symbol>");
                return;
            }
            if (player1.Symbol == player2.Symbol)
            {
                boardView.Display("You can't take the same symbols ! Enter player2 again as <name:symbol>");
            }
            else
            {
                isPlayer1Starting = true;
                currentPlayer = player1;
                boardView.Display("Let's play !");
                StartGame();
            }
        }

        private void ParseMove(string input)
        {
            string[] parsed = input.Trim('<', '>').Split(" ");
            if (parsed.Length > 3)
            {
                boardView.Display("Too many instructions in your move :-(");
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
                int x = int.Parse(parsed[1]), y = int.Parse(parsed[2]);
                if (board.Play(x,y, currentPlayer))
                {
                    currentPlayer = (currentPlayer == player1 ? player2 : player1);
                    boardView.DisplayBoard(board);
                    if (board.isFinished()) {
                        board.Winner()?.IncScore();
                        boardView.DisplayScore(player1, player2);
                        Console.WriteLine("Press [Y] to play again, [R] to change player, [stop] to end");
                        state = State.end;
                    }
                    else
                    {
                        boardView.Display($"turn : {currentPlayer}");
                    }
                }
                else
                {
                    boardView.Display($"You cannot do this move !");
                    return;
                }
            } catch
            {
                boardView.Display($"Impossible to read your coordinates");
                return;
            }
        }

        private void StartGame()
        {
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
                    currentPlayer = isPlayer1Starting ? player1 : player2;
                    StartGame();
                    break;
                case "r":
                    boardView.Display("Entering new game, press any key to continue");
                    state = State.start;
                    break;
                default:
                    break;
            }
        }

        public Player ParsePlayer(String identifier)
        {
            string[] parsed = identifier.Trim('<','>').Split(":");
            if(parsed.Length != 2)
            {
                throw new Exception("Could not parse");
            }

            string symbolString = parsed[1];
            char symbol = symbolString[0];
            if (symbolString.Length > 1) {
                boardView.Display($"symbole too long, truncated to {symbol}");
            }
            return new Player(parsed[0], parsed[1][0]);
        }
    }
}
