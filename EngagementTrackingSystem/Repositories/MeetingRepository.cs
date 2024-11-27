//meetingRepository.cs
// Using directives
using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    // Repository class for managing meetings
    public class MeetingRepository
    {
        private List<Meeting> meetings = new List<Meeting>(); // In-memory list of meetings
        private JsonDataStorage dataStorage; // Data storage utility

        // Constructor initializes data storage and loads existing meetings
        public MeetingRepository(string filePath)
        {
            dataStorage = new JsonDataStorage(filePath);
            meetings = dataStorage.LoadData<List<Meeting>>() ?? new List<Meeting>();
        }

        // Retrieves all meetings
        public IEnumerable<Meeting> GetAllMeetings()
        {
            return meetings;
        }

        // Retrieves a meeting by its unique ID
        public Meeting GetMeetingById(int id)
        {
            return meetings.Find(m => m.Id == id);
        }

        // Adds a new meeting and saves data
        public void AddMeeting(Meeting meeting)
        {
            // Auto-generate a unique ID
            meeting.Id = meetings.Count > 0 ? meetings.Max(m => m.Id) + 1 : 1;
            meetings.Add(meeting);
            dataStorage.SaveData(meetings); // Save after adding
        }

        // Updates an existing meeting and saves data
        public void UpdateMeeting(Meeting meeting)
        {
            var existingMeeting = GetMeetingById(meeting.Id);
            if (existingMeeting != null)
            {
                existingMeeting.Date = meeting.Date;
                // existingMeeting.Description = meeting.Description; // Commented out
                dataStorage.SaveData(meetings); // Save after updating
            }
        }

        // Deletes a meeting by ID and saves data
        public void DeleteMeeting(int id)
        {
            var meeting = GetMeetingById(id);
            if (meeting != null)
            {
                meetings.Remove(meeting);
                dataStorage.SaveData(meetings); // Save after deleting
            }
        }
    }
}