//student.cs
// Using directives
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace EngagementTrackingSystem.Models
{
    // Represents a student in the engagement tracking system
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Default value
        public string Email { get; set; } = string.Empty; // Default value
        public string StatusReport { get; set; } = string.Empty; // Default value
        public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>(); // Default value
    }

}