using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Classes;
using Project.Classes.Field;
using QuoridorConsole.Controller;

namespace TestInputParser
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMoveCommand()
        {
            Dictionary<string, int[]> mapCommandsToCoords = new Dictionary<string, int[]>
            {
                {"move E2", new []{2, 8}},
                {"move F2", new []{2, 10}},
                {"move F3", new []{4, 10}},
                {"move F4", new []{6, 10}},
                {"move G4", new []{6, 12}},
                {"move H4", new []{6, 14}},

            };
            
            var game = Game.CreatePlayerVsBot();
            var testPlayer = game.Players[0];
            var inputParser = new InputParser();
            
            game.StartGame();

            foreach (KeyValuePair<string, int[]> entry in mapCommandsToCoords)
            {
                while (!testPlayer.myTurn)
                {
                    game.Tick();
                    await Task.Yield();
                }
                var action = inputParser.ParseInputString(testPlayer, entry.Key);
                action();
                
                await Task.Delay(500);
                
                int[] testPlayerPos = new[] {testPlayer.Pawn.Y, testPlayer.Pawn.X};
                
                Assert.AreEqual(entry.Value[0], testPlayerPos[0]);
                Assert.AreEqual(entry.Value[1], testPlayerPos[1]);
            }
        }
        
        [TestMethod]
        public async Task TestWallCommand()
        {
            Dictionary<string, int[]> mapCommandsToCoords = new Dictionary<string, int[]>
            {
                {"wall S1h", new []{1, 1}},
                {"wall S2h", new []{3, 1}},
                {"wall S3v", new []{5, 1}},
                {"wall T3v", new []{5, 3}},
            };
            
            var game = Game.CreatePlayerVsBot();
            var testPlayer = game.Players[0];
            var inputParser = new InputParser();
            
            game.StartGame();

            foreach (KeyValuePair<string, int[]> entry in mapCommandsToCoords)
            {
                while (!testPlayer.myTurn)
                {
                    game.Tick();
                    await Task.Yield();
                }
                
                var action = inputParser.ParseInputString(testPlayer, entry.Key);
                action();
                await Task.Delay(500);

                Assert.AreEqual(FieldSpace.BlockType.Wall, game.Field.FieldSpaces[entry.Value[0], entry.Value[1]].Type);
            }
        }
    }
}