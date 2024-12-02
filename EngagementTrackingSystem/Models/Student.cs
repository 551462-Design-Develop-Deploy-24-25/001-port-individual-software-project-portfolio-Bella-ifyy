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
        [JsonIgnore] // Exclude from serialization
        public string StatusReport { get; set; } = string.Empty;

        [JsonIgnore] // Exclude from serialization
        public List<Meeting>? Meetings { get; set; }
    }

}