using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWars
{
    public class RobotInputLine
    {
        public string PositionLine { get; set; }
        public string CommandInstruction { get; set; }
    }
    public class BattleArena
    {
        public int XBoundry { get; private set; } = 5;
        public int YBoundry { get; private set; } = 5;

        public int XStart { get; set; } = 0;
        public int YStart { get; set; } = 0;

        public BattleArena(int xBoundry, int yBoundry)
        {
            XBoundry = xBoundry;
            YBoundry = yBoundry;
        }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Direction { get; set; } = "N";

        public Position(int x, int y, string direction)
        {
            X= x;
            Y= y;
            Direction= direction;
        }
    }

    public static class Directions
    {
        public static List<string> CompassPoints { get; set; } = new List<string>() { "N", "E", "S", "W" };
    }

    public class Robot
    {
        public string Name { get; set; }
        public Position Position { get; private set; }
        private readonly ICommand _left;
        private readonly ICommand _move;
        private readonly ICommand _right;
        public Robot(string name, Position position, MoveCommand move, LeftRotateCommand leftRotate, RightRotateCommand rightRotate)
        {
            Name = name;
            Position = position;
            _left = leftRotate;
            _move = move;
            _right = rightRotate;
        }

        public Position Move()
        {
            Position = _move.Execute(Position);
            return Position;
        }

        public Position GoLeft()
        {
            Position = _left.Execute(Position);
            return Position;
        }

        public Position GoRight()
        {
            Position = _right.Execute(Position);
            return Position;
        }
    }

    public interface ICommand
    {
        Position Execute(Position position);
    }

    public class MoveCommand : ICommand
    {
        private readonly BattleArena _arena;

        public MoveCommand(BattleArena arena)
        {
            _arena = arena;
        }
        public Position Execute(Position position)
        {
            var newPostion = new Position(position.X, position.Y, position.Direction);
            switch (position.Direction)
            {
                case "N":
                    newPostion.Y = position.Y + 1 > _arena.YBoundry ? _arena.YBoundry :  position.Y + 1;
                    break;
                case "S":
                    newPostion.Y = position.Y - 1 < _arena.YStart ? _arena.YStart : position.Y - 1;
                    break;
                case "E":
                    newPostion.X = position.X + 1 > _arena.XBoundry ? _arena.XBoundry : position.X + 1;
                    break;
                case "W":
                    newPostion.X = position.X - 1 < _arena.XStart ? _arena.XStart : position.X - 1;
                    break;
                default:
                    break;
            }
            return newPostion;
        }
    }

    public class LeftRotateCommand : ICommand
    {

        public Position Execute(Position position)
        {
            var newPostion = new Position(position.X, position.Y, position.Direction);
         
            var index = Directions.CompassPoints.FindIndex(x => x == position.Direction);
            if (index == 0) newPostion.Direction = Directions.CompassPoints.Last();
            else newPostion.Direction = Directions.CompassPoints[index - 1];

            return newPostion;
        }
    }

    public class RightRotateCommand : ICommand
    {

        public Position Execute(Position position)
        {
            var newPostion = new Position(position.X, position.Y, position.Direction);

            var index = Directions.CompassPoints.FindIndex(x => x == position.Direction);
            if (index == Directions.CompassPoints.Count()-1) newPostion.Direction = Directions.CompassPoints.First();
            else newPostion.Direction = Directions.CompassPoints[index + 1];

            return newPostion;
        }
    }

    public class RobotWarsGame
    {
        private Robot _robot;
        private readonly BattleArena _arena;
        public RobotWarsGame(Robot robot, BattleArena arena)
        {
            _robot = robot;
            _arena = arena;
        }

        public Robot ExecuteTheRobot(string command)
        {
            foreach(char c in command)
            {
                switch(c)
                {
                    case 'M':
                        _robot.Move();
                        break;
                    case 'L':
                        _robot.GoLeft();
                        break;
                    case 'R':
                        _robot.GoRight();
                        break;
                    default:
                        break;
                }
            }
            return _robot;
        }
    }

}
