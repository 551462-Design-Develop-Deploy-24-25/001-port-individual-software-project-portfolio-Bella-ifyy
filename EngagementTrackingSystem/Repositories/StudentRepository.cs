//StudentRepository.cs
using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    // Handles data access and management for students
    public class StudentRepository
    {
        private List<Student> students; // In-memory list of students
        private JsonDataStorage dataStorage; // Utility for data persistence

        // Constructor initializes repository with file storage
        public StudentRepository(string filePath)
        {
            dataStorage = new JsonDataStorage(filePath);
            students = dataStorage.LoadData<List<Student>>() ?? new List<Student>();
        }

        // Retrieves all students
        public IEnumerable<Student> GetAllStudents()
        {
            return students;
        }

        // Retrieves all students by their id
        public Student GetStudentById(int id)
        {
            return students.Find(s => s.Id == id) ?? throw new KeyNotFoundException($"Student with ID {id} not found.");
        }

        //  Adds a new student and saves to storage
        public void AddStudent(Student student)
        {
            students.Add(student);
            dataStorage.SaveData(students);
        }

        // Updates an existing student
        public void UpdateStudent(Student student)
        {
            var existingStudent = students.Find(s => s.Id == student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name ?? existingStudent.Name;
                existingStudent.StatusReport = student.StatusReport ?? existingStudent.StatusReport;
                existingStudent.Meetings = student.Meetings ?? existingStudent.Meetings;
                dataStorage.SaveData(students);
            }
        }
    }
}
