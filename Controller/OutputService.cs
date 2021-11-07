using System;
using System.Collections.Generic;
using Project.Classes.Field;

namespace QuoridorConsole.Controller {
    public class OutputService {
        private readonly string moveCommand = "move";
        private readonly string jumpCommand = "jump";
        private readonly string placeWallCommand = "wall";

        private Dictionary<int, char> coordinates = new Dictionary<int, char> {
            {0, 'A'},
            {2, 'B'},
            {4, 'C'},
            {6, 'D'},
            {8, 'E'},
            {10, 'F'},
            {12, 'G'},
            {14, 'H'},
            {16, 'I'},
            {1, 'S'},
            {3, 'T'},
            {5, 'U'},
            {7, 'V'},
            {9, 'W'},
            {11, 'X'},
            {13, 'Y'},
            {15, 'Z'},
        };

        private Dictionary<Wall.Type, char> wallTypes = new Dictionary<Wall.Type, char> {
            {Wall.Type.Vertical, 'v'},
            {Wall.Type.Horizontal, 'h'}
        };

        public void MoveCommand(Point startPoint, Point endPoint) {
            var moveVector = startPoint - endPoint;
            var stringCoordinate = GetStringCoordinateFromPoint(endPoint);
            Console.WriteLine(Math.Abs(moveVector.X) + Math.Abs(moveVector.Y) == 2
                ? $"{moveCommand} {stringCoordinate}"
                : $"{jumpCommand} {stringCoordinate}");
        }

        public void SetWallCommand(Wall wall) {
            var stringCoordinate = GetStringCoordinateFromPoint(wall.Pos);
            Console.WriteLine($"{placeWallCommand} {stringCoordinate}{wallTypes[wall.WallType]}");
        }
        
        private string GetStringCoordinateFromPoint(Point point) {
            return $"{coordinates[point.X]}{point.Y / 2 + 1}";
        }
    }
}