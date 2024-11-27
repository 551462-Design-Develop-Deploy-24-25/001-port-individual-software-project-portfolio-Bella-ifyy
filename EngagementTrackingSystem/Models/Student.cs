//student.cs
// Using directives
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EngagementTrackingSystem.Models
{
    // Represents a student in the engagement tracking system
    public class Student
    {
        public int Id { get; set; } // Unique identifier for the student
        public string Name { get; set; } // Name of the student
        public string Email { get; set; } // Email address
        public string StatusReport { get; set; }  // Status report provided by the student

        [JsonIgnore] // Do not serialize Meetings
        public virtual ICollection<Meeting> Meetings { get; set; }
    } // Collection of meetings the student has
}