using DroneDelivery.Logic.Models.Enums;

namespace DroneDelivery.Logic.Models
{
    public struct Coordinates
    {
        public Coordinates(int x, int y, Directions direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Directions Direction { get; set; }

        public override string ToString() => $"({X}, {Y}), Direction {DirectionName}";

        private string DirectionName => (Direction) switch
        {
            Directions.N => "North",
            Directions.S => "South",
            Directions.W => "West",
            Directions.E => "East",
            _ => throw new System.NotImplementedException(),
        };
    }
}