using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public Vector2Int gridPosition;

    // Additional properties, methods, or events related to the tile can be added here.

    void Start()
    {
        // Set gridPosition based on the cube's position
        gridPosition = new Vector2Int(transform.GetSiblingIndex(), transform.parent.GetSiblingIndex());
    }
}
