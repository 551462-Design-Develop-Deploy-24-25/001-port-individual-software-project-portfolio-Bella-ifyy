//PersonalSupervisorService.cs
using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;

namespace EngagementTrackingSystem.Service
{
    // Service class for personal supervisor-related operations
    public class PersonalSupervisorService
    {
        private readonly PersonalSupervisorRepository personalSupervisorRepository; // Handles Supervisor data

        // Constructor initializes the repository
        public PersonalSupervisorService(PersonalSupervisorRepository personalSupervisorRepository)
        {
            this.personalSupervisorRepository = personalSupervisorRepository;
        }

        // Retrieves all personal supervisors
        public IEnumerable<PersonalSupervisor> GetAllPersonalSupervisors()
        {
            return personalSupervisorRepository.GetAllPersonalSupervisors();
        }

        // Retrieves students assigned to a specific supervisor
        public IEnumerable<Student> GetStudentsBySupervisorId(int supervisorId)
        {
            var supervisor = personalSupervisorRepository.GetPersonalSupervisorById(supervisorId);
            if (supervisor != null)
            {
                return supervisor.Students; // Return students linked to this supervisor
            }
            return new List<Student>(); // Return an empty list if no supervisor found
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

        // Updates details for an existing supervisor
        public void UpdatePersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            personalSupervisorRepository.UpdatePersonalSupervisor(personalSupervisor);
        }

        // Deletes a personal supervisor by ID
        public void DeletePersonalSupervisor(int id)
        {
            personalSupervisorRepository.DeletePersonalSupervisor(id);
        }

        // Links a student to a supervisor
        public void LinkStudentToSupervisor(int studentId, int supervisorId)
        {
            var supervisor = personalSupervisorRepository.GetPersonalSupervisorById(supervisorId);
            if (supervisor == null)
            {
                throw new KeyNotFoundException($"Supervisor with ID {supervisorId} not found.");
            }

            var student = supervisor.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                Console.WriteLine($"Error: Student with ID {studentId} not linked to supervisor ID {supervisorId}.");
                return;
            }
        }
    }
}
