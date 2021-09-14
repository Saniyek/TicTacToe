using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Game
{
    public class Program
    {
        public static int[,] winnerSquence = new int[8, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
        public static int[,] corners = new int[4, 4] { { 0, 2, 8, 6 }, { 2, 8, 6, 0 }, { 8, 6, 0, 2 }, { 6, 0, 2, 8 } };
        public static int[] winnerLine;


        static void Main(string[] args)
        {
            do
            {
                PlayGame();
            } while (CheckNewGame());
        }

        public static void PlayGame()
        {
            int gameStatus = 0;
            int currentPlayer = -1;
            List<char> gameMarkers = new List<char>() { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
            int howManyPlayers = 2; // Default to 2

            // Get how many players are palying. If 1 then second player will be computer.
            howManyPlayers = CheckPlayers();

            do
            {
                Console.Clear();

                currentPlayer = GetNextPlayer(currentPlayer);

                HeadsUpDisplay(currentPlayer);
                DrawGameboard(gameMarkers);

                GameEngine(gameMarkers, currentPlayer);

                // gameStatus = 0 =>  no winner, no draw
                gameStatus = CheckWinner(gameMarkers);

                // If one player then computers turn
                if (gameStatus.Equals(0) && howManyPlayers == 1)
                {
                    currentPlayer = GetNextPlayer(currentPlayer);
                    ComputerMakesMove(gameMarkers);
                    gameStatus = CheckWinner(gameMarkers);
                }

            } while (gameStatus.Equals(0));


            Console.Clear();
            HeadsUpDisplay(currentPlayer);
            DrawGameboard(gameMarkers);


            if (gameStatus.Equals(1))
            {
                Console.WriteLine($"Player {currentPlayer} is the winner!");
            }

            if (gameStatus.Equals(2))
            {
                Console.WriteLine($"The game is a draw!");
            }
            Console.ReadLine();
        }

        private static int CheckPlayers()
        {
            int count;
            Console.WriteLine("How many players? [1/2] ");
            do
            {
                string userInput = Console.ReadLine();

                int.TryParse(userInput, out count);

            } while (count != 1 && count != 2);

            return count;
        }

        private static bool CheckNewGame()
        {
            ConsoleKey response;
            do
            {
                Console.Write("Would you like new game? [y/n] ");
                response = Console.ReadKey(false).Key;
                if (response != ConsoleKey.Enter)
                    Console.WriteLine();

            } while (response != ConsoleKey.Y && response != ConsoleKey.N);
            return response == ConsoleKey.Y;
        }

        public static void ComputerMakesMove(List<char> gameMarkers)
        {
            // Priority 1 : get tic tac toe
            // Priority 2 : block x
            // Priority 3: select  corner space
            // Priority 4: pick open space

            int idx;
            idx = LookForWinOrBlock(gameMarkers, 'O');
            if (idx == -1)
            {
                idx = LookForWinOrBlock(gameMarkers, 'X');
                if (idx == -1)
                {
                    idx = LookForCorner(gameMarkers);
                    if (idx == -1)
                    {
                        idx = LookForOpenSpace(gameMarkers);
                    }
                }
            }
            gameMarkers[idx] = 'O';

        }

        private static int LookForOpenSpace(List<char> gameMarkers)
        {
            return gameMarkers.FindIndex(x => x == ' ');
        }

        private static int LookForCorner(List<char> gameMarkers)
        {


            for (int i = 0; i < corners.GetLength(0); i++)
            {

                if (gameMarkers[corners[i, 0]].Equals(' ')) return corners[i, 0];
                else if (gameMarkers[corners[i, 1]].Equals(' ')) return corners[i, 1];
                else if (gameMarkers[corners[i, 2]].Equals(' ')) return corners[i, 2];
                else if (gameMarkers[corners[i, 3]].Equals(' ')) return corners[i, 3];

            }

            return -1;

        }

        //If there are 2 same mark and one space on one winner line then blok or select it
        private static int LookForWinOrBlock(List<char> gameMarkers, char mark)
        {
            List<char> newList;

            for (int i = 0; i < winnerSquence.GetLength(0); i++)
            {

                newList = new List<char>() { gameMarkers[winnerSquence[i, 0]], gameMarkers[winnerSquence[i, 1]], gameMarkers[winnerSquence[i, 2]] };

                if (newList.FindAll(x => x == mark).Count == 2 && newList.Contains(' '))
                    return winnerSquence[i, newList.FindIndex(x => x == ' ')];
            }


            return -1;
        }

        public static int CheckWinner(List<char> gameMarkers)
        {
            // If we have a winner, announce who won 
            if (IsGameWinner(gameMarkers))
            {
                return 1;
            }

            // If all markers are placed and no winner then it's a draw stop the game
            if (IsGameDraw(gameMarkers))
            {
                return 2;
            }

            return 0;
        }

        private static bool IsGameDraw(List<char> gameMarkers)
        {

            return !gameMarkers.Contains(' ');

        }

        private static bool IsGameWinner(List<char> gameMarkers)
        {

            for (int i = 0; i < winnerSquence.GetLength(0); i++)
            {
                if (IsGameMarkersTheSame(gameMarkers, winnerSquence[i, 0], winnerSquence[i, 1], winnerSquence[i, 2]))
                {
                    winnerLine = new int[] { winnerSquence[i, 0], winnerSquence[i, 1], winnerSquence[i, 2] };
                    return true;
                }
            }

            return false;
        }


        private static bool IsGameMarkersTheSame(List<char> testGameMarkers, int pos1, int pos2, int pos3)
        {
            // Check all 
            return (testGameMarkers[pos1] != ' '  // Check if there is empty line
                && testGameMarkers[pos1].Equals(testGameMarkers[pos2])
                && testGameMarkers[pos2].Equals(testGameMarkers[pos3]));
        }

        private static void GameEngine(List<char> gameMarkers, int currentPlayer)
        {
            bool notValidMove = true;
            do
            {
                // As the user places markers on the game, update the board then notify which player has a turn
                string userInput = Console.ReadLine().ToUpper();

                if (userInput.Equals("A1") || userInput.Equals("A2") || userInput.Equals("A3") ||
                    userInput.Equals("B1") || userInput.Equals("B2") || userInput.Equals("B3") ||
                    userInput.Equals("C1") || userInput.Equals("C2") || userInput.Equals("C3"))

                //int.TryParse(userInput, out var gamePlacementMarker);

                // if (gamePlac ementMarker >= 1  && gamePlacementMarker <= 9)               
                {
                    char[] userInputArr = userInput.ToCharArray();

                    int gamePlacementMarker = (((int)char.ToUpper(userInputArr[0])) - 65) * 3 + int.Parse(userInputArr[1].ToString());
                    //char currentMarker;

                    //currentMarker = gameMarkers[gamePlacementMarker - 1];

                    if (!gameMarkers[gamePlacementMarker - 1].Equals(' '))
                    {
                        Console.WriteLine("Placement has already a marker please select anotyher placement.");
                    }
                    else
                    {
                        gameMarkers[gamePlacementMarker - 1] = GetPlayerMarker(currentPlayer);
                        notValidMove = false;
                    }

                }
                else
                {
                    Console.WriteLine("Invalid value please select anotyher placement.");
                }

            } while (notValidMove);
        }

        private static char GetPlayerMarker(int player)
        {
            if (player % 2 == 0)
            {
                return 'O';
            }

            return 'X';
        }

        static void HeadsUpDisplay(int PlayerNumber)
        {
            // Provide instructions
            // A greeting
            Console.WriteLine("Welcome to the Super Duper Tic Tac Toe Game!");
            // Console.WriteLine("To exit please press Esc");

            // Display player sign, Player 1 is X and Player 2 is O
            Console.WriteLine("Player 1: X");
            Console.WriteLine("Player 2: O");
            Console.WriteLine();

            // Who's turn is it?
            // Instruct the user to enter a number between 1 and 9
            Console.WriteLine($"Player {PlayerNumber} to move");
            Console.WriteLine("Make your selection according to vertical and horizontal numbers.");
            Console.WriteLine();
        }


        static void DrawGameboard(List<char> gameMarkers)
        {
            Console.WriteLine("     1 | 2 | 3 ");
            Console.WriteLine("   ------------");
            Console.WriteLine($" A | {(gameMarkers[0].Equals(' ') ? '-' : gameMarkers[0]) } " +
                $"| {(gameMarkers[1].Equals(' ') ? '-' : gameMarkers[1])} " +
                $"| {(gameMarkers[2].Equals(' ') ? '-' : gameMarkers[2])} ");
            Console.WriteLine("   ---+---+---");
            Console.WriteLine($" B | {(gameMarkers[3].Equals(' ') ? '-' : gameMarkers[3]) } " +
                $"| {(gameMarkers[4].Equals(' ') ? '-' : gameMarkers[4])} " +
                $"| {(gameMarkers[5].Equals(' ') ? '-' : gameMarkers[5])} ");
            // Console.WriteLine($" {gameMarkers[3]} | {gameMarkers[4]} | {gameMarkers[5]} ");
            Console.WriteLine("   ---+---+---");
            Console.WriteLine($" C | {(gameMarkers[6].Equals(' ') ? '-' : gameMarkers[6]) } " +
                $"| {(gameMarkers[7].Equals(' ') ? '-' : gameMarkers[7])} " +
                $"| {(gameMarkers[8].Equals(' ') ? '-' : gameMarkers[8])} ");
            // Console.WriteLine($" {gameMarkers[6]} | {gameMarkers[7]} | {gameMarkers[8]} ");
        }

        static int GetNextPlayer(int player)
        {
            if (player.Equals(1))
            {
                return 2;
            }

            return 1;
        }
    }
}

