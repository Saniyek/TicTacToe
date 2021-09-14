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
       
        [TestMethod]
        public void ComputerMakesMove_Test()
        {
            List<char> actual = new List<char>() {
                    'X', ' ', 'O',
                    ' ', 'X', ' ',
                    ' ', ' ', ' ' };
            List<char> expected = new List<char>() {
                    'X', ' ', 'O',
                    ' ', 'X', ' ',
                    ' ', ' ', 'O' };

            TicTacToe.Game.Program.ComputerMakesMove(actual);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

             

        
        [DataRow(0, new char[] { 'X', ' ', ' ', ' ', 'O', 'X', ' ', ' ', ' ' })] //the game continues
        [DataRow(1, new char[] { 'X', 'X', 'X', 'O', 'X', 'O', 'X', 'O', 'X' })] // winner
        [DataRow(2, new char[] { 'O', 'X', 'O', 
                                 'O', 'X', 'O', 
                                 'X', 'O', 'X' })] // draw
        [DataTestMethod]
        public void CheckWinner_GameStatus_Test(int gameStatusExpected, char[] actualList)
        {
            int gameStatus;
           
            gameStatus = TicTacToe.Game.Program.CheckWinner(actualList.ToList());

            Assert.AreEqual(gameStatus, gameStatusExpected);
        }
    }
}
