using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public GameObject inventoryPanel;
    public Transform inventoryContent;
    public GameObject inventoryItemPrefab;
    private bool movingToInventory = false;


    private List<GameObject> inventoryItems = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        inventoryPanel.SetActive(false);
    }

    public void AddToInventory(GameObject furniture)
    {
        inventoryItems.Add(furniture);
        GameObject item = Instantiate(inventoryItemPrefab, inventoryContent);
        item.GetComponentInChildren<TMP_Text>().text = furniture.name;
        item.GetComponent<Button>().onClick.AddListener(() => PlaceFromInventory(furniture));
    }

    public void PlaceFromInventory(GameObject furniture)
    {
        furniture.SetActive(true);
        GridUIManager.Instance.EnterEditMode(furniture.GetComponent<FurnitureController>());
        inventoryItems.Remove(furniture);
        Destroy(inventoryContent.Find(furniture.name).gameObject); // Remove item from inventory UI
    }

    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);
        // Populate inventory UI
    }

    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
    } 
}
