//personalSupervisor.cs
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EngagementTrackingSystem.Models
{
    // Represents a personal supervisor who oversees students
    public class PersonalSupervisor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Default value
        public string Email { get; set; } = string.Empty; // Default value
        public virtual ICollection<Student> Students { get; set; } = new List<Student>(); // Default value
        public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>(); // Default value
    }

}
