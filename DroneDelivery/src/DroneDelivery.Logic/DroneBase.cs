namespace DroneDelivery.Logic
{
    public class DroneBase
    {
        public string Key { get; init; }
        public DroneBase(string key)
        {
            Key = key;
        }
    }
}