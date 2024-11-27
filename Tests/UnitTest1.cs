//unittests.cs
using System;
using System.Linq;
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;
using EngagementTrackingSystem.Service;

namespace Tests
{
    [TestFixture]
    public class Test
    {
        private const string StudentFilePath = "test-students.json";
        private const string MeetingFilePath = "test-meetings.json";
        private const string PersonalSupervisorFilePath = "test-supervisors.json";
        private const string SeniorTutorFilePath = "test-tutors.json";

        [SetUp]
        public void SetUp()
        {
            // Clear test files before each test run
            System.IO.File.WriteAllText(StudentFilePath, "[]");
            System.IO.File.WriteAllText(MeetingFilePath, "[]");
            System.IO.File.WriteAllText(PersonalSupervisorFilePath, "[]");
            System.IO.File.WriteAllText(SeniorTutorFilePath, "[]");
        }

        [Test]
        public void TestStudentSelfReporting()
        {
            // Arrange
            var studentService = new StudentService(new StudentRepository(StudentFilePath));
            var student = new Student { Id = 9999, Name = "John Doe", Email = "john.doe@example.com" };

            // Act
            studentService.AddStudent(student);
            var fetchedStudent = studentService.GetStudentById(9999);

            // Assert
            Assert.That(fetchedStudent, Is.Not.Null);
            Assert.That(fetchedStudent.Name, Is.EqualTo("John Doe"));
            Assert.That(fetchedStudent.Email, Is.EqualTo("john.doe@example.com"));
        }


        [Test]
        public void TestAddMultipleStudents()
        {
            // Arrange
            var studentService = new StudentService(new StudentRepository(StudentFilePath));

            var student1 = new Student { Id = 1, Name = "Emily", Email = "emily@example.com" };
            var student2 = new Student { Id = 2, Name = "Allen", Email = "allen@example.com" };

            // Act
            studentService.AddStudent(student1);
            studentService.AddStudent(student2);

            var allStudents = studentService.GetAllStudents().ToList();

            // Assert
            Assert.That(allStudents.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestScheduleMeeting()
        {
            // Arrange
            var meetingService = new MeetingService(new MeetingRepository(MeetingFilePath));
            var date = new DateTime(2024, 12, 20); // A test date

            // Act
            meetingService.ScheduleMeeting(1, 1, date);
            var meetings = meetingService.GetAllMeetings().ToList();

            // Assert
            Assert.That(meetings.Count, Is.EqualTo(1));
            Assert.That(meetings[0].StudentId, Is.EqualTo(1));
            Assert.That(meetings[0].Date.Hour, Is.EqualTo(12)); // Check the hour is set to 12 PM
            Assert.That(meetings[0].Date.Minute, Is.EqualTo(0)); // Check the minute is 0
        }

        [Test]
        public void TestAddPersonalSupervisor()
        {
            // Arrange
            var psService = new PersonalSupervisorService(new PersonalSupervisorRepository(PersonalSupervisorFilePath));
            var supervisor = new PersonalSupervisor { Id = 1, Name = "Dr. Anita", Email = "dr.anita@example.com" };

            // Act
            psService.AddPersonalSupervisor(supervisor);
            var fetchedSupervisor = psService.GetPersonalSupervisorById(1);

            // Assert
            Assert.That(fetchedSupervisor, Is.Not.Null);
            Assert.That(fetchedSupervisor.Name, Is.EqualTo("Dr. Anita"));
            Assert.That(fetchedSupervisor.Email, Is.EqualTo("dr.anita@example.com"));
        }

        [Test]
        public void TestAddAndViewSeniorTutor()
        {
            // Arrange
            var stService = new SeniorTutorService(new SeniorTutorRepository(SeniorTutorFilePath));
            var seniorTutor = new SeniorTutor { Id = 1, Name = "Prof. Henry", Email = "prof.henry@example.com" };

            // Act
            stService.AddSeniorTutor(seniorTutor);
            var fetchedSeniorTutor = stService.GetSeniorTutorById(1);

            // Assert
            Assert.That(fetchedSeniorTutor, Is.Not.Null);
            Assert.That(fetchedSeniorTutor.Name, Is.EqualTo("Prof. Henry"));
            Assert.That(fetchedSeniorTutor.Email, Is.EqualTo("prof.henry@example.com"));
        }

        [Test]
        public void TestMeetingTimeAlwaysSetTo12PM()
        {
            // Arrange
            var meetingFilePath = "test-meetings.json"; // Use a separate test file
            File.WriteAllText(meetingFilePath, "[]");   // Clear previous test data

            var meetingRepository = new MeetingRepository(meetingFilePath);
            var meetingService = new MeetingService(meetingRepository);
            var testDate = new DateTime(2024, 12, 20); // Test date without a time component

            // Act
            meetingService.ScheduleMeeting(1, 2, testDate);
            var scheduledMeeting = meetingService.GetAllMeetings().FirstOrDefault();

            // Assert
            Assert.That(scheduledMeeting, Is.Not.Null, "No meeting was scheduled.");
            Assert.That(scheduledMeeting.Date.Hour, Is.EqualTo(12), "Meeting hour is not set to 12 PM.");
            Assert.That(scheduledMeeting.Date.Minute, Is.EqualTo(0), "Meeting minute is not set to 0.");
        }
        
        [Test]
        public void TestStudentSerializationOnlyIncludesIdNameEmail()
        {
            // Arrange
            var studentService = new StudentService(new StudentRepository(StudentFilePath));
            var student = new Student { Id = 9999, Name = "John Doe", Email = "john.doe@example.com", StatusReport = "Busy" };
            studentService.AddStudent(student);

            // Act
            var serializedData = System.IO.File.ReadAllText(StudentFilePath);

            // Assert
            Assert.That(serializedData, Does.Contain("\"Id\": 9999"));
            Assert.That(serializedData, Does.Contain("\"Name\": \"John Doe\""));
            Assert.That(serializedData, Does.Contain("\"Email\": \"john.doe@example.com\""));
            Assert.That(serializedData, Does.Not.Contain("StatusReport"));
        }

        [Test]
        public void TestPersonalSupervisorSerializationOnlyIncludesIdNameEmail()
        {
            // Arrange
            var psService = new PersonalSupervisorService(new PersonalSupervisorRepository(PersonalSupervisorFilePath));
            var supervisor = new PersonalSupervisor { Id = 1, Name = "Dr. Smith", Email = "dr.smith@example.com" };

            // Act
            psService.AddPersonalSupervisor(supervisor);
            var serializedData = File.ReadAllText(PersonalSupervisorFilePath);

            // Assert
            Assert.That(serializedData, Does.Contain("\"Id\": 1"));
            Assert.That(serializedData, Does.Contain("\"Name\": \"Dr. Smith\""));
            Assert.That(serializedData, Does.Contain("\"Email\": \"dr.smith@example.com\""));
        }

        [Test]
        public void TestSeniorTutorSerializationOnlyIncludesIdNameEmail()
        {
            // Arrange
            var stService = new SeniorTutorService(new SeniorTutorRepository(SeniorTutorFilePath));
            var tutor = new SeniorTutor { Id = 1, Name = "Prof. Alan", Email = "prof.alan@example.com" };

            // Act
            stService.AddSeniorTutor(tutor);
            var serializedData = File.ReadAllText(SeniorTutorFilePath);

            // Assert
            Assert.That(serializedData, Does.Contain("\"Id\": 1"));
            Assert.That(serializedData, Does.Contain("\"Name\": \"Prof. Alan\""));
            Assert.That(serializedData, Does.Contain("\"Email\": \"prof.alan@example.com\""));
        }
    }
}
