//personalSupervisor.cs
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EngagementTrackingSystem.Models
{
    // Represents a personal supervisor who oversees students
    public class PersonalSupervisor
    {
        public int Id { get; set; } // Unique identifier for the personal supervisor
        public string Name { get; set; } // Name of the supervisor
        public string Email { get; set; } // Email address

        [JsonIgnore] // Do not serialize navigation properties
        public virtual ICollection<Student> Students { get; set; }

        [JsonIgnore]
        public virtual ICollection<Meeting> Meetings { get; set; }
    }
}
