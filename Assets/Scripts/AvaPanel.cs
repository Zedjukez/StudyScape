using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AvaPanel : MonoBehaviour
{
    public Transform daysParent;
    public Transform calendarParent; // Reference to the actual calendar parent
    public PlanningScreen planningScreen;
    private bool[] nonAvailableDays;
    private Transform[] originalDayButtons;

    private void Start()
    {
        nonAvailableDays = new bool[32];
        originalDayButtons = new Transform[calendarParent.childCount];
    }

    public void ShowDays(int deadline)
    {
        // Store original day buttons
        for (int i = 0; i < calendarParent.childCount; i++)
        {
            originalDayButtons[i] = calendarParent.GetChild(i);
        }

        // Clear current daysParent
        foreach (Transform child in daysParent)
        {
            Destroy(child.gameObject);
        }

        // Move relevant day buttons to daysParent
        int currentDay = DateTime.Now.Day;
        nonAvailableDays = new bool[deadline + 1];

        for (int day = currentDay; day <= deadline; day++)
        {
            Transform dayButton = originalDayButtons[day - 1];
            dayButton.SetParent(daysParent);
            int dayIndex = day; // Capture day in a local variable

            Button button = dayButton.GetComponent<Button>();
            button.onClick.AddListener(() => ToggleDay(dayIndex));
        }
    }

    private void ToggleDay(int day)
    {
        nonAvailableDays[day] = !nonAvailableDays[day];
        Transform dayButton = daysParent.GetChild(day - DateTime.Now.Day);
        dayButton.GetComponent<Image>().color = nonAvailableDays[day] ? Color.red : Color.white;
    }

    public void OnSaveButtonClicked()
    {
        planningScreen.SaveAvailability(nonAvailableDays);
        RestoreDayButtons();
    }

    private void RestoreDayButtons()
    {
        foreach (Transform dayButton in originalDayButtons)
        {
            dayButton.SetParent(calendarParent);
        }
    }
}
