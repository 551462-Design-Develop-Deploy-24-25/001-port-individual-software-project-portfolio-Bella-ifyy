//PersonalSupervisor.cs
using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    // Handles data access and management for personal supervisors
    public class PersonalSupervisorRepository
    {
        private List<PersonalSupervisor> personalSupervisors; // In-memory list of personal supervisors
        private JsonDataStorage dataStorage; // Utility for data persistence

        // Constructor initializes repository with file storage
        public PersonalSupervisorRepository(string filePath)
        {
            dataStorage = new JsonDataStorage(filePath);
            personalSupervisors = dataStorage.LoadData<List<PersonalSupervisor>>() ?? new List<PersonalSupervisor>();
        }

        // Retrieves all personal supervisors
        public IEnumerable<PersonalSupervisor> GetAllPersonalSupervisors()
        {
            return personalSupervisors;
        }

        // Retrieves a personal supervisor by their ID
        public PersonalSupervisor GetPersonalSupervisorById(int id)
        {
            return personalSupervisors.Find(ps => ps.Id == id);// ?? throw new KeyNotFoundException($"Senior Tutor with ID {id} not found.");
        }

        // Adds a new personal supervisor and saves to storage
        public void AddPersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            personalSupervisors.Add(personalSupervisor);
            dataStorage.SaveData(personalSupervisors);
        }

        // Updates an existing personal supervisor
        public void UpdatePersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            var existingSupervisor = GetPersonalSupervisorById(personalSupervisor.Id);
            if (existingSupervisor != null)
            {
                existingSupervisor.Name = personalSupervisor.Name ?? existingSupervisor.Name;
                existingSupervisor.Email = personalSupervisor.Email ?? existingSupervisor.Email;
                existingSupervisor.Students = personalSupervisor.Students;
                existingSupervisor.Meetings = personalSupervisor.Meetings;
                dataStorage.SaveData(personalSupervisors);
            }
        }

        // Deletes a personal supervisor by their ID
        public void DeletePersonalSupervisor(int id)
        {
            var supervisor = GetPersonalSupervisorById(id);
            if (supervisor != null)
            {
                personalSupervisors.Remove(supervisor);
                dataStorage.SaveData(personalSupervisors); // Save updated data
            }
        }

    }
}
