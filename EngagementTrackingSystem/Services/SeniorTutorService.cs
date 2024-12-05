//SeniorTutor.cs
using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;

namespace EngagementTrackingSystem.Service
{
    // Service class for senior tutor-related operations
    public class SeniorTutorService
    {
        private SeniorTutorRepository seniorTutorRepository; // Handles Senior tutor data

        // Constructor accepts a senior tutor repository instance
        public SeniorTutorService(SeniorTutorRepository seniorTutorRepository)
        {
            this.seniorTutorRepository = seniorTutorRepository;
        }

        // Retrieves all senior tutors
        public IEnumerable<SeniorTutor> GetAllSeniorTutors()
        {
            return seniorTutorRepository.GetAllSeniorTutors();
        }

        // Retrieves a senior tutor by ID
        public SeniorTutor GetSeniorTutorById(int id)
        {
            return seniorTutorRepository.GetSeniorTutorById(id);
        }

        // Adds a new senior tutor
        public void AddSeniorTutor(SeniorTutor seniorTutor)
        {
            seniorTutorRepository.AddSeniorTutor(seniorTutor);
        }

        // Updates an existing senior tutor
        public void UpdateSeniorTutor(SeniorTutor seniorTutor)
        {
            seniorTutorRepository.UpdateSeniorTutor(seniorTutor);
        }

        // Deletes a senior tutor by ID
        public void DeleteSeniorTutor(int id)
        {
            seniorTutorRepository.DeleteSeniorTutor(id);
        }
    }
}
