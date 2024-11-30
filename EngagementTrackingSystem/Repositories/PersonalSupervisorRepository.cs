using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    public class PersonalSupervisorRepository
    {
        private List<PersonalSupervisor> personalSupervisors;
        private JsonDataStorage dataStorage;

        public PersonalSupervisorRepository(string filePath)
        {
            dataStorage = new JsonDataStorage(filePath);
            personalSupervisors = dataStorage.LoadData<List<PersonalSupervisor>>() ?? new List<PersonalSupervisor>();
        }

        public IEnumerable<PersonalSupervisor> GetAllPersonalSupervisors()
        {
            return personalSupervisors;
        }

        public PersonalSupervisor GetPersonalSupervisorById(int id)
        {
            return personalSupervisors.Find(ps => ps.Id == id) ?? throw new KeyNotFoundException($"Senior Tutor with ID {id} not found.");
        }
        
        public void AddPersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            personalSupervisors.Add(personalSupervisor);
            dataStorage.SaveData(personalSupervisors);
        }

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
