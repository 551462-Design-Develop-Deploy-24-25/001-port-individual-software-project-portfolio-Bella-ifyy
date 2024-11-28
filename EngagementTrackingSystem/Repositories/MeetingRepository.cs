using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    public class MeetingRepository
    {
        private List<Meeting> meetings; // In-memory list of meetings
        private JsonDataStorage dataStorage; // Data storage utility

        public MeetingRepository(string filePath)
        {
            dataStorage = new JsonDataStorage(filePath);

            // Ensure the data is never null
            meetings = dataStorage.LoadData<List<Meeting>>() ?? throw new InvalidOperationException("Meeting data is missing or invalid.");
        }

        // Retrieves all meetings
        public IEnumerable<Meeting> GetAllMeetings()
        {
            return meetings; // Safe because meetings is initialized in the constructor
        }

        // Retrieves a meeting by ID
        public Meeting GetMeetingById(int id)
        {
            return meetings.Find(m => m.Id == id) ?? throw new KeyNotFoundException($"Meeting with ID {id} not found.");
        }

        // Adds a new meeting and saves data
        public void AddMeeting(Meeting meeting)
        {
            meetings.Add(meeting);
            dataStorage.SaveData(meetings); // Save updated data
        }

        // Updates an existing meeting and saves data
        public void UpdateMeeting(Meeting meeting)
        {
            var existingMeeting = GetMeetingById(meeting.Id);
            if (existingMeeting != null)
            {
                existingMeeting.Date = meeting.Date;
                existingMeeting.StudentId = meeting.StudentId;
                existingMeeting.PersonalSupervisorId = meeting.PersonalSupervisorId;
                dataStorage.SaveData(meetings); // Save updated data
            }
        }

        // Deletes a meeting by ID
        public void DeleteMeeting(int id)
        {
            var meeting = GetMeetingById(id);
            if (meeting != null)
            {
                meetings.Remove(meeting);
                dataStorage.SaveData(meetings); // Save updated data
            }
        }
    }
}
