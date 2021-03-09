using DroneDelivery.Logic.Models;
using DroneDelivery.Logic.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DroneDelivery.Logic.Translator
{
    public class CoordinateTranslator : ITranslator<IEnumerable<string>, IEnumerable<DeliveryPlan>>
    {
        public IEnumerable<DeliveryPlan> Translate(IEnumerable<string> source)
        {
            if (source == null)
                return Enumerable.Empty<DeliveryPlan>();

            var plan = new List<DeliveryPlan>(source.Count());
            int index = 0;
            foreach (var movements in source)
            {
                int currentX = 0, currentY = 0;
                var currentDirection = Directions.N;
                var coordinates = new List<Coordinates> { new Coordinates { X = 0, Y = 0, Direction = Directions.N } };

                foreach (var movement in movements)
                {
                    currentDirection = CalculateDirection(currentDirection, movement);
                    switch (currentDirection)
                    {
                        case Directions.N:
                            currentY++;
                            break;
                        case Directions.S:
                            currentY--;
                            break;
                        case Directions.W:
                            currentX--;
                            break;
                        case Directions.E:
                            currentX++;
                            break;
                    }
                    coordinates.Add(new Coordinates { X = currentX, Y = currentY, Direction = currentDirection });
                }
                plan.Add(new DeliveryPlan { Address = $"St {++index}", Coordinates = coordinates });
            }
            return plan;
        }

        private static Directions CalculateDirection(Directions currentDirection, char movement) => (currentDirection, movement) switch
        {
            (Directions.N, 'A') => Directions.N,
            (Directions.N, 'I') => Directions.W,
            (Directions.N, 'D') => Directions.E,
            (Directions.N, _) => Directions.N,

            (Directions.S, 'A') => Directions.S,
            (Directions.S, 'I') => Directions.E,
            (Directions.S, 'D') => Directions.W,
            (Directions.S, _) => Directions.S,

            (Directions.W, 'A') => Directions.W,
            (Directions.W, 'I') => Directions.S,
            (Directions.W, 'D') => Directions.N,
            (Directions.W, _) => Directions.W,

            (Directions.E, 'A') => Directions.E,
            (Directions.E, 'I') => Directions.N,
            (Directions.E, 'D') => Directions.S,
            (Directions.E, _) => Directions.E,

            _ => throw new System.NotImplementedException(),
        };
    }
}