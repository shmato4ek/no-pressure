namespace NoPressure.BLL.Helpers
{
    public class NotificationLinkHelper
    {
        private static string apiUrl = "/profile/";
        public static string GetNewSubscriptionLink(string email)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(email);
            var id = System.Convert.ToBase64String(plainTextBytes);
            return apiUrl + id;
        }
    }
}
