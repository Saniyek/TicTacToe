using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Game;


namespace TicTacToe.UnitTest
{
    [TestClass]
    public class TicTacToeWinnerTest
    {
        public static int[,] winnerSquence = new int[8, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
        public static int[,] corners = new int[4, 4] { { 0, 2, 8, 6 }, { 2, 8, 6, 0 }, { 8, 6, 0, 2 }, { 6, 0, 2, 8 } };
        public static int[] winnerLine;

        [TestMethod]
        public void CheckWinner_Test()
        {
            List<char> actual = new List<char>() { 'X', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
            List<char> expected = new List<char>() { 'X', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
            int gameStatus = 1;

            gameStatus = TicTacToe.Game.Program.CheckWinner(actual);

           // Assert.IsTrue(expected.SequenceEqual(actual));
          
            Assert.AreEqual(gameStatus, 0);
        }
    }
}
