﻿// Program.cs
using System;
using System.Text.RegularExpressions;
using EngagementTrackingSystem.Models;
using EngagementTrackingSystem.Repositories;
using EngagementTrackingSystem.Service;

class Program
{
    static void Main(string[] args)
    {
        // Initialize repositories with file paths for data storage
        var studentRepository = new StudentRepository("JsonData/students.json");
        var personalSupervisorRepository = new PersonalSupervisorRepository("JsonData/personalSupervisors.json");
        var seniorTutorRepository = new SeniorTutorRepository("JsonData/seniorTutors.json");
        var meetingRepository = new MeetingRepository("JsonData/meetings.json");

        // Initialize services
        var studentService = new StudentService(studentRepository, personalSupervisorRepository);
        var personalSupervisorService = new PersonalSupervisorService(personalSupervisorRepository);
        var seniorTutorService = new SeniorTutorService(seniorTutorRepository);
        var meetingService = new MeetingService(meetingRepository, personalSupervisorRepository, studentRepository);
     
        bool running = true; // Controls the main menu loop

        while (running)
        {
            Console.Clear(); // Clear the console for better readability
            Console.WriteLine("Welcome to your Engagement Tracking System!");
            Console.WriteLine("1. Login as a Student");
            Console.WriteLine("2. Login as a Personal Supervisor");
            Console.WriteLine("3. Login as a Senior Tutor");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine() ?? string.Empty; // Read user input

            // Handle user choice
            switch (choice)
            {
                case "1":
                    var student = AddStudent(studentService); // Get the logged-in student
                    if (student != null)
                    {
                        StudentMenu(studentService, meetingService, student);
                    }
                    break;
                case "2":
                    var supervisor = AddPersonalSupervisor(personalSupervisorService); // Get the logged-in supervisor
                    if (supervisor != null)
                    {
                        PersonalSupervisorMenu(personalSupervisorService, meetingService, studentService, supervisor);
                    }
                    break;
                case "3":
                    if (AddSeniorTutor(seniorTutorService)) // Handle senior tutor login
                    {
                        SeniorTutorMenu(seniorTutorService, studentService, meetingService);
                    }
                    break;
                case "4":
                    running = false; // Exit the application
                    Console.WriteLine("Exiting the program...");
                    Pause();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Pause();
                    break;
            }
        }
    }

    // Validation for IDs
    static bool ValidateId(int id, int min, int max, string idType)
    {
        if (id < min || id > max)
        {
            Console.WriteLine($"Error: {idType} ID must be a number between {min} and {max}.");
            return false;
        }
        return true;
    }
     
    // Validation for Emails
    static bool ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine("Error: Email cannot be empty.");
            return false; // Always return false for invalid cases
        }

        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern); // Match or return false
    }

    // Validation for Dates
    static bool ValidateFutureDate(string dateInput, out DateTime date)
    {
        date = default;

        // Check format and parse date
        if (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out date))
        {
            Console.WriteLine("Error: Invalid date format. Please use the format yyyy-MM-dd.");
            return false;
        }

        // Ensure the date is in the future
        if (date <= DateTime.Now)
        {
            Console.WriteLine("Error: The date must be in the future.");
            return false;
        }

        return true;
    }

    // Helper method to pause the console
    static void Pause()
    {
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    // Add a Student and return the logged-in student object
    static Student AddStudent(StudentService studentService)
    {
        while (true)
        {
            try
            {
                Console.Write("Enter your four-digit Student ID: ");
                var input = Console.ReadLine() ?? string.Empty;

                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine("Invalid input. Please enter a valid four-digit number.");
                    continue; // Retry
                }

                if (!ValidateId(id, 1000, 9999, "Student"))
                {
                    continue; // Retry
                }

                Console.Write("Enter your Name: ");
                string name = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    Pause();
                    return null;
                }

                Console.Write("Enter your Email: ");
                string email = Console.ReadLine() ?? string.Empty;
                if (!ValidateEmail(email))
                {
                    continue; // Retry
                }

                var student = new Student { Id = id, Name = name, Email = email, StatusReport = "N/A" };
                studentService.AddStudent(student);

                Console.WriteLine("Student logged in successfully.");
                Pause();
                return student;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Pause();
            }
        }
    }


    // Add a Personal Supervisor
    static PersonalSupervisor AddPersonalSupervisor(PersonalSupervisorService personalSupervisorService)
    {
        while (true)
        {
            try
            {
                Console.Write("Enter your three-digit Supervisor ID: ");
                var input = Console.ReadLine() ?? string.Empty;

                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine("Invalid input. Please enter a valid three-digit number.");
                    continue;
                }

                if (!ValidateId(id, 100, 999, "Personal Supervisor"))
                {
                    continue;
                }

                Console.Write("Enter your Name: ");
                string name = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    Pause();
                    return null;
                }

                Console.Write("Enter your Email: ");
                string email = Console.ReadLine() ?? string.Empty;
                if (!ValidateEmail(email))
                {
                    continue;
                }

                var supervisor = new PersonalSupervisor { Id = id, Name = name, Email = email };
                personalSupervisorService.AddPersonalSupervisor(supervisor);

                Console.WriteLine("Supervisor added successfully.");
                Pause();
                return supervisor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Pause();
            }
        }
    }

    // Add a Senior Tutor
    static bool AddSeniorTutor(SeniorTutorService seniorTutorService)
    {
        try
        {
            Console.Write("Enter your two-digit Senior Tutor ID: ");
            var input = Console.ReadLine() ?? string.Empty;

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("Invalid ID. Please try again.");
                Pause();
                return false;
            }

            if (!ValidateId(id, 10, 99, "Senior Tutor"))
            {
                Pause();
                return false;
            }

            Console.Write("Enter your Name: ");
            string name = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                Pause();
                return false;
            }

            Console.Write("Enter your Email: ");
            string email = Console.ReadLine() ?? string.Empty;
            if (!ValidateEmail(email))
            {
                Pause();
                return false;
            }

            var seniorTutor = new SeniorTutor { Id = id, Name = name, Email = email };
            seniorTutorService.AddSeniorTutor(seniorTutor);

            Console.WriteLine("Senior Tutor added successfully.");
            Pause();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Pause();
            return false;
        }
    }


    // Student Menu with logged-in student
    static void StudentMenu(StudentService studentService, MeetingService meetingService, Student loggedInStudent)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine($"Welcome, {loggedInStudent.Name}! (ID: {loggedInStudent.Id})");
            Console.WriteLine("1. Report your Status");
            Console.WriteLine("2. Schedule a Meeting");
            Console.WriteLine("3. Log out");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ReportStudentStatus(studentService, loggedInStudent);
                    break;
                case "2":
                    ScheduleMeeting(meetingService, loggedInStudent);
                    break;
                case "3":
                    running = false;
                    Console.WriteLine("Logging out...");
                    Pause();
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    Pause();
                    break;
            }
        }
    }

    // Personal Supervisor Menu with logged-in supervisor
    static void PersonalSupervisorMenu(PersonalSupervisorService personalSupervisorService, MeetingService meetingService, StudentService studentService, PersonalSupervisor loggedInSupervisor)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine($"Welcome, {loggedInSupervisor.Name}! (ID: {loggedInSupervisor.Id})");
            Console.WriteLine("1. Schedule a Meeting");
            Console.WriteLine("2. View All Students' Status");
            Console.WriteLine("3. Log out");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ScheduleMeetingAsSupervisor(meetingService, loggedInSupervisor);
                    break;
                case "2":
                    ViewAllStudents(studentService);
                    break;
                case "3":
                    running = false;
                    Console.WriteLine("Logging out...");
                    Pause();
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    Pause();
                    break;
            }
        }
    }
    // Senior tutor menu for various actions
    static void SeniorTutorMenu(SeniorTutorService seniorTutorService, StudentService studentService, MeetingService meetingService)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine($"Welcome to your Portal!");
            Console.WriteLine("Senior Tutor Menu:");
            Console.WriteLine("1. View All Students");
            Console.WriteLine("2. View All Scheduled Meetings");
            Console.WriteLine("3. Log out");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewAllStudents(studentService);
                    break;
                case "2":
                    ViewAllMeetings(meetingService);
                    break;
                case "3":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    Console.ReadLine(); // Pause for invalid input
                    break;
            }
        }
    }

    // Schedule Meeting for Supervisor
    static void ScheduleMeetingAsSupervisor(MeetingService meetingService, PersonalSupervisor loggedInSupervisor)
    {
        Console.Write("Enter Student ID: ");
        var input = Console.ReadLine() ?? string.Empty;

        if (!int.TryParse(input, out int studentId))
        {
            Console.WriteLine("Invalid Student ID. Please try again.");
            Pause();
            return;
        }

        Console.Write("Enter Meeting Date (yyyy-mm-dd): ");
        var dateInput = Console.ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(dateInput) || !ValidateFutureDate(dateInput, out DateTime date))
        {
            Console.WriteLine("Invalid date. Please try again.");
            Pause();
            return;
        }

        meetingService.ScheduleMeeting(studentId, loggedInSupervisor.Id, date);
        Console.WriteLine("Done.");
        Pause();
    }

    // Method for a student to report their status
    static void ReportStudentStatus(StudentService studentService, Student loggedInStudent)
    {
        Console.Write("Enter your Status Report: ");
        string status = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(status))
        {
            Console.WriteLine("Status cannot be empty.");
            return;
        }

        // Update the status in memory and save it
        loggedInStudent.StatusReport = status;
        studentService.UpdateStudent(loggedInStudent);

        Console.WriteLine($"Status updated successfully for {loggedInStudent.Name}");
        Pause();
    }

    // Method to schedule a meeting
    static void ScheduleMeeting(MeetingService meetingService, Student loggedInStudent)
    {
        Console.Write("Enter Personal Supervisor ID: ");
        var input = Console.ReadLine() ?? string.Empty;

        if (!int.TryParse(input, out int supervisorId))
        {
            Console.WriteLine("Invalid supervisor ID. Please try again.");
            Pause();
            return;
        }

        Console.Write("Enter Meeting Date (yyyy-mm-dd): ");
        var dateInput = Console.ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(dateInput) || !ValidateFutureDate(dateInput, out DateTime date))
        {
            Console.WriteLine("Invalid date. Please enter a valid future date.");
            Pause();
            return;
        }

        meetingService.ScheduleMeeting(loggedInStudent.Id, supervisorId, date);
        Console.WriteLine("Meeting scheduled successfully.");
        Pause();
    }


    // Method to view all registered students
    static void ViewAllStudents(StudentService studentService)
    {
        var students = studentService.GetAllStudents();
        if (!students.Any())
        {
            Console.WriteLine("No students logged in today.");
            Console.ReadLine(); // Pause to let user see the message
            return;
        }

        Console.WriteLine("All Registered Students:");
        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Email: {student.Email}, Status: {student.StatusReport}");
        }
        Console.ReadLine(); // Pause to let user review the list
    }

    // Method to view all scheduled meetings
    static void ViewAllMeetings(MeetingService meetingService)
    {
        var meetings = meetingService.GetAllMeetings();
        if (!meetings.Any())
        {
            Console.WriteLine("No meetings scheduled for today.");
            Console.ReadLine(); // Pause to let user see the message
            return;
        }

        Console.WriteLine("All Scheduled Meetings:");
        foreach (var meeting in meetings)
        {
            Console.WriteLine($"Meeting ID: {meeting.Id}, Date: {meeting.Date}, Student ID: {meeting.StudentId}, Supervisor ID: {meeting.PersonalSupervisorId}");
        }
        Console.ReadLine(); // Pause to let user see the message
    }
}
