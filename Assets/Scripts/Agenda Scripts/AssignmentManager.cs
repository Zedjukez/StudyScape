using System.Collections.Generic;
using UnityEngine;

public class AssignmentManager : MonoBehaviour
{
    public CalendarUI calendarUI;
    public List<Assignment> assignments = new List<Assignment>();
    public UIManager uiManager;
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
            int hoursToAllocate = Mathf.Min(hoursPerDay, hoursLeft);
            calendarUI.AddAssignmentToDay(day, assignment, hoursToAllocate);
            hoursLeft -= hoursToAllocate;
        }
    }

    public void RemoveAssignment(Assignment assignment)
    {
        assignments.Remove(assignment);
        calendarUI.UpdateCalendar();
    }

    public List<Assignment> GetAssignments()
    {
        return new List<Assignment>(assignments);
    }

    public void MoveAssignment(int fromDay, int toDay, Assignment assignment)
    {
        if (toDay < System.DateTime.Now.Day || toDay > assignment.Deadline)
        {
            Debug.LogError("Cannot move task to the selected day.");
            return;
        }

        calendarUI.RemoveAssignmentFromDay(fromDay, assignment);
        calendarUI.AddAssignmentToDay(toDay, assignment, assignment.Hours);
        calendarUI.UpdateCalendar();
    }
}
