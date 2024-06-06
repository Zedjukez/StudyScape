using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanningScreen : MonoBehaviour
{
    public TMP_InputField assignmentNameInput;
    public TMP_Dropdown assignmentTypeDropdown;
    public TMP_InputField deadlineInput;
    public TMP_InputField hoursInput;
    public AvaPanel avaPanelData; // Reference to the AvaPanel script
    public GameObject avaPanel;
    public AssignmentManager assignmentManager;
    public GameObject warningPanel; // Reference to the warning panel

    private bool[] nonAvailableDays;

    private void Start()
    {
        nonAvailableDays = new bool[32];
    }

    public void OnPlanButtonClicked()
    {
        string name = assignmentNameInput.text;
        string type = assignmentTypeDropdown.options[assignmentTypeDropdown.value].text;
        if (!int.TryParse(deadlineInput.text, out int deadline) || deadline <= 0 ||
            !int.TryParse(hoursInput.text, out int hours) || hours <= 0)
        {
            warningPanel.SetActive(true);
            Debug.LogError("Please fill in all the fields correctly.");
            return;
        }

        if (nonAvailableDays == null || nonAvailableDays.Length == 0)
        {
            warningPanel.SetActive(true);
            Debug.LogError("Please set your availability.");
            return;
        }

        Assignment newAssignment = new Assignment(name, type, deadline, hours);
        assignmentManager.AddAssignment(newAssignment);

        ClearInputFields();
    }

    public void OpenAvaPanel()
    {
        if (!int.TryParse(deadlineInput.text, out int deadline) || deadline <= 0)
        {
            warningPanel.SetActive(true);
            Debug.LogError("Please enter a valid deadline.");
            return;
        }

        avaPanel.SetActive(true);
        avaPanelData.ShowDays(deadline);
    }

    public void SaveAvailability(bool[] availability)
    {
        nonAvailableDays = availability;
        avaPanel.SetActive(false);
    }

    private void ClearInputFields()
    {
        assignmentNameInput.text = "";
        assignmentTypeDropdown.value = 0;
        deadlineInput.text = "";
        hoursInput.text = "";
    }
}
