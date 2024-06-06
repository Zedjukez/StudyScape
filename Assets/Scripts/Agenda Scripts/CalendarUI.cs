using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CalendarUI : MonoBehaviour
{
    public GameObject dayPrefab;
    public Transform calendarParent;
    public AgendaScreen agendaScreen;
    public HighlightCurrentDay highlightCurrentDay;

    private Dictionary<int, List<Assignment>> assignmentsPerDay = new Dictionary<int, List<Assignment>>();

    private void Start()
    {
        PopulateCalendar();
        highlightCurrentDay.HighlightToday();
    }

    private void PopulateCalendar()
    {
        for (int i = 1; i <= 31; i++)
        {
            GameObject dayButton = Instantiate(dayPrefab, calendarParent);
            dayButton.GetComponentInChildren<TMP_Text>().text = i.ToString();
            int day = i;
            dayButton.GetComponent<Button>().onClick.AddListener(() => OnDaySelected(day));

            assignmentsPerDay[day] = new List<Assignment>();
        }
    }

    private void OnDaySelected(int day)
    {
        agendaScreen.ShowDayDetails(day, assignmentsPerDay[day]);
    }

    public void AddAssignmentToDay(int day, Assignment assignment, int hours)
    {
        assignmentsPerDay[day].Add(assignment);
        UpdateDayButton(day);
    }

    public void RemoveAssignmentFromDay(int day, Assignment assignment)
    {
        if (assignmentsPerDay.ContainsKey(day))
        {
            assignmentsPerDay[day].Remove(assignment);
            UpdateDayButton(day);
        }
    }

    public void UpdateCalendar()
    {
        for (int day = 1; day <= 31; day++)
        {
            UpdateDayButton(day);
        }
    }

    private void UpdateDayButton(int day)
    {
        Transform dayButton = calendarParent.GetChild(day - 1);
        DayUI dayUI = dayButton.GetComponent<DayUI>();
        dayUI.UpdateStatusBar(assignmentsPerDay[day]);
    }

    public void MoveAssignment(int fromDay, int toDay, Assignment assignment)
    {
        if (toDay < System.DateTime.Now.Day || toDay > assignment.Deadline)
        {
            Debug.LogError("Cannot move task to the selected day.");
            return;
        }

        RemoveAssignmentFromDay(fromDay, assignment);
        AddAssignmentToDay(toDay, assignment, assignment.HoursPerDay(toDay));
        UpdateCalendar();
    }

    public void MarkSessionAsCompleted(int day, Assignment assignment)
    {
        if (assignmentsPerDay.ContainsKey(day))
        {
            foreach (var assign in assignmentsPerDay[day])
            {
                if (assign == assignment)
                {
                    // Mark the session as completed
                    assign.Hours--; // Assuming each session is 1 hour
                    TMP_Text detailsText = agendaScreen.dayDetailsText;
                    detailsText.text += "\n<color=green>voltooid!</color>";
                }
            }
            UpdateDayButton(day);
        }
    }
}
