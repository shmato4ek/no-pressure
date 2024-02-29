using System;

namespace NoPressure.BLL.Exceptions
{
    public class NoAccessException : Exception
    {
        public NoAccessException() 
            : base($"Access denied") 
        {

        }
    }
}