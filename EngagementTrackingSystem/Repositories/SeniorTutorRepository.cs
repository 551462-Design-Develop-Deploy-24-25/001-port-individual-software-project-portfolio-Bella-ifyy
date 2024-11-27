//seniorTutorRepository.cs
// Using directives
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    // Repository class for managing senior tutors
    public class SeniorTutorRepository
    {
        private List<SeniorTutor> seniorTutors = new List<SeniorTutor>(); // In-memory list
        private JsonDataStorage dataStorage; // Data storage utility

        // Constructor initializes data storage and loads existing tutors
        public SeniorTutorRepository(string filePath)
        {
            dataStorage = new JsonDataStorage(filePath);
            seniorTutors = dataStorage.LoadData<List<SeniorTutor>>() ?? new List<SeniorTutor>();
        }

        // Retrieves all senior tutors
        public IEnumerable<SeniorTutor> GetAllSeniorTutors()
        {
            return seniorTutors;
        }

        // Retrieves a senior tutor by ID
        public SeniorTutor GetSeniorTutorById(int id)
        {
            return seniorTutors.Find(st => st.Id == id);
        }

        // Adds a new senior tutor and saves data
        public void AddSeniorTutor(SeniorTutor seniorTutor)
        {
            seniorTutors.Add(seniorTutor);
            dataStorage.SaveData(seniorTutors); // Save after adding
        }

        // Updates an existing senior tutor and saves data
        public void UpdateSeniorTutor(SeniorTutor seniorTutor)
        {
            var existingSeniorTutor = GetSeniorTutorById(seniorTutor.Id);
            if (existingSeniorTutor != null)
            {
                existingSeniorTutor.Name = seniorTutor.Name;
                existingSeniorTutor.Email = seniorTutor.Email;
                dataStorage.SaveData(seniorTutors); // Save after updating
            }
        }

        // Deletes a senior tutor by ID and saves data
        public void DeleteSeniorTutor(int id)
        {
            var seniorTutor = GetSeniorTutorById(id);
            if (seniorTutor != null)
            {
                seniorTutors.Remove(seniorTutor);
                dataStorage.SaveData(seniorTutors); // Save after deleting
            }
        }
    }
}