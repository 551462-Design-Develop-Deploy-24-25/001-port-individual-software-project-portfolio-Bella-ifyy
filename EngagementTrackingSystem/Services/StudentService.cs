// StudentService.cs
using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;

namespace EngagementTrackingSystem.Service
{
    // Service class for student-related operations
    public class StudentService
    {
        private readonly StudentRepository studentRepository; // Handles Student data
        private readonly PersonalSupervisorRepository personalSupervisorRepository; // Handles Supervisor data

        // Constructor initializes the repository
        public StudentService(StudentRepository studentRepository, PersonalSupervisorRepository personalSupervisorRepository)
        {
            this.studentRepository = studentRepository;
            this.personalSupervisorRepository = personalSupervisorRepository;
        }

        // Retrieves all students
        public IEnumerable<Student> GetAllStudents()
        {
            return studentRepository.GetAllStudents();
        }

        // Retrieves a student by ID
        public Student GetStudentById(int id)
        {
            return studentRepository.GetStudentById(id);
        }

        // Adds a new student
        public void AddStudent(Student student)
        {
            studentRepository.AddStudent(student);
        }

        // Updates details for an existing student
        public void UpdateStudent(Student student)
        {
            studentRepository.UpdateStudent(student);
        }

        // allows for student self report
        public void ReportStatus(int studentId, string status)
        {
            var student = studentRepository.GetStudentById(studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found!");
                return;
            }

            student.StatusReport = status;
            studentRepository.UpdateStudent(student);

            // Link status to supervisor
            foreach (var supervisor in personalSupervisorRepository.GetAllPersonalSupervisors())
            {
                var supervisedStudent = supervisor.Students?.FirstOrDefault(s => s.Id == studentId);
                if (supervisedStudent != null)
                {
                    supervisedStudent.StatusReport = status;
                    personalSupervisorRepository.UpdatePersonalSupervisor(supervisor);
                    break;
                }
            }
        }
    }
}
