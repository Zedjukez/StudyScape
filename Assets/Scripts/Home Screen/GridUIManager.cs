using UnityEngine;
using UnityEngine.UI;

public class GridUIManager : MonoBehaviour
{
    public static GridUIManager Instance;

    public GameObject confirmButton;
    public GameObject rotateButton;
    public GameObject inventoryButton;
    public GameObject cancelButton;
    public GameObject inventoryPanel;

    private FurnitureController selectedFurniture;
    private bool movingToInventory = false;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Hide edit mode buttons at start
        HideEditModeButtons();
    }

    public void EnterEditMode(FurnitureController furniture)
    {
        selectedFurniture = furniture; // Assign selectedFurniture
        ShowEditModeButtons();
    }


    public void ExitEditMode()
    {
        HideEditModeButtons();
    }

    public void AddToInventory(GameObject furniture)
    {
        InventoryManager.Instance.AddToInventory(furniture);
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }

    public void ConfirmPosition()
    {
        if (selectedFurniture != null)
        {
            selectedFurniture.ConfirmPosition();
        }
    }

    public void RotateFurniture()
    {
        if (selectedFurniture != null)
        {
            selectedFurniture.RotateFurniture();
        }
    }

    public void MoveToInventory()
    {
        if (!movingToInventory && selectedFurniture != null) // Ensure only one call to MoveToInventory at a time
        {
            movingToInventory = true;
            selectedFurniture.MoveToInventory();
            movingToInventory = false;
        }
    }


    public void CancelEdit()
    {
        if (selectedFurniture != null)
        {
            selectedFurniture.CancelEdit();
        }
    }

    private void ShowEditModeButtons()
    {
        confirmButton.SetActive(true);
        rotateButton.SetActive(true);
        inventoryButton.SetActive(true);
        cancelButton.SetActive(true);
    }

    private void HideEditModeButtons()
    {
        confirmButton.SetActive(false);
        rotateButton.SetActive(false);
        inventoryButton.SetActive(false);
        cancelButton.SetActive(false);
    }
}
