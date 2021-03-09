using DroneDelivery.Logic.Models.Enums;
using DroneDelivery.Logic.Translator;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DroneDelivery.UnitTests.Translator
{
    public class CoordinateTranslatorTests
    {
        [Fact]
        public async Task CreateTranslator_WhenSourceIsNull_ShouldGetEmptyPlan()
        {
            var translator = new CoordinateTranslator();
            var result = translator.Translate(null);
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateTranslator_WhenSourceHasAnElement_ShouldGetOneDeliveryPlan()
        {
            var source = new string[] { "AID", "DIA", "AAI" };
            var translator = new CoordinateTranslator();
            var result = translator.Translate(source);
            result.Should().NotBeEmpty();
            result.Count().Should().Be(3);
            result.ElementAt(0).Address.Should().Be("St 1");
            result.ElementAt(0).Coordinates.Count().Should().Be(4);
            
            result.ElementAt(0).Coordinates.ElementAt(0).X.Should().Be(0);
            result.ElementAt(0).Coordinates.ElementAt(0).Y.Should().Be(0);
            result.ElementAt(0).Coordinates.ElementAt(0).Orientation.Should().Be(Orientation.N);

            result.ElementAt(0).Coordinates.ElementAt(1).X.Should().Be(0);
            result.ElementAt(0).Coordinates.ElementAt(1).Y.Should().Be(1);
            result.ElementAt(0).Coordinates.ElementAt(1).Orientation.Should().Be(Orientation.N);

            result.ElementAt(0).Coordinates.ElementAt(2).X.Should().Be(-1);
            result.ElementAt(0).Coordinates.ElementAt(2).Y.Should().Be(1);
            result.ElementAt(0).Coordinates.ElementAt(2).Orientation.Should().Be(Orientation.W);

            result.ElementAt(0).Coordinates.ElementAt(3).X.Should().Be(-1);
            result.ElementAt(0).Coordinates.ElementAt(3).Y.Should().Be(2);
            result.ElementAt(0).Coordinates.ElementAt(3).Orientation.Should().Be(Orientation.N);

            result.ElementAt(1).Address.Should().Be("St 2");
            result.ElementAt(1).Coordinates.Count().Should().Be(4);

            result.ElementAt(1).Coordinates.ElementAt(0).X.Should().Be(0);
            result.ElementAt(1).Coordinates.ElementAt(0).Y.Should().Be(0);
            result.ElementAt(1).Coordinates.ElementAt(0).Orientation.Should().Be(Orientation.N);

            result.ElementAt(1).Coordinates.ElementAt(1).X.Should().Be(1);
            result.ElementAt(1).Coordinates.ElementAt(1).Y.Should().Be(0);
            result.ElementAt(1).Coordinates.ElementAt(1).Orientation.Should().Be(Orientation.E);

            result.ElementAt(1).Coordinates.ElementAt(2).X.Should().Be(1);
            result.ElementAt(1).Coordinates.ElementAt(2).Y.Should().Be(1);
            result.ElementAt(1).Coordinates.ElementAt(2).Orientation.Should().Be(Orientation.N);

            result.ElementAt(1).Coordinates.ElementAt(3).X.Should().Be(1);
            result.ElementAt(1).Coordinates.ElementAt(3).Y.Should().Be(2);
            result.ElementAt(1).Coordinates.ElementAt(3).Orientation.Should().Be(Orientation.N);

            result.ElementAt(2).Address.Should().Be("St 3");
            result.ElementAt(2).Coordinates.Count().Should().Be(4);

            result.ElementAt(2).Coordinates.ElementAt(0).X.Should().Be(0);
            result.ElementAt(2).Coordinates.ElementAt(0).Y.Should().Be(0);
            result.ElementAt(2).Coordinates.ElementAt(0).Orientation.Should().Be(Orientation.N);

            result.ElementAt(2).Coordinates.ElementAt(1).X.Should().Be(0);
            result.ElementAt(2).Coordinates.ElementAt(1).Y.Should().Be(1);
            result.ElementAt(2).Coordinates.ElementAt(1).Orientation.Should().Be(Orientation.N);

            result.ElementAt(2).Coordinates.ElementAt(2).X.Should().Be(0);
            result.ElementAt(2).Coordinates.ElementAt(2).Y.Should().Be(2);
            result.ElementAt(2).Coordinates.ElementAt(2).Orientation.Should().Be(Orientation.N);

            result.ElementAt(2).Coordinates.ElementAt(3).X.Should().Be(-1);
            result.ElementAt(2).Coordinates.ElementAt(3).Y.Should().Be(2);
            result.ElementAt(2).Coordinates.ElementAt(3).Orientation.Should().Be(Orientation.W);
        }
    }
}