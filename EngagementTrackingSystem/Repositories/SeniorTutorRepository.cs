//SeniorTutor.cs
using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    // Handles data access and management for senior tutors
    public class SeniorTutorRepository
    {
        private List<SeniorTutor> seniorTutors; // In-memory list
        private JsonDataStorage dataStorage; // Data storage utility

        // Constructor initializes repository with file storage
        public SeniorTutorRepository(string filePath)
        {
            dataStorage = new JsonDataStorage(filePath);

            // Ensure the data is never null
            seniorTutors = dataStorage.LoadData<List<SeniorTutor>>() ?? throw new InvalidOperationException("Senior Tutor data is missing or invalid.");
        }

        // Retrieves all senior tutors
        public IEnumerable<SeniorTutor> GetAllSeniorTutors()
        {
            return seniorTutors; // Safe because seniorTutors is initialized in the constructor
        }

        // Retrieves a senior tutor by ID
        public SeniorTutor GetSeniorTutorById(int id)
        {
            return seniorTutors.Find(st => st.Id == id) ?? throw new KeyNotFoundException($"Senior Tutor with ID {id} not found.");
        }

        // Adds a new senior tutor and saves data to storage
        public void AddSeniorTutor(SeniorTutor seniorTutor)
        {
            seniorTutors.Add(seniorTutor);
            dataStorage.SaveData(seniorTutors); // Save updated data
        }

        // Updates an existing senior tutor and saves data
        public void UpdateSeniorTutor(SeniorTutor seniorTutor)
        {
            var existingTutor = GetSeniorTutorById(seniorTutor.Id);
            if (existingTutor != null)
            {
                existingTutor.Name = seniorTutor.Name;
                existingTutor.Email = seniorTutor.Email;
                dataStorage.SaveData(seniorTutors); // Save updated data
            }
        }

        // Deletes a senior tutor by ID
        public void DeleteSeniorTutor(int id)
        {
            var tutor = GetSeniorTutorById(id);
            if (tutor != null)
            {
                seniorTutors.Remove(tutor);
                dataStorage.SaveData(seniorTutors); // Save updated data
            }
        }
    }
}
