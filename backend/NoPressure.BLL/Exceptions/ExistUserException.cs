using System;

namespace NoPressure.BLL.Exceptions
{
    public class ExistUserException : Exception
    {
        public ExistUserException(string email)
            : base($"User with email {email} already exists.")
        {

        }
    }
}