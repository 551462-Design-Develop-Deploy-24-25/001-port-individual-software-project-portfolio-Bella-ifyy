//studentRepository.cs
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    // Repository class for managing students
    public class StudentRepository
    {
        private List<Student> students = new List<Student>(); // In-memory list
        private JsonDataStorage dataStorage; // Data storage utility

        // Constructor initializes data storage and loads existing students
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

        // Retrieves a student by ID
        public Student GetStudentById(int id)
        {
            return students.Find(s => s.Id == id);
        }

        // Adds a new student and saves data
        public void AddStudent(Student student)
        {
            students.Add(student);
            dataStorage.SaveData(students); // Save after adding
        }

        // Updates an existing student and saves data
        public void UpdateStudent(Student student)
        {
            var existingStudent = GetStudentById(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
                existingStudent.StatusReport = student.StatusReport; // Update for self-reporting
                dataStorage.SaveData(students); // Save after updating
            }
        }

        // Deletes a student by ID and saves data
        public void DeleteStudent(int id)
        {
            var student = GetStudentById(id);
            if (student != null)
            {
                students.Remove(student);
                dataStorage.SaveData(students); // Save after deleting
            }
        }
    }
}