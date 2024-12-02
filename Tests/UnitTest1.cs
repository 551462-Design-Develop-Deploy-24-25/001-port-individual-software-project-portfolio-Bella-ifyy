//unittests.cs
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;
using EngagementTrackingSystem.Service;

namespace Tests
{
    [TestFixture]
    public class Test
    {
        private const string StudentFilePath = "students.json";
        private const string MeetingFilePath = "meetings.json";
        private const string PersonalSupervisorFilePath = "personalSupervisors.json";
        private const string SeniorTutorFilePath = "seniorTutors.json";

        [SetUp]
        public void SetUp()
        {
            // Clear test files before each test run
            System.IO.File.WriteAllText(StudentFilePath, "[]");
            System.IO.File.WriteAllText(PersonalSupervisorFilePath, "[]");
            System.IO.File.WriteAllText(SeniorTutorFilePath, "[]");
            Console.WriteLine($"Clearing file: {MeetingFilePath}");
            System.IO.File.WriteAllText(MeetingFilePath, "[]");
            Console.WriteLine("File cleared.");

        }


        [Test]
        public void TestStudentSelfReporting()
        {
            // Arrange
            var studentRepository = new StudentRepository(StudentFilePath);
            var personalSupervisorRepository = new PersonalSupervisorRepository(PersonalSupervisorFilePath);
            var studentService = new StudentService(studentRepository, personalSupervisorRepository);

            var student = new Student { Id = 2000, Name = "Ify", Email = "ify@gmail.com", StatusReport = "" };
            studentService.AddStudent(student);

            // Act
            studentService.ReportStatus(2000, "Feeling good!");

            // Assert
            var updatedStudent = studentService.GetStudentById(2000);
            Assert.That(updatedStudent.StatusReport, Is.EqualTo("Feeling good!"));
        }

        [Test]
        public void TestAddStudent()
        {
            // Arrange
            var studentRepository = new StudentRepository(StudentFilePath);
            var studentService = new StudentService(studentRepository, new PersonalSupervisorRepository(PersonalSupervisorFilePath));

            var student = new Student { Id = 3001, Name = "John Doe", Email = "john.doe@gmail.com" };

            // Act
            studentService.AddStudent(student);

            // Assert
            var fetchedStudent = studentService.GetStudentById(3001);
            Assert.That(fetchedStudent, Is.Not.Null);
            Assert.That(fetchedStudent.Name, Is.EqualTo("John Doe"));
        }

        [Test]
        public void TestAddPersonalSupervisor()
        {
            // Arrange
            var personalSupervisorRepository = new PersonalSupervisorRepository(PersonalSupervisorFilePath);
            var personalSupervisorService = new PersonalSupervisorService(personalSupervisorRepository);

            var supervisor = new PersonalSupervisor { Id = 101, Name = "Dr. Jane", Email = "jane@gmail.com" };

            // Act
            personalSupervisorService.AddPersonalSupervisor(supervisor);

            // Assert
            var fetchedSupervisor = personalSupervisorService.GetPersonalSupervisorById(101);
            Assert.That(fetchedSupervisor, Is.Not.Null);
            Assert.That(fetchedSupervisor.Name, Is.EqualTo("Dr. Jane"));
        }

        [Test]
        public void TestAddSeniorTutor()
        {
            // Arrange
            var seniorTutorRepository = new SeniorTutorRepository(SeniorTutorFilePath);
            var seniorTutorService = new SeniorTutorService(seniorTutorRepository);

            var seniorTutor = new SeniorTutor { Id = 10, Name = "Prof. Alan", Email = "alan@gmail.com" };

            // Act
            seniorTutorService.AddSeniorTutor(seniorTutor);

            // Assert
            var fetchedSeniorTutor = seniorTutorService.GetSeniorTutorById(10);
            Assert.That(fetchedSeniorTutor, Is.Not.Null);
            Assert.That(fetchedSeniorTutor.Name, Is.EqualTo("Prof. Alan"));
        }

        [Test]
        public void TestBookingMeeting()
        {
            // Arrange
            var studentRepository = new StudentRepository(StudentFilePath);
            var personalSupervisorRepository = new PersonalSupervisorRepository(PersonalSupervisorFilePath);
            var meetingRepository = new MeetingRepository(MeetingFilePath);
            var meetingService = new MeetingService(meetingRepository, personalSupervisorRepository, studentRepository);

            var student = new Student { Id = 2000, Name = "Ify", Email = "ify@gmail.com" };
            var supervisor = new PersonalSupervisor { Id = 100, Name = "Dr. Smith", Email = "smith@gmail.com" };

            studentRepository.AddStudent(student);
            personalSupervisorRepository.AddPersonalSupervisor(supervisor);

            // Act
            meetingService.ScheduleMeeting(2000, 100, DateTime.Now.AddDays(1));

            // Assert
            var meetings = meetingService.GetAllMeetings();
            //Assert.That(meetings.Count(), Is.EqualTo(1)); // Check count
            //Assert.That(meetings.First().Id, Is.EqualTo(1)); // Check ID explicitly
            Assert.That(meetings.First().StudentId, Is.EqualTo(2000)); // Check StudentId
            Assert.That(meetings.First().PersonalSupervisorId, Is.EqualTo(100)); // Check SupervisorId
        }
        

        [Test]
        public void TestViewStudentsAsPersonalSupervisor()
        {
            // Arrange
            var studentRepository = new StudentRepository(StudentFilePath);
            var personalSupervisorRepository = new PersonalSupervisorRepository(PersonalSupervisorFilePath);
            var personalSupervisorService = new PersonalSupervisorService(personalSupervisorRepository);

            var student = new Student { Id = 2000, Name = "Ify", Email = "ify@gmail.com" };
            var supervisor = new PersonalSupervisor { Id = 100, Name = "Dr. Smith", Email = "smith@gmail.com" };

            studentRepository.AddStudent(student);
            personalSupervisorRepository.AddPersonalSupervisor(supervisor);

            // Act
            var fetchedSupervisor = personalSupervisorRepository.GetPersonalSupervisorById(100);
            fetchedSupervisor.Students.Add(student); // Dynamically associate the student in memory
            var students = personalSupervisorService.GetStudentsBySupervisorId(100);

            // Assert
            Assert.That(students.Count(), Is.EqualTo(1));
            Assert.That(students.First().Name, Is.EqualTo("Ify"));
        }
    }
}
