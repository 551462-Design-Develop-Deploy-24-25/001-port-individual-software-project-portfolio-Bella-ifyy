using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;

namespace EngagementTrackingSystem.Service
{
    public class StudentService
    {
        private StudentRepository studentRepository;

        public StudentService(StudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
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
        }
    }
}
