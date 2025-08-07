class StudentGradeSystem
{
    static int studentCounter = 0; // Counter to generate unique student IDs
    static void Main(string[] args)
    {
        Console.WriteLine("Student Grade System");
        Console.WriteLine("Student details");
        AddStudentName();
        AddStudentId();
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
}
