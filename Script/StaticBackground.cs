using UnityEngine;

public class StaticBackground : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            // Background akan SELALU mengikuti posisi kamera
            // Jadi seolah-olah backgroundnya tidak terbatas
            transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, 10f);
        }
    }
}