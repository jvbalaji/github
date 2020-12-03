namespace MarsRover
{
    /// <summary>
    /// Class allows to create a Plateau
    /// </summary>
    public class Plateau
    {
        public int PlateauWidth { get; }
        public int PlateauHeight { get; }

        public Plateau(int width, int height)
        {
            PlateauWidth = width;
            PlateauHeight = height;
        }
    }
}