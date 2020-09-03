namespace Parking.Data
{
    public interface ITypeConverter<in TSource, TDestination>
    {
        TDestination Convert(TSource source);
    }
}
