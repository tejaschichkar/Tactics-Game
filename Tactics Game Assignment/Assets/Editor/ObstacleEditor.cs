using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    ObstacleData obstacleData;

    void OnEnable()
    {
        obstacleData = (ObstacleData)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Obstacle Editor");

        for (int x = 0; x < obstacleData.obstacleGrid.GetLength(0); x++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int z = 0; z < obstacleData.obstacleGrid.GetLength(1); z++)
            {
                obstacleData.obstacleGrid[x, z] = EditorGUILayout.Toggle(obstacleData.obstacleGrid[x, z]);
            }

            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(obstacleData);
            ObstacleManager.Instance.UpdateObstacles();
        }
    }
}
