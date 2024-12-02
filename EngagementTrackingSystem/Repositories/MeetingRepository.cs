using System;
using System.Collections.Generic;
using System.Linq;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    public class MeetingRepository
    {
        private List<Meeting> meetings;
        private JsonDataStorage dataStorage;

        public MeetingRepository(string filePath)
        {
            dataStorage = new JsonDataStorage(filePath);
            meetings = dataStorage.LoadData<List<Meeting>>() ?? new List<Meeting>();

            // Reset ID counter based on current meetings
            if (meetings.Count > 0)
            {
                nextId = meetings.Max(m => m.Id) + 1;
            }
            else
            {
                nextId = 1;
            }
        }

        private int nextId;

        public void AddMeeting(Meeting meeting)
        {
            meeting.Id = nextId++;
            meetings.Add(meeting);
            dataStorage.SaveData(meetings);
        }


        public IEnumerable<Meeting> GetAllMeetings()
        {
            return meetings;
        }

        //public Meeting GetMeetingById(int id)
        //{
        //    var meeting = meetings.Find(m => m.Id == id);
        //    if (meeting == null)
        //    {
        //        Console.WriteLine($"[Warning] Meeting with ID {id} not found.");
        //        throw new KeyNotFoundException($"Meeting with ID {id} not found.");
        //    }
        //    return meeting;
        //}

        public void UpdateMeeting(Meeting meeting)
        {
            var existingMeeting = meetings.Find(m => m.Id == meeting.Id);
            if (existingMeeting == null)
            {
                Console.WriteLine($"[Warning] Cannot update meeting with ID {meeting.Id}. Meeting not found.");
                return;
            }
            existingMeeting.Date = meeting.Date;
            existingMeeting.StudentId = meeting.StudentId;
            existingMeeting.PersonalSupervisorId = meeting.PersonalSupervisorId;
            dataStorage.SaveData(meetings);
        }

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

        public IEnumerable<Meeting> GetMeetingsByStudentId(int studentId)
        {
            return meetings.Where(m => m.StudentId == studentId);
        }

        public IEnumerable<Meeting> GetMeetingsBySupervisorId(int supervisorId)
        {
            return meetings.Where(m => m.PersonalSupervisorId == supervisorId);
        }

        private int GenerateMeetingId()
        {
            return meetings.Any() ? meetings.Max(m => m.Id) + 1 : 1;
        }
    }
}
