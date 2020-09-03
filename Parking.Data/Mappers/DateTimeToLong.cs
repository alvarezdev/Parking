using System;

namespace Parking.Data
{
    public class DateTimeToLong : ITypeConverter<DateTime, long>
    {
        public DateTimeToLong()
        {
        }

        public long Convert(DateTime source)
        {
            return (long)source.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}
