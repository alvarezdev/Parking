using System;
namespace Parking.Data
{
    public class LongToDateTime : ITypeConverter<long, DateTime>
    {
        public LongToDateTime()
        {
        }

        public DateTime Convert(long source)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(source).ToUniversalTime();
        }
    }
}
