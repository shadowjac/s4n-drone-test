using System.Threading.Tasks;

namespace DroneDelivery.Logic.IO
{
    public interface ILoader<T>
    {
        Task<T> LoadInfoAsync();
    }
}