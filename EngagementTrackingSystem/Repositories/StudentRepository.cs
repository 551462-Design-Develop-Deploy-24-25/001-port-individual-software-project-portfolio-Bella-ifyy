using System;
using System.Collections.Generic;
using EngagementTrackingSystem.Models;

namespace EngagementTrackingSystem.Repositories
{
    public class StudentRepository
    {
        private List<Student> students;
        private JsonDataStorage dataStorage;

        public StudentRepository(string filePath)
        {
            dataStorage = new JsonDataStorage(filePath);
            students = dataStorage.LoadData<List<Student>>() ?? new List<Student>();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return students;
        }

        public Student GetStudentById(int id)
        {
            return students.Find(s => s.Id == id) ?? throw new KeyNotFoundException($"Student with ID {id} not found.");
        }


        public void AddStudent(Student student)
        {
            students.Add(student);
            dataStorage.SaveData(students);
        }

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
