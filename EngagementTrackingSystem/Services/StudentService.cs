// Updated StudentService.cs
using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;

namespace EngagementTrackingSystem.Service
{
    public class StudentService
    {
        private readonly StudentRepository studentRepository;
        private readonly PersonalSupervisorRepository personalSupervisorRepository;

        public StudentService(StudentRepository studentRepository, PersonalSupervisorRepository personalSupervisorRepository)
        {
            this.studentRepository = studentRepository;
            this.personalSupervisorRepository = personalSupervisorRepository;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return studentRepository.GetAllStudents();
        }

        public Student GetStudentById(int id)
        {
            return studentRepository.GetStudentById(id);
        }

        public void AddStudent(Student student)
        {
            studentRepository.AddStudent(student);
        }

        public void UpdateStudent(Student student)
        {
            studentRepository.UpdateStudent(student);
        }

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
