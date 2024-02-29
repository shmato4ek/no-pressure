using System;

namespace NoPressure.BLL.Exceptions
{
    public sealed class UserInTeamException : Exception
    {
        public UserInTeamException(int userId, int teamId)
            : base($"User with id {userId} is already exist in team with id {teamId}")
        {
            
        }
    }
}