using System.Collections.Generic;
using UnityEngine;

public class AssignmentManager : MonoBehaviour
{
    public CalendarUI calendarUI;
    private List<Assignment> assignments = new List<Assignment>();

    public void AddAssignment(Assignment assignment)
    {
        assignments.Add(assignment);
        PlanAssignment(assignment);
        calendarUI.UpdateCalendar();
    }

    private void PlanAssignment(Assignment assignment)
    {
        int currentDay = System.DateTime.Now.Day;
        int totalDays = assignment.Deadline - currentDay + 1;
        int hoursPerDay = Mathf.CeilToInt((float)assignment.Hours / totalDays);
        int hoursLeft = assignment.Hours;

        for (int day = currentDay; day <= assignment.Deadline && hoursLeft > 0; day++)
        {
            if (!assignment.NonAvailableDays[day] && hoursLeft > 0)
            {
                int hoursToAllocate = Mathf.Min(hoursPerDay, hoursLeft);
                calendarUI.AddAssignmentToDay(day, assignment, hoursToAllocate);
                hoursLeft -= hoursToAllocate;
            }
        }
    }
}
