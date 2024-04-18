using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] functionPanels; // Array of panels corresponding to each function (Home, Agenda, Planning)
    public GameObject[] mainFunctionIcons; // Array of main function icons (HomeIcon, AgendaIcon, PlanningIcon)
    public GameObject[] bumps; // Array of bumps behind each icon
    public GameObject[] dropdownArrows; // Array of dropdown arrows for each main function

    public Sprite dropDownSprite;
    public Sprite dropUpSprite;

    private GameObject selectedMainFunctionIcon;

    private void Start()
    {
        // Initially hide all function panels and dropdown arrows
        foreach (GameObject panel in functionPanels)
        {
            panel.SetActive(false);
        }
        foreach (GameObject arrow in dropdownArrows)
        {
            arrow.SetActive(false);
        }
    }

    public void SelectMainFunction(int index)
    {
        // Hide previously selected main function icon's bump and dropdown arrow
        if (selectedMainFunctionIcon != null)
        {
            bumps[index].SetActive(false);
            dropdownArrows[index].SetActive(false);
        }

        // Show bump and dropdown arrow above the selected main function icon
        GameObject selectedIcon = mainFunctionIcons[index];
        bumps[index].SetActive(true);
        dropdownArrows[index].SetActive(true);
        selectedMainFunctionIcon = selectedIcon;

        // Toggle the visibility of the corresponding function panel
        for (int i = 0; i < functionPanels.Length; i++)
        {
            functionPanels[i].SetActive(i == index);
        }

        // Toggle the visibility of the dropdown arrow and change its sprite
        GameObject dropdownArrow = dropdownArrows[index];
        dropdownArrow.GetComponent<Image>().sprite = dropUpSprite;

        // Hide other dropdown arrows
        foreach (GameObject arrow in dropdownArrows)
        {
            if (arrow != dropdownArrow)
            {
                arrow.SetActive(false);
            }
        }
    }
}
