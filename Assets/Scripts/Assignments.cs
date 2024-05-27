using UnityEngine;

public class Assignment
{
    public string Name { get; set; }
    public string Type { get; set; } // "Exam", "Studying", "School project"
    public int Deadline { get; set; }
    public int Hours { get; set; }
    public bool[] NonAvailableDays { get; set; }

    public Assignment(string name, string type, int deadline, int hours, bool[] nonAvailableDays)
    {
        Name = name;
        Type = type;
        Deadline = deadline;
        Hours = hours;
        NonAvailableDays = nonAvailableDays;
    }
}
