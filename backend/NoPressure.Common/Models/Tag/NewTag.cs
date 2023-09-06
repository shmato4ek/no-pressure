using System.Drawing;
using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.Tag
{
    public class NewTag
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}