using DroneDelivery.Logic.Models.Enums;

namespace DroneDelivery.Logic.Models
{
    public struct Coordinates
    {
        public Coordinates(int x, int y, Orientation orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Orientation Orientation { get; set; }

        public override string ToString() => $"({X}, {Y}), Direction {Orientation}";
    }
}