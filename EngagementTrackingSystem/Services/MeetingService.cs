//meetingService.cs
using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;

namespace EngagementTrackingSystem.Service
{
    // Service class for meeting-related operations
    public class MeetingService
    {
        private MeetingRepository meetingRepository;

        // Constructor accepts a meeting repository instance
        public MeetingService(MeetingRepository meetingRepository)
        {
            this.meetingRepository = meetingRepository;
        }

        // Retrieves all meetings
        public IEnumerable<Meeting> GetAllMeetings()
        {
            return meetingRepository.GetAllMeetings();
        }

        // Retrieves a meeting by ID
        public Meeting GetMeetingById(int id)
        {
            return meetingRepository.GetMeetingById(id);
        }

        // Adds a new meeting
        public void AddMeeting(Meeting meeting)
        {
            meetingRepository.AddMeeting(meeting);
        }

        // Updates an existing meeting
        public void UpdateMeeting(Meeting meeting)
        {
            meetingRepository.UpdateMeeting(meeting);
        }

        // Deletes a meeting by ID
        public void DeleteMeeting(int id)
        {
            meetingRepository.DeleteMeeting(id);
        }

        // Schedules a meeting between a student and a supervisor
        public void ScheduleMeeting(int studentId, int supervisorId, DateTime date)
        {
            var meeting = new Meeting
            {
                StudentId = studentId,
                PersonalSupervisorId = supervisorId,
                Date = date.Date.AddHours(12)
                // Description = description // Currently commented out
            };
            meetingRepository.AddMeeting(meeting);
        }
    }
}
