//seniorTutor.cs
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EngagementTrackingSystem.Models
{
    // Represents a senior tutor who oversees personal supervisors
    public class SeniorTutor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Default value
        public string Email { get; set; } = string.Empty; // Default value
        public virtual ICollection<PersonalSupervisor> PersonalSupervisors { get; set; } = new List<PersonalSupervisor>(); // Default value
    }

}