using System;
using System.Collections.Generic;
using System.Linq; // Add this for LINQ methods like Count()
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

        // Retrieve a meeting by ID
        public Meeting GetMeetingById(int id)
        {
            var meeting = meetingRepository.GetMeetingById(id);
            if (meeting == null)
            {
                throw new KeyNotFoundException($"Meeting with ID {id} not found.");
            }
            return meeting;
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

        // Schedule a meeting between a student and a supervisor
        public void ScheduleMeeting(int studentId, int supervisorId, DateTime date)
        {
            try
            {
                // Validate supervisor existence
                var supervisor = personalSupervisorRepository.GetPersonalSupervisorById(supervisorId);
                if (supervisor == null)
                {
                    Console.WriteLine($"Error: Supervisor with ID {supervisorId} not found. Please try again.");
                    return;
                }

                // Validate student existence
                var student = studentRepository.GetStudentById(studentId);
                if (student == null)
                {
                    Console.WriteLine($"Error: Student with ID {studentId} is not logged in. Please try again.");
                    return;
                }

                // Proceed to create and save meeting
                var meeting = new Meeting
                {
                    Id = meetingRepository.GetAllMeetings().Count() + 1,
                    StudentId = studentId,
                    PersonalSupervisorId = supervisorId,
                    Date = date.Date.AddHours(12)
                };
                meetingRepository.AddMeeting(meeting);

                Console.WriteLine("Meeting successfully scheduled!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

        }
    }
}

