//personalSupervisorRepository.cs
// Using directives
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    // Repository class for managing personal supervisors
    public class PersonalSupervisorRepository
    {
        private List<PersonalSupervisor> personalSupervisors = new List<PersonalSupervisor>(); // In-memory list
        private JsonDataStorage dataStorage; // Data storage utility

        // Constructor initializes data storage and loads existing supervisors
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

        // Retrieves a personal supervisor by ID
        public PersonalSupervisor GetPersonalSupervisorById(int id)
        {
            return personalSupervisors.Find(ps => ps.Id == id);
        }

        // Adds a new personal supervisor and saves data
        public void AddPersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            personalSupervisors.Add(personalSupervisor);
            dataStorage.SaveData(personalSupervisors); // Save after adding
        }

        // Updates an existing personal supervisor and saves data
        public void UpdatePersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            var existingPersonalSupervisor = GetPersonalSupervisorById(personalSupervisor.Id);
            if (existingPersonalSupervisor != null)
            {
                existingPersonalSupervisor.Name = personalSupervisor.Name;
                existingPersonalSupervisor.Email = personalSupervisor.Email;
                dataStorage.SaveData(personalSupervisors); // Save after updating
            }
        }

        // Deletes a personal supervisor by ID and saves data
        public void DeletePersonalSupervisor(int id)
        {
            var personalSupervisor = GetPersonalSupervisorById(id);
            if (personalSupervisor != null)
            {
                personalSupervisors.Remove(personalSupervisor);
                dataStorage.SaveData(personalSupervisors); // Save after deleting
            }
        }
    }
}
