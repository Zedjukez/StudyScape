using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    // Define the corners of the grid as public fields for easy adjustment in the Inspector
    public Vector2 bottomLeftCorner = new Vector2(-0.25f, -2f);
    public Vector2 topLeftCorner = new Vector2(1.8f, -1f);
    public Vector2 topRightCorner = new Vector2(3.6f, -2f);
    public Vector2 bottomRightCorner = new Vector2(1.8f, -3f);

    private void Awake()
    {
        Instance = this;
    }

    // Method to check if a position is within the grid boundaries
    public static bool IsWithinGrid(Vector2 position)
    {
        return Instance != null && Instance.CheckIfWithinGrid(position);
    }

    private bool CheckIfWithinGrid(Vector2 position)
    {
        // Use a point-in-polygon check for the isometric grid
        return IsPointInPolygon(new Vector2[] { bottomLeftCorner, topLeftCorner, topRightCorner, bottomRightCorner }, position);
    }

    // Helper method to check if a point is within a polygon
    private bool IsPointInPolygon(Vector2[] polygon, Vector2 point)
    {
        int j = polygon.Length - 1;
        bool inside = false;
        for (int i = 0; i < polygon.Length; j = i++)
        {
            if (((polygon[i].y > point.y) != (polygon[j].y > point.y)) &&
                (point.x < (polygon[j].x - polygon[i].x) * (point.y - polygon[i].y) / (polygon[j].y - polygon[i].y) + polygon[i].x))
            {
                inside = !inside;
            }
        }
        return inside;
    }

    // OnDrawGizmos to visualize the grid in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Draw lines between each corner to form the grid boundary
        Gizmos.DrawLine(bottomLeftCorner, topLeftCorner);
        Gizmos.DrawLine(topLeftCorner, topRightCorner);
        Gizmos.DrawLine(topRightCorner, bottomRightCorner);
        Gizmos.DrawLine(bottomRightCorner, bottomLeftCorner);

        // Optionally, draw a grid within the boundaries
        DrawGridLines();
    }

    // Optional method to draw grid lines within the boundaries
    private void DrawGridLines()
    {
        float gridSpacing = 0.5f; // Adjust this value to change the grid spacing
        for (float x = bottomLeftCorner.x; x <= topRightCorner.x; x += gridSpacing)
        {
            Gizmos.DrawLine(new Vector2(x, bottomLeftCorner.y), new Vector2(x, topLeftCorner.y));
        }
        for (float y = bottomRightCorner.y; y <= topLeftCorner.y; y += gridSpacing)
        {
            Gizmos.DrawLine(new Vector2(bottomLeftCorner.x, y), new Vector2(topRightCorner.x, y));
        }
    }
}
