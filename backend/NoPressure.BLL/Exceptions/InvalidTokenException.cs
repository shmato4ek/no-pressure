using System;

namespace NoPressure.BLL.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string tokenName) 
            : base($"Invalid {tokenName} token.") 
        {

        }
    }
}