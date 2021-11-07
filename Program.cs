using System;
using Project.Classes;
using Project.Classes.Player;
using QuoridorConsole.Controller;

namespace QuoridorConsole {
    internal class Program {
        public static void Main(string[] args) {
            Initialize(out var parser, out var outputService, out var playerFirst, out var game);
            game.StartGame();
            while (game.GameRunning) {
                game.Tick();
            }
        }

        private static void Initialize(out InputParser parser, out OutputService outputService, out bool playerFirst, out Game game) {
            parser = new InputParser();
            outputService = new OutputService();
            playerFirst = parser.ParseColor(Console.ReadLine()?.ToLower()) == 1;
            game = Game.CreatePlayerVsBot(playerFirst);
            var player = playerFirst ? game.Players[0] : game.Players[1];
            var bot = playerFirst ? game.Players[1] : game.Players[0];
            var _parser = parser;
            var _game = game;
            game.OnNextPlayer += () => {
                if (_game.CurrentPlayer is SuperDuperUltraGiperBot) {
                    return;
                }
                
                _parser.ParseInputString(player, Console.ReadLine());
            };
            
            bot.OnPawnMoved += outputService.MoveCommand;
            bot.OnWallPlaced += outputService.SetWallCommand;
        }
    }
}