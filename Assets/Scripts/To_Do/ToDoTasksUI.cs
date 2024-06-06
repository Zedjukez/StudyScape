using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ToDoTasksUI : MonoBehaviour
{
    public GameObject taskPrefab;
    public Transform tasksParent;
    public AssignmentManager assignmentManager;

    private void Start()
    {
        UpdateTasksUI();
    }

    public void UpdateTasksUI()
    {
        // Clear existing tasks
        foreach (Transform child in tasksParent)
        {
            Destroy(child.gameObject);
        }

        // Add new tasks
        foreach (Assignment assignment in assignmentManager.assignments)
        {
            CreateTaskUI(assignment);
        }
    }

    private void CreateTaskUI(Assignment assignment)
    {
        GameObject taskObject = Instantiate(taskPrefab, tasksParent);
        taskObject.transform.Find("TDName").GetComponent<TextMeshProUGUI>().text = assignment.Name;
        taskObject.transform.Find("TDSessions").GetComponent<TextMeshProUGUI>().text = $"{assignment.SessionsCompleted}/{assignment.TotalSessions}";
        taskObject.transform.Find("TDDeadline").GetComponent<TextMeshProUGUI>().text = $"deadline: {assignment.Deadline}";
        Button advanceButton = taskObject.transform.Find("TDAdvance").GetComponent<Button>();
        advanceButton.onClick.AddListener(() => AdvanceTask(assignment));
    }

    private void AdvanceTask(Assignment assignment)
    {
        assignment.SessionsCompleted++;

        // Calculate XP
        int baseXP = 10 * assignment.Hours;
        int sessionMultiplier = assignment.SessionsCompleted > 1 ? assignment.SessionsCompleted - 1 : 1;
        int totalXP = (int)(baseXP * sessionMultiplier * 1.5f);

        // Calculate Gold
        int totalGold = assignment.SessionsCompleted * 5;

        // Grant rewards
        assignmentManager.uiManager.AddCoins(totalGold);
        assignmentManager.uiManager.AddXP(totalXP);

        // Check if task is completed
        if (assignment.SessionsCompleted >= assignment.TotalSessions)
        {
            assignmentManager.RemoveAssignment(assignment);
        }

        UpdateTasksUI();
    }
}
