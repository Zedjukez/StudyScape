using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void OnMouseDown()
    {
        if (isDragging) return;
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
        isDragging = true;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        // Enter edit mode
        GridUIManager.Instance.EnterEditMode(this);
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z)) + offset;
        newPosition.z = 0; // Ensure it stays on the same z plane

        if (GridManager.IsWithinGrid(new Vector2(newPosition.x, newPosition.y)))
        {
            transform.position = newPosition;
        }
    }

    public void ConfirmPosition()
    {
        // Check if the furniture is within the grid and not colliding with any other furniture
        if (GridManager.IsWithinGrid(new Vector2(transform.position.x, transform.position.y)) && !IsCollidingWithOtherFurniture())
        {
            // Confirm position and disable edit panel
            GridUIManager.Instance.ExitEditMode();
        }
        else
        {
            // Revert to initial position if not valid
            CancelEdit();
        }
    }

    private bool IsCollidingWithOtherFurniture()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<Collider2D>().bounds.size, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Furniture"))
            {
                return true;
            }
        }
        return false;
    }

    public void RotateFurniture()
    {
        Debug.Log("Rotated");
        transform.Rotate(0, 180, 0);

    }

    public void CancelEdit()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    public void MoveToInventory()
    {
        // Add to inventory
        GridUIManager.Instance.AddToInventory(this.gameObject);
        gameObject.SetActive(false);
    }
}

