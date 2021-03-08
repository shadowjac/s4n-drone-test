namespace DroneDelivery.Logic.Translator
{
    public interface ITranslator<TSource, TDestination>
    {
        TDestination Translate(TSource source);
    }
}