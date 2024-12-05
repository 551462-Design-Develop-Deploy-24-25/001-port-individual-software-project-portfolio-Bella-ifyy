//MeetingRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    // Handles data access and management for meetings
    public class MeetingRepository
    {
        private List<Meeting> meetings; // In-memory meeting list
        private JsonDataStorage dataStorage; // Utility for JSON file handling
        private int nextId; // Tracks the next ID for meetings

        // Constructor initializes repository with file storage
        public MeetingRepository(string filePath)
        {
            dataStorage = new JsonDataStorage(filePath);
            meetings = dataStorage.LoadData<List<Meeting>>() ?? new List<Meeting>();

            // Reset ID counter based on current meetings
            if (meetings.Count > 0)
            {
                // Initialize nextId based on existing meetings
                nextId = meetings.Max(m => m.Id) + 1;
            }
            else
            {
                nextId = 1;
            }
        }

        // Retrieves meetings for a specific student
        public IEnumerable<Meeting> GetMeetingsByStudentId(int studentId)
        {
            return meetings.Where(m => m.StudentId == studentId);
        }

        // Retrieves meetings for a specific supervisor
        public IEnumerable<Meeting> GetMeetingsBySupervisorId(int supervisorId)
        {
            return meetings.Where(m => m.PersonalSupervisorId == supervisorId);
        }
        // Adds a new meeting and saves to storage
        public void AddMeeting(Meeting meeting)
        {
            meeting.Id = nextId++;
            meetings.Add(meeting);
            dataStorage.SaveData(meetings);
        }

        // Retrieves all meetings
        public IEnumerable<Meeting> GetAllMeetings()
        {
            return meetings;
        }

        // Updates an existing meeting
        public void UpdateMeeting(Meeting meeting)
        {
            var existingMeeting = meetings.Find(m => m.Id == meeting.Id);
            if (existingMeeting == null)
            {
                Console.WriteLine($"[Warning] Cannot update meeting with ID {meeting.Id}. Meeting not found.");
                return;
            }
            // Update meeting details
            existingMeeting.Date = meeting.Date;
            existingMeeting.StudentId = meeting.StudentId;
            existingMeeting.PersonalSupervisorId = meeting.PersonalSupervisorId;
            dataStorage.SaveData(meetings);
        }

        // Deletes a meeting by ID
        public void DeleteMeeting(int id)
        {
            var meeting = meetings.Find(m => m.Id == id);
            if (meeting == null)
            {
                Console.WriteLine($"[Warning] Cannot delete meeting with ID {id}. Meeting not found.");
                return;
            }
            meetings.Remove(meeting);
            dataStorage.SaveData(meetings);
        }

    }
}
