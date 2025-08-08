class StudentGradeSystem
{
    static string studentName = "";
    static string studentId = "";
    static int studentCounter = 0; // Counter to generate unique student IDs
    static readonly string[] subjects = ["Maths", "English", "Physics", "Chemistry", "History", "Geography", "IT", "Arts", "PE", "Accounting"];

    static void Main(string[] args)
    {
        Console.WriteLine("Student Grade System");
        Console.WriteLine("Student details");
        AddStudentName();
        AddStudentId();
        EnterSubjectsAndMarks();
    }

    static void AddStudentName()
    {
        while (true)
        {
            Console.Write("Enter student name: ");
            studentName = (Console.ReadLine()?.Trim()) ?? string.Empty;

            // Data integrity: Check for empty or too long input
            if (string.IsNullOrWhiteSpace(studentName))
            {
                Console.WriteLine("Name cannot be empty. Please try again.");
                continue;
            }
            if (studentName.Length > 50)
            {
                Console.WriteLine("Name is too long. Maximum 50 characters allowed.");
                continue;
            }

            // Security: Allow only letters, spaces, hyphens, and apostrophes
            if (!System.Text.RegularExpressions.Regex.IsMatch(studentName, @"^[a-zA-Z\s\-']+$"))
            {
                Console.WriteLine("Name contains invalid charaters. Only letters, spaces, hyphens, and apostrophes are allowed.");
                continue;
            }

            // Name is valid
            break;
        }
        Console.WriteLine($"Student name '{studentName}' has been added successfully.");
    }

    static void AddStudentId()
    {
        studentCounter++;
        string yearPart = DateTime.Now.Year.ToString().Substring(2, 2); // Get last two digits of the current year
        studentId  = $"{yearPart}{studentCounter.ToString("D4")}"; // Format student ID as YYNNNNN
        Console.WriteLine($"Assigned Student ID: {studentId}");
    }

    static void EnterSubjectsAndMarks()
    {
        var selectedSubjects = new List<string>();
        var marksDict = new Dictionary<string, int>();

        bool addingSubjects = true;
        Console.WriteLine("\nAvailable subjects:");
        for (int i = 0; i < subjects.Length; i++)
        {
            if (!selectedSubjects.Contains(subjects[i]))
                Console.WriteLine($"{i + 1}. {subjects[i]}");
        }
        while (addingSubjects)
        {


            Console.Write("Select a subject by number (or type 0 to finish): ");
            if (!int.TryParse(Console.ReadLine(), out int subjectChoice) || subjectChoice < 0 || subjectChoice > subjects.Length)
            {
                Console.WriteLine("Invalid choice. Try again.");
                continue;
            }
            if (subjectChoice == 0)
                break;

            string chosenSubject = subjects[subjectChoice - 1];
            if (selectedSubjects.Contains(chosenSubject))
            {
                Console.WriteLine("Subject already selected.");
                continue;
            }

            int marks;
            while (true)
            {
                Console.Write($"Enter marks for {chosenSubject} (0-100):");
                if (int.TryParse(Console.ReadLine(), out marks) && marks >= 0 && marks <= 100)
                    break;
                Console.WriteLine("Invalid marks. Enter a number between 0 and 100.");
            }

            selectedSubjects.Add(chosenSubject);
            marksDict[chosenSubject] = marks;

            Console.Write("Add another subject? (y/n):");
            string addMore = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
            if (addMore != "y")
                addingSubjects = false;
        }

        // Option to remove subjects
        bool removing = true;
        while (removing && selectedSubjects.Count > 0)
        {
            Console.WriteLine("\nCurrent subjects and marks:");
            for (int i = 0; i < selectedSubjects.Count; i++)
            {
                string subj = selectedSubjects[i];
                int mark = marksDict[subj];
                string grade = GetGrade(mark);
                Console.WriteLine($"{i + 1}. {subj} - {mark} - Grade: {grade}");
            }
            Console.Write("Enter the number of the subject to remove (or 0 to finish): ");
            if (!int.TryParse(Console.ReadLine(), out int removeChoice) || removeChoice < 0 || removeChoice > selectedSubjects.Count)
            {
                Console.WriteLine("Invalid choice.");
                continue;
            }
            if (removeChoice == 0)
                break;
            string toRemove = selectedSubjects[removeChoice - 1];
            selectedSubjects.RemoveAt(removeChoice - 1);
            marksDict.Remove(toRemove);
        }

        // Calculate average marks
        if (selectedSubjects.Count > 0)
        {
            double averageMarks = marksDict.Values.Average();
            string averageGrade = GetGrade((int)Math.Round(averageMarks));

            // Display report
            Console.WriteLine("\n------------ Student Report ------------");
            Console.WriteLine($"Student Name : {studentName}");
            Console.WriteLine($"Student ID   : {studentId}");
            Console.WriteLine("Subjects - Marks - Grade");
            foreach (var subj in selectedSubjects)
            {
                int mark = marksDict[subj];
                string grade = GetGrade(mark);
                Console.WriteLine($"{subj} - {mark} - {grade}");
            }
            Console.WriteLine($"Average Marks: {averageMarks:F2}");
            Console.WriteLine($"Average Grade: {averageGrade}");
        }
        else
        {
            Console.WriteLine("No subjects entered.");
        }

    }

    static string GetGrade(int marks)
    {
        return marks switch
        {
            >= 80 and <= 100 => "A",
            >= 70 and < 80 => "B",
            >= 60 and < 70 => "C",
            >= 50 and < 60 => "D",
            >= 40 and < 50 => "E",
            >= 0 and < 40 => "F",
            _ => "Invalid marks"
        };
    }
}
