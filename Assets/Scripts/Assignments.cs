using UnityEngine;

public class Assignment
{
    public string Name { get; set; }
    public string Type { get; set; } // "Exam", "Studying", "School project"
    public int Deadline { get; set; }
    public int Hours { get; set; }
    public int SessionsCompleted { get; set; } // Track completed sessions
    public int TotalSessions { get; private set; } // Total sessions calculated based on hours and deadline

    public Assignment(string name, string type, int deadline, int hours)
    {
        Name = name;
        Type = type;
        Deadline = deadline;
        Hours = hours;
        SessionsCompleted = 0;
        TotalSessions = Mathf.CeilToInt((float)hours / HoursPerDay(System.DateTime.Now.Day));
    }

    public int HoursPerDay(int day)
    {
        int currentDay = System.DateTime.Now.Day;
        int totalDays = Deadline - currentDay + 1;
        return Mathf.CeilToInt((float)Hours / totalDays);
    }
}
