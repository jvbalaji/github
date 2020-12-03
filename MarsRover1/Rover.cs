using System;

namespace MarsRover
{
    /// <summary>
    /// Class allows to initialise a Rover and move it around a Plateau
    /// </summary>
    public class Rover
    {
        private int X { get; set; }
        private int Y { get; set; }
        private string Direction { get; set; }

        private Plateau _plateau;

        public Rover(Plateau p)
        {
            _plateau = p;

            X = 0;
            Y = 0;
            Direction = "N";
        }

        // initialise rover co-ordinates
        public void SetPosition(int x, int y, string direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        // returns current co-ordinates of rover
        public Tuple<int, int, string> CurrentPosition()
        {
            return Tuple.Create(X, Y, Direction);
        }

        // method to move a rover in a certain direction across a number of positions
        public bool MoveRover(char direction, int pos)
        {
            RotateRover(direction);

            switch (Direction)
            {
                case "N":
                    Y += pos;
                    break;
                case "W":
                    X -= pos;
                    break;
                case "S":
                    Y -= pos;
                    break;
                case "E":
                    X += pos;
                    break;
            }

            return IsRoverInPlateau();
        }

        // method to rotate a rover left or right
        private void RotateRover(char direction)
        {
            if (direction == 'L')
            {
                TurnLeft();
            }
            else if (direction == 'R')
            {
                TurnRight();
            }
            else
            {
                Console.WriteLine("Invalid Rover command.");
            }
        }

        // check if rover is inside a plateau
        private bool IsRoverInPlateau()
        {
            return X >= 0 &&
                   X < _plateau.PlateauWidth &&
                   Y >= 0 &&
                   Y < _plateau.PlateauHeight;
        }

        // method to rotate a rover left 
        private void TurnLeft()
        {
            switch (Direction)
            {
                case "N":
                    Direction = "W";
                    break;
                case "W":
                    Direction = "S";
                    break;
                case "S":
                    Direction = "E";
                    break;
                case "E":
                    Direction = "N";
                    break;
            }
        }

        // method to rotate a rover right
        private void TurnRight()
        {
            switch (Direction)
            {
                case "N":
                    Direction = "E";
                    break;
                case "E":
                    Direction = "S";
                    break;
                case "S":
                    Direction = "W";
                    break;
                case "W":
                    Direction = "N";
                    break;
            }
        }
    }
}