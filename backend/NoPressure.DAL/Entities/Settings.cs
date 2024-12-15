using NoPressure.Common.Enums;

namespace NoPressure.DAL.Entities
{
    public class Settings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public SettingsPrivacy Statistic { get; set; }
        public SettingsPrivacy Activities { get; set; }
    }
}
