//SeniorTutor.cs
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EngagementTrackingSystem.Models
{
    // Represents a senior tutor who oversees personal supervisors
    public class SeniorTutor
    {
        public int Id { get; set; } // Unique identifier for the supervisor
        public string Name { get; set; } = string.Empty; // Senior Tutor's name, defaulted to an empty string
        public string Email { get; set; } = string.Empty; // Senior Tutor's email, defaulted to an empty string
        public virtual ICollection<PersonalSupervisor> PersonalSupervisors { get; set; } = new List<PersonalSupervisor>(); // List of personal supervisors managed by the senior tutor
    }

}