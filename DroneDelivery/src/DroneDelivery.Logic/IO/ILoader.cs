using System.Threading.Tasks;

namespace DroneDelivery.Logic.IO
{
    public interface ILoader<T>
    {
        T LoadInfo();
    }
}