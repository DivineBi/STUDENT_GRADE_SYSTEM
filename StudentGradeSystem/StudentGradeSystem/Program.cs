class StudentGradeSystem
{
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
        string name;
        while (true)
        {
            Console.Write("Enter student name: ");
            name = (Console.ReadLine()?.Trim()) ?? string.Empty;

            // Data integrity: Check for empty or too long input
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty. Please try again.");
                continue;
            }
            if (name.Length > 50)
            {
                Console.WriteLine("Name is too long. Maximum 50 characters allowed.");
                continue;
            }

            // Security: Allow only letters, spaces, hyphens, and apostrophes
            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z\s\-']+$"))
            {
                Console.WriteLine("Name contains invalid charaters. Only letters, spaces, hyphens, and apostrophes are allowed.");
                continue;
            }

            // Name is valid
            break;
        }
        Console.WriteLine($"Student name '{name}' has been added successfully.");
    }

    static void AddStudentId()
    {
        studentCounter++;
        string yearPart = DateTime.Now.Year.ToString().Substring(2, 2); // Get last two digits of the current year
        string id = $"{yearPart}{studentCounter.ToString("D4")}"; // Format student ID as YYNNNNN
        Console.WriteLine($"Assigned Student ID: {id}");
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
