using System;

namespace NoPressure.BLL.Exceptions
{
    public sealed class InvalidUserNameOrPasswordException : Exception
    {
        public InvalidUserNameOrPasswordException() 
            : base("Invalid username or password.") 
        {

        }
    }
}