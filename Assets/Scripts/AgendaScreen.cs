using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class AgendaScreen : MonoBehaviour
{
    public GameObject detailedDayPanel;
    public TMP_Text dayDetailsText;

    public void ShowDayDetails(int day, List<Assignment> assignments)
    {
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
                dayDetailsText.text += $"{assignment.Name.ToLower()} ({assignment.Type.ToLower()}) - {assignment.Hours} uren\n";
                dayDetailsText.text += $"deadline: {assignment.Deadline}\n"; // Display the deadline
            }
        }
    }
}
