﻿namespace NoPressure.DAL.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PlanId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
