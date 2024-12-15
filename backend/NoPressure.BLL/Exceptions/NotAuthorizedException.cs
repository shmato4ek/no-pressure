using System;

namespace NoPressure.BLL.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() 
            : base($"User is not authorized") 
        {

        }
    }
}