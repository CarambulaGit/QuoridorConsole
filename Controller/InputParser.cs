using System;
using System.Collections.Generic;
using Project.Classes;
using Project.Classes.Field;
using Project.Classes.Player;

namespace QuoridorConsole.Controller
{
    public class InputParser
    {
        private Dictionary<char, int> coordinates = new Dictionary<char, int>
        {
            { 'A', 0 },
            { 'B', 2 },
            { 'C', 4 },
            { 'D', 6 },
            { 'E', 8 },
            { 'F', 10 },
            { 'G', 12 },
            { 'H', 14 },
            { 'I', 16 },
            { 'S', 1 },
            { 'T', 3 },
            { 'U', 5 },
            { 'V', 7 },
            { 'W', 9 },
            { 'X', 11 },
            { 'Y', 13 },
            { 'Z', 15 },
        };

        private Dictionary<char, Wall.Type> wallTypes = new Dictionary<char, Wall.Type>
        {
            { 'v', Wall.Type.Vertical },
            { 'h', Wall.Type.Horizontal }
        };

        private Wall GetWallFromString(string str)
        {
            var yInt = int.Parse(str[1].ToString());

            var x = coordinates[str[0]];
            var y = 2 * (yInt - 1) + 1;
            var wallType = wallTypes[str[2]];

            return new Wall(y, x, wallType);
        }

        private Point GetPawnPointFromString(string str)
        {
            var yInt = int.Parse(str[1].ToString());

            var x = coordinates[str[0]];
            var y = 2 * (yInt - 1);

            return new Point(y, x);
        }

        public Action ParseInputString(Player player, string str)
        {
            string[] strArr = str.Split(' ');
            string command = strArr[0];
            string strCoordinates = strArr[1];

            if (command == Consts.SET_WALL_COMMAND)
            {
                return () => player.TrySetWall(GetWallFromString(strCoordinates));
            }
            else if (command is Consts.MOVE_COMMAND or Consts.JUMP_COMMAND)
            {
                return () => player.TryMovePawn(GetPawnPointFromString(strCoordinates));
            }
            else
            {
                throw new System.ArgumentException();
            }
        }
    }
}
