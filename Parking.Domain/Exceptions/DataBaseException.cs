using System;
namespace Parking.Domain
{
    public class DataBaseException : Exception
    {
        public DataBaseException(string message) : base(message)
        {
        }
    }
}
