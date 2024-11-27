//studentService.cs
// Using directives
using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;

namespace EngagementTrackingSystem.Service
{
    // Service class for student-related operations
    public class StudentService
    {
        private StudentRepository studentRepository;

        // Constructor accepts a student repository instance
        public StudentService(StudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
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

        // Updates an existing student
        public void UpdateStudent(Student student)
        {
            studentRepository.UpdateStudent(student);
        }

        // Deletes a student by ID
        public void DeleteStudent(int id)
        {
            studentRepository.DeleteStudent(id);
        }

        // Allows a student to report their current status
        public void ReportStatus(int studentId, string status)
        {
            var student = studentRepository.GetStudentById(studentId);
            if (student != null)
            {
                student.StatusReport = status;
                studentRepository.UpdateStudent(student);
            }
        }
    }
}
