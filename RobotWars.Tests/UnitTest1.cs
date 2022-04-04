using Xunit;

namespace RobotWars.Tests
{
    public class RobotWarsGameTests
    {

        [Fact]
        public void Should_Assign_Position_Correctly()
        {
            var robotPosition = new Position(1, 2, "N");

            var robot = new Robot("", robotPosition, null, null, null);

            Assert.Equal(robot.Position.X, 1);
            Assert.Equal(robot.Position.Y, 2);
            Assert.Equal(robot.Position.Direction, "N");

        }


        [Fact]
        public void Should_Give_Correct_Position_12N()
        {
            //Arrange
            var arena = new BattleArena(5, 5);
            var robotPosition = new Position(1, 2, "N");
            var move = new MoveCommand(arena);
            var leftRotate = new LeftRotateCommand();
            var rightRotate = new RightRotateCommand();
            var robot = new Robot("", robotPosition, move, leftRotate, rightRotate);
            var game = new RobotWarsGame(robot, arena);

            //Act
            game.ExecuteTheRobot("LMLMLMLMM");

            //Assert
            Assert.Equal(robot.Position.X, 1);
            Assert.Equal(robot.Position.Y, 3);
            Assert.Equal(robot.Position.Direction, "N");
        }


        [Fact]
        public void Should_Give_Correct_Position_33E()
        {
            //Arrange
            var arena = new BattleArena(5, 5);
            var robotPosition = new Position(3, 3, "E");
            var move = new MoveCommand(arena);
            var leftRotate = new LeftRotateCommand();
            var rightRotate = new RightRotateCommand();
            var robot = new Robot("", robotPosition, move, leftRotate, rightRotate);
            var game = new RobotWarsGame(robot, arena);

            //Act
            game.ExecuteTheRobot("MMRMMRMRRM");

            //Assert
            Assert.Equal(robot.Position.X, 5);
            Assert.Equal(robot.Position.Y, 1);
            Assert.Equal(robot.Position.Direction, "E");
        }
    }
}