//meeting.cs
using System;

namespace EngagementTrackingSystem.Models
{
    // Represents a meeting between a student and a personal supervisor
    public class Meeting
    {
        public int Id { get; set; } // Unique identifier for the meeting
        public DateTime Date { get; set; } // Date and time of the meeting
        public int StudentId { get; set; } // ID of the student attending the meeting
        public int PersonalSupervisorId { get; set; } // ID of the personal supervisor
    }
}