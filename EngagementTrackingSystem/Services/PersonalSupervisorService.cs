//personalSupervisorService.cs
// Using directives
using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;

namespace EngagementTrackingSystem.Service
{
    // Service class for personal supervisor-related operations
    public class PersonalSupervisorService
    {
        private PersonalSupervisorRepository personalSupervisorRepository;

        // Constructor accepts a personal supervisor repository instance
        public PersonalSupervisorService(PersonalSupervisorRepository personalSupervisorRepository)
        {
            this.personalSupervisorRepository = personalSupervisorRepository;
        }

        // Retrieves all personal supervisors
        public IEnumerable<PersonalSupervisor> GetAllPersonalSupervisors()
        {
            return personalSupervisorRepository.GetAllPersonalSupervisors();
        }

        // Retrieves a personal supervisor by ID
        public PersonalSupervisor GetPersonalSupervisorById(int id)
        {
            return personalSupervisorRepository.GetPersonalSupervisorById(id);
        }

        // Adds a new personal supervisor
        public void AddPersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            personalSupervisorRepository.AddPersonalSupervisor(personalSupervisor);
        }

        // Updates an existing personal supervisor
        public void UpdatePersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            personalSupervisorRepository.UpdatePersonalSupervisor(personalSupervisor);
        }

        // Deletes a personal supervisor by ID
        public void DeletePersonalSupervisor(int id)
        {
            personalSupervisorRepository.DeletePersonalSupervisor(id);
        }

        // Retrieves students supervised by a specific supervisor
        public IEnumerable<Student> GetStudentsBySupervisorId(int supervisorId)
        {
            var supervisor = personalSupervisorRepository.GetPersonalSupervisorById(supervisorId);
            return supervisor?.Students ?? new List<Student>();
        }
    }
}