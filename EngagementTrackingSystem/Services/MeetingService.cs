// Updated MeetingService.cs
using System;
using System.Collections.Generic;
using System.Linq; // For LINQ methods like Count()
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;

namespace EngagementTrackingSystem.Service
{
    public class MeetingService
    {
        private readonly MeetingRepository meetingRepository;
        private readonly PersonalSupervisorRepository personalSupervisorRepository;
        private readonly StudentRepository studentRepository;

        public MeetingService(
            MeetingRepository meetingRepository,
            PersonalSupervisorRepository personalSupervisorRepository,
            StudentRepository studentRepository)
        {
            this.meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
            this.personalSupervisorRepository = personalSupervisorRepository ?? throw new ArgumentNullException(nameof(personalSupervisorRepository));
            this.studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        }

        // Retrieve all meetings
        public IEnumerable<Meeting> GetAllMeetings()
        {
            return meetingRepository.GetAllMeetings();
        }

        // Add a new meeting
        public void AddMeeting(Meeting meeting)
        {
            if (meeting == null)
            {
                throw new ArgumentNullException(nameof(meeting));
            }
            meetingRepository.AddMeeting(meeting);
        }

        // Schedule a meeting
        public void ScheduleMeeting(int studentId, int supervisorId, DateTime date)
        {
            try
            {
                var supervisor = personalSupervisorRepository.GetPersonalSupervisorById(supervisorId)
                ?? throw new KeyNotFoundException($"Supervisor with ID {supervisorId} not found.");

                var student = studentRepository.GetStudentById(studentId)
                    ?? throw new KeyNotFoundException($"Student with ID {studentId} not found.");

                if (supervisor == null)
                {
                    Console.WriteLine($"Error: Supervisor with ID {supervisorId} not found. Please try again.");
                    return;
                }

                if (student == null)
                {
                    Console.WriteLine($"Error: Student with ID {studentId} not found. Please try again.");
                    return;
                }

                var meeting = new Meeting
                {
                    Id = meetingRepository.GetAllMeetings().Count() + 1,
                    StudentId = studentId,
                    PersonalSupervisorId = supervisorId,
                    Date = date.Date.AddHours(12)
                };
                meetingRepository.AddMeeting(meeting);

                // Update supervisor's meeting list
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
