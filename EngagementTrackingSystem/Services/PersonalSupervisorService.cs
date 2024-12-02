// Updated PersonalSupervisorService.cs
using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;

namespace EngagementTrackingSystem.Service
{
    public class PersonalSupervisorService
    {
        private readonly PersonalSupervisorRepository personalSupervisorRepository;

        public PersonalSupervisorService(PersonalSupervisorRepository personalSupervisorRepository)
        {
            this.personalSupervisorRepository = personalSupervisorRepository;
        }

        public IEnumerable<PersonalSupervisor> GetAllPersonalSupervisors()
        {
            return personalSupervisorRepository.GetAllPersonalSupervisors();
        }

        public IEnumerable<Student> GetStudentsBySupervisorId(int supervisorId)
        {
            var supervisor = personalSupervisorRepository.GetPersonalSupervisorById(supervisorId);
            if (supervisor != null)
            {
                return supervisor.Students;
            }
            return new List<Student>();
        }

        public PersonalSupervisor GetPersonalSupervisorById(int id)
        {
            return personalSupervisorRepository.GetPersonalSupervisorById(id);
        }

        public void AddPersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            personalSupervisorRepository.AddPersonalSupervisor(personalSupervisor);
        }

        public void UpdatePersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            personalSupervisorRepository.UpdatePersonalSupervisor(personalSupervisor);
        }

        public void DeletePersonalSupervisor(int id)
        {
            personalSupervisorRepository.DeletePersonalSupervisor(id);
        }

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
