using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Kecepatan putar, bisa diubah di Inspector
    public float rotationSpeed = 300f;

    void Update()
    {
        // Memutar objek pada sumbu Z setiap frame
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}