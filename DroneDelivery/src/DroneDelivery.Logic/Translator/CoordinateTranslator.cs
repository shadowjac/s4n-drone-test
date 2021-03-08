using DroneDelivery.Logic.Models;
using DroneDelivery.Logic.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DroneDelivery.Logic.Translator
{
    public class CoordinateTranslator : ITranslator<IEnumerable<string>, ICollection<DeliveryPlan>>
    {
        public ICollection<DeliveryPlan> Translate(IEnumerable<string> source)
        {
            var plan = new List<DeliveryPlan>(source.Count());
            int index = 0, currentX = 0, currentY = 0;
            var currentOrientation = Orientation.N;

            foreach (var movements in source)
            {
                var coordinates = new List<Coordinates> { new Coordinates { X = 0, Y = 0, Orientation = Orientation.N } };

                foreach (var movement in movements)
                {
                    switch (currentOrientation)
                    {
                        case Orientation.N:
                            currentY++;
                            break;
                        case Orientation.S:
                            currentY--;
                            break;
                        case Orientation.W:
                            currentX--;
                            break;
                        case Orientation.E:
                            currentX++;
                            break;
                    }
                    currentOrientation = CalculateOrientation(currentOrientation, movement);
                    coordinates.Add(new Coordinates { X = currentX, Y = currentY, Orientation = currentOrientation });
                }
                plan.Add(new DeliveryPlan { Address = $"St {++index}", Coordinates = coordinates });
            }
            return plan;
        }

        private static Orientation CalculateOrientation(Orientation currentOrientation, char movement) => (currentOrientation, movement) switch
        {
            (Orientation.N, 'A') => Orientation.N,
            (Orientation.N, 'I') => Orientation.W,
            (Orientation.N, 'D') => Orientation.E,
            (Orientation.N, _) => Orientation.N,

            (Orientation.S, 'A') => Orientation.S,
            (Orientation.S, 'I') => Orientation.E,
            (Orientation.S, 'D') => Orientation.W,
            (Orientation.S, _) => Orientation.S,

            (Orientation.W, 'A') => Orientation.W,
            (Orientation.W, 'I') => Orientation.S,
            (Orientation.W, 'D') => Orientation.N,
            (Orientation.W, _) => Orientation.W,

            (Orientation.E, 'A') => Orientation.E,
            (Orientation.E, 'I') => Orientation.N,
            (Orientation.E, 'D') => Orientation.S,
            (Orientation.E, _) => Orientation.E,

            _ => throw new System.NotImplementedException(),
        };
    }
}