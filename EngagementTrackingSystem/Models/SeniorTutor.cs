//seniorTutor.cs
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EngagementTrackingSystem.Models
{
    // Represents a senior tutor who oversees personal supervisors
    public class SeniorTutor
    {
        public int Id { get; set; } // Unique identifier for the senior tutor
        public string Name { get; set; } // Name of the senior tutor
        public string Email { get; set; } // Email address

        [JsonIgnore] // Do not serialize navigation properties
        public virtual ICollection<PersonalSupervisor> PersonalSupervisors { get; set; }
    }
}