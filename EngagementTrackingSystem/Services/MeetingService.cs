//MeetingService.cs
using System;
using System.Collections.Generic;
using System.Linq; // For LINQ methods like Count()
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;

namespace EngagementTrackingSystem.Service
{
    // Service class for meeting-related operations
    public class MeetingService
    {
        private readonly MeetingRepository meetingRepository; // Handles meeting data
        private readonly PersonalSupervisorRepository personalSupervisorRepository; // Handles supervisor data
        private readonly StudentRepository studentRepository; // Handles student data

        // Constructor initializes repositories
        public MeetingService(
            MeetingRepository meetingRepository,
            PersonalSupervisorRepository personalSupervisorRepository,
            StudentRepository studentRepository)
        {
            this.meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
            this.personalSupervisorRepository = personalSupervisorRepository ?? throw new ArgumentNullException(nameof(personalSupervisorRepository));
            this.studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        }

        // Retrieves all meetings from the repository
        public IEnumerable<Meeting> GetAllMeetings()
        {
            return meetingRepository.GetAllMeetings();
        }

        // Adds a new meeting to the repository
        public void AddMeeting(Meeting meeting)
        {
            if (meeting == null)
            {
                throw new ArgumentNullException(nameof(meeting));
            }
            meetingRepository.AddMeeting(meeting);
        }

        // Schedules a meeting between a student and a supervisor
        public void ScheduleMeeting(int studentId, int supervisorId, DateTime date)
        {
            try
            {
                // Fetch supervisor and student from repositories
                var supervisor = personalSupervisorRepository.GetPersonalSupervisorById(supervisorId)
                ?? throw new KeyNotFoundException($"Supervisor with ID {supervisorId} not found.");

                var student = studentRepository.GetStudentById(studentId)
                    ?? throw new KeyNotFoundException($"Student with ID {studentId} not found.");
                
                
                // Creates a new meeting 
                var meeting = new Meeting
                {
                    Id = meetingRepository.GetAllMeetings().Count() + 1,
                    StudentId = studentId,
                    PersonalSupervisorId = supervisorId,
                    Date = date.Date.AddHours(12)
                };

                // Adds meeting to the repoisitory
                meetingRepository.AddMeeting(meeting);

                // Link meeting to supervisor and update repository
                supervisor.Meetings.Add(meeting);
                personalSupervisorRepository.UpdatePersonalSupervisor(supervisor);

                Console.WriteLine("Meeting successfully scheduled!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
