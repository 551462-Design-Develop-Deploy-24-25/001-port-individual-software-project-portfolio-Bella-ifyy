//Student.cs
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace EngagementTrackingSystem.Models
{
    // Represents a student in the Engagement Tracking System
    public class Student
    {
        public int Id { get; set; } // Unique identifier for the student
        public string Name { get; set; } = string.Empty; // Student's name, defaulted to an empty string
        public string Email { get; set; } = string.Empty; // Student's email, defaulted to an empty string

        [JsonIgnore] // Excluded from serialization
        public string StatusReport { get; set; } = string.Empty;

        [JsonIgnore] // Excluded from serialization
        public List<Meeting>? Meetings { get; set; }
    }

}