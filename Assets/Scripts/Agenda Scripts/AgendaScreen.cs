using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class AgendaScreen : MonoBehaviour
{
    public GameObject detailedDayPanel;
    public TMP_Text dayDetailsText;
    private int currentDay;
    private Assignment currentAssignment;
    private List<Assignment> currentAssignments;
    public CalendarUI calendarUI;

    public TMP_Dropdown newDayDropdown;
    public GameObject moveTaskPanel;

    private void Start()
    {
        detailedDayPanel.SetActive(false);
        moveTaskPanel.SetActive(false);
    }

    public void ShowDayDetails(int day, List<Assignment> assignments)
    {
        currentDay = day;
        currentAssignments = assignments;
        detailedDayPanel.SetActive(true);

        if (assignments.Count == 0)
        {
            dayDetailsText.text = "geen plannen vandaag";
        }
        else
        {
            dayDetailsText.text = "";
            foreach (Assignment assignment in assignments)
            {
                dayDetailsText.text += $"{assignment.Name.ToLower()} ({assignment.Type.ToLower()}) - {assignment.Hours} hours\n";
                dayDetailsText.text += $"deadline: {assignment.Deadline}\n";
                dayDetailsText.text += $"hours allocated: {assignment.HoursPerDay(day)}\n";
            }
        }
    }

    public void SetCurrentAssignmentToMove()
    {
        if (currentAssignments.Count > 0)
        {
            currentAssignment = currentAssignments[0];
            moveTaskPanel.SetActive(true);
            PopulateNewDayDropdown();
        }
    }

    private void PopulateNewDayDropdown()
    {
        newDayDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentDay = System.DateTime.Now.Day;

        for (int i = currentDay; i <= currentAssignment.Deadline; i++)
        {
            options.Add(i.ToString());
        }

        newDayDropdown.AddOptions(options);
    }

    public void ConfirmMoveTask()
    {
        if (currentAssignment != null)
        {
            int newDay = int.Parse(newDayDropdown.options[newDayDropdown.value].text);
            calendarUI.MoveAssignment(currentDay, newDay, currentAssignment);
            moveTaskPanel.SetActive(false);
            detailedDayPanel.SetActive(false);
        }
    }
}
