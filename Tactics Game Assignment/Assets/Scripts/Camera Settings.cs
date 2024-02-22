using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public float height = 10f;
    public float distance = 10f;
    public float angle = 45f;

    void Start()
    {
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        float radians = angle * Mathf.Deg2Rad;

        float x = Mathf.Sin(radians) * distance;
        float z = Mathf.Cos(radians) * distance;

        transform.position = new Vector3(x, height, z);
        transform.rotation = Quaternion.Euler(90f - angle, 0f, 0f);
    }
}