using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text tileInfoText;

    void Awake()
    {
        Instance = this;
    }

    public void DisplayTileInfo(Vector2Int gridPosition)
    {
        tileInfoText.text = "Tile Position: " + gridPosition.x + ", " + gridPosition.y;
    }
}
