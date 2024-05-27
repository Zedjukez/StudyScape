using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject[] bumps; // Array of bumps behind each icon
    public GameObject[] dropdownArrows; // Array of dropdown arrows for each main function
    public GameObject[] functionPanels; // Array of panels corresponding to each function (Home, Agenda, Planning, Shop)
    public GameObject home; // GameObject for Home panel
    public GameObject agenda; // GameObject for Agenda panel
    public GameObject planning; // GameObject for Planning panel
    public GameObject shop; // GameObject for Shop panel
    public TextMeshProUGUI coinsText; // UI Text component to display coins

    public Sprite dropDownSprite;
    public Sprite dropUpSprite;

    public int currentFunctionIndex = 0; // 0 for Home, 1 for Agenda, 2 for Planning, 3 for Shop
    private int coins = 50; // Starting coins

    private void Start()
    {
        // Initially, set only the Home bump and function panel active
        ActivateFunction(0);
        UpdateCoinsUI();
    }

    public void SelectMainFunction(int index)
    {
        // Activate the selected function
        ActivateFunction(index);
    }

    public void ToggleFunctionPanel(int index)
    {
        // Toggle the visibility of the function panel corresponding to the index
        functionPanels[index].SetActive(!functionPanels[index].activeSelf);

        // Change the dropdown arrow sprite
        GameObject dropdownArrow = dropdownArrows[index];
        dropdownArrow.GetComponent<Image>().sprite = functionPanels[index].activeSelf ? dropUpSprite : dropDownSprite;

        // Hide other function panels
        for (int i = 0; i < functionPanels.Length; i++)
        {
            if (i != index)
            {
                functionPanels[i].SetActive(false);
            }
        }
    }

    public void ShopToggle()
    {
        // Toggle the visibility of the shop panel
        shop.SetActive(!shop.activeSelf);

        // Hide other function panels
        for (int i = 0; i < functionPanels.Length; i++)
        {
            if (i != 3) // Index 3 corresponds to the Shop panel
            {
                functionPanels[i].SetActive(false);
            }
        }
    }

    private void ActivateFunction(int index)
    {
        // Activate the selected function bump and function panel
        bumps[index].SetActive(true);
        functionPanels[index].SetActive(true);

        // Activate corresponding function panels
        home.SetActive(index == 0);
        agenda.SetActive(index == 1);
        planning.SetActive(index == 2);
        shop.SetActive(index == 3);

        // Deactivate other function bumps and panels
        for (int i = 0; i < bumps.Length; i++)
        {
            if (i != index)
            {
                bumps[i].SetActive(false);
                functionPanels[i].SetActive(false);
            }
        }

        // Update the current function index
        currentFunctionIndex = index;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinsUI();
    }

    public bool RemoveCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateCoinsUI();
            return true;
        }
        return false;
    }

    private void UpdateCoinsUI()
    {
        // Update the UI text component to display coins
        coinsText.text = coins.ToString();
    }

    public void CloseParentWindow(GameObject child)
    {
        // Deactivate the parent GameObject of the given child GameObject
        if (child.transform.parent != null)
        {
            child.transform.parent.gameObject.SetActive(false);
        }
    }

    public void ActivateWindow(GameObject activated)
    {
        if(activated.transform.parent != null)
        {
            activated.SetActive(true);
        }
    }
}
