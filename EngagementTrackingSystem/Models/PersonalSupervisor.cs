//PersonalSupervisor.cs
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EngagementTrackingSystem.Models
{
    // Represents a personal supervisor who oversees students
    public class PersonalSupervisor
    {
        public int Id { get; set; } // Unique identifier for the supervisor
        public string Name { get; set; } = string.Empty; // Supervisor's name, defaulted to an empty string
        public string Email { get; set; } = string.Empty; // Supervisor's email, defaulted to an empty string

        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; } = new List<Student>(); // Excluded from serialization

        [JsonIgnore]
        public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>(); // Excluded from serialization
    }


}
