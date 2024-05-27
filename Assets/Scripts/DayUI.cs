using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DayUI : MonoBehaviour
{
    public Transform statusBarParent; // Parent object for status bars

    public void UpdateStatusBar(List<Assignment> assignments)
    {
        // Clear existing status bars
        foreach (Transform child in statusBarParent)
        {
            Destroy(child.gameObject);
        }

        // Create new status bars based on assignments
        foreach (var assignment in assignments)
        {
            GameObject statusBar = new GameObject("StatusBar", typeof(Image));
            statusBar.transform.SetParent(statusBarParent);

            Image statusImage = statusBar.GetComponent<Image>();
            switch (assignment.Type)
            {
                case "Exam":
                    statusImage.color = Color.red;
                    break;
                case "Studying":
                    statusImage.color = Color.green;
                    break;
                case "School project":
                    statusImage.color = Color.blue;
                    break;
            }
        }
    }
}
