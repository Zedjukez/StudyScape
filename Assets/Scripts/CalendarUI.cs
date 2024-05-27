using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CalendarUI : MonoBehaviour
{
    public GameObject dayPrefab;
    public Transform calendarParent;
    public AgendaScreen agendaScreen;
    public HighlightCurrentDay highlightCurrentDay; // Reference to HighlightCurrentDay script

    private Dictionary<int, List<Assignment>> assignmentsPerDay = new Dictionary<int, List<Assignment>>();

    private void Start()
    {
        PopulateCalendar();
        highlightCurrentDay.HighlightToday(); // Call HighlightToday after populating the calendar
    }

    private void PopulateCalendar()
    {
        for (int i = 1; i <= 31; i++)
        {
            GameObject dayButton = Instantiate(dayPrefab, calendarParent);
            dayButton.GetComponentInChildren<TMP_Text>().text = i.ToString();
            int day = i;
            dayButton.GetComponent<Button>().onClick.AddListener(() => OnDaySelected(day));

            // Initialize the assignment list for each day
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

        // Update the status bar based on the assignments
        dayUI.UpdateStatusBar(assignmentsPerDay[day]);
    }
}
