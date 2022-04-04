// See https://aka.ms/new-console-template for more information
using RobotWars;

Console.WriteLine("First line is arena boundry, second line robot A position, third line robot A instructions, fourth and fifth lines similar to robot B");
var arenaBoundryLine = Console.ReadLine();


var robotInputList = new List<RobotInputLine>();
var currentPositionLine = Console.ReadLine();
var robotCommand = Console.ReadLine();
robotInputList.Add(new RobotInputLine() { PositionLine = currentPositionLine, CommandInstruction = robotCommand });
currentPositionLine = Console.ReadLine();
robotCommand = Console.ReadLine();
robotInputList.Add(new RobotInputLine() { PositionLine = currentPositionLine, CommandInstruction = robotCommand });

var arenaBoundry = arenaBoundryLine?.Split(' ').Select(int.Parse).ToList();
var arena = new BattleArena(arenaBoundry[0], arenaBoundry[1]);

foreach(var line in robotInputList)
{
    var currentPosition = line.PositionLine?.Split(' ');
    var robotPosition = new Position(int.Parse(currentPosition[0]), int.Parse(currentPosition[1]), currentPosition[2]);

    var move = new MoveCommand(arena);
    var leftRotate = new LeftRotateCommand();
    var rightRotate = new RightRotateCommand();

    var robot = new Robot("", robotPosition, move, leftRotate, rightRotate);

    var game = new RobotWarsGame(robot, arena);
    game.ExecuteTheRobot(line.CommandInstruction);
    Console.WriteLine(robot.Position.X.ToString() + " " + robot.Position.Y.ToString() + " " + robot.Position.Direction);
}
