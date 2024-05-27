using UnityEngine;
using System;

public class HighlightCurrentDay : MonoBehaviour
{
    public GameObject highlightPrefab; // Prefab to instantiate
    public Transform calendarParent; // Parent holding the day buttons

    public void HighlightToday()
    {
        int currentDay = DateTime.Now.Day;

        Debug.Log("Highlighting today's date: " + currentDay);
        Debug.Log("Number of children in calendarParent: " + calendarParent.childCount);

        if (currentDay >= 1 && currentDay <= 31)
        {
            // Check if the calendarParent has enough children
            if (calendarParent.childCount < currentDay)
            {
                Debug.LogError("Calendar parent does not have enough children for the current day.");
                return;
            }

            // Find the corresponding day button (assuming the day buttons are children of calendarParent)
            Transform dayButton = calendarParent.GetChild(currentDay - 1);

            if (dayButton != null)
            {
                // Instantiate the highlight prefab on top of the day button
                GameObject highlight = Instantiate(highlightPrefab, dayButton);
                highlight.transform.SetAsLastSibling(); // Ensure the highlight is on top
            }
            else
            {
                Debug.LogError("Day button is null for current day: " + currentDay);
            }
        }
        else
        {
            Debug.LogError("Current day is out of valid range: " + currentDay);
        }
    }
}
