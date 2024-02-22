using UnityEngine;

public class TileSelector : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            TileInfo tileInfo = hit.transform.GetComponentInParent<TileInfo>();
            if (tileInfo != null)
            {
                UIManager.Instance.DisplayTileInfo(tileInfo.gridPosition);
            }
        }
    }
}
