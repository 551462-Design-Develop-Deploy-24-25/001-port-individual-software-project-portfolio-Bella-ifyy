using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    public class PersonalSupervisorRepository
    {
        private List<PersonalSupervisor> personalSupervisors; // In-memory list
        private JsonDataStorage dataStorage; // Data storage utility

        public PersonalSupervisorRepository(string filePath)
        {
            dataStorage = new JsonDataStorage(filePath);

            // Ensure the data is never null
            personalSupervisors = dataStorage.LoadData<List<PersonalSupervisor>>() ?? throw new InvalidOperationException("Personal Supervisor data is missing or invalid.");
        }

        // Retrieves all personal supervisors
        public IEnumerable<PersonalSupervisor> GetAllPersonalSupervisors()
        {
            return personalSupervisors; // Safe because personalSupervisors is initialized in the constructor
        }

        // Retrieves a personal supervisor by ID
        public PersonalSupervisor GetPersonalSupervisorById(int id)
        {
            return personalSupervisors.Find(ps => ps.Id == id) ?? throw new KeyNotFoundException($"Personal Supervisor with ID {id} not found.");
        }

        // Adds a new personal supervisor and saves data
        public void AddPersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            personalSupervisors.Add(personalSupervisor);
            dataStorage.SaveData(personalSupervisors); // Save updated data
        }

        // Updates an existing personal supervisor and saves data
        public void UpdatePersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            var existingSupervisor = GetPersonalSupervisorById(personalSupervisor.Id);
            if (existingSupervisor != null)
            {
                existingSupervisor.Name = personalSupervisor.Name;
                existingSupervisor.Email = personalSupervisor.Email;
                dataStorage.SaveData(personalSupervisors); // Save updated data
            }
        }

        // Deletes a personal supervisor by ID
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
