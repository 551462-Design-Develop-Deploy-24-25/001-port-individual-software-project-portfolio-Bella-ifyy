using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    public class StudentRepository
    {
        private List<Student> students; // In-memory list of students
        private JsonDataStorage dataStorage; // Data storage utility

        public StudentRepository(string filePath)
        {
            dataStorage = new JsonDataStorage(filePath);

            // Ensure the data is never null
            students = dataStorage.LoadData<List<Student>>() ?? throw new InvalidOperationException("Student data is missing or invalid.");
        }

        // Retrieves all students
        public IEnumerable<Student> GetAllStudents()
        {
            return students; // Safe because students is initialized in the constructor
        }

        // Retrieves a student by ID
        public Student GetStudentById(int id)
        {
            return students.Find(s => s.Id == id) ?? throw new KeyNotFoundException($"Student with ID {id} not found.");
        }

        // Adds a new student and saves data
        public void AddStudent(Student student)
        {
            students.Add(student);
            dataStorage.SaveData(students); // Save updated data
        }

        // Updates an existing student and saves data
        public void UpdateStudent(Student student)
        {
            var existingStudent = GetStudentById(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
                existingStudent.StatusReport = student.StatusReport;
                dataStorage.SaveData(students); // Save updated data
            }
        }

        // Deletes a student by ID
        public void DeleteStudent(int id)
        {
            var student = GetStudentById(id);
            if (student != null)
            {
                students.Remove(student);
                dataStorage.SaveData(students); // Save updated data
            }
        }
    }
}
