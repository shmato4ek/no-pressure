using Microsoft.AspNetCore.Mvc;

namespace NoPressure.API.Extentions
{
    public static class ControllerExtentions
    {
        public static int GetUserIdFromToken(this ControllerBase controller)
        {
            var claimsUserId = controller.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

            if (string.IsNullOrEmpty(claimsUserId))
            {
                throw new Exception("Invalid token");
            }

            return int.Parse(claimsUserId);
        }
    }
}