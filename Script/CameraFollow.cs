using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Bisa dikosongin di Inspector, nanti dia nyari sendiri
    public Vector3 offset = new Vector3(0, 2, -10);
    public float smoothSpeed = 0.125f;

    void Update()
    {
        // JIKA TARGET KOSONG (misal pas baru masuk Scene 2), CARI SIAPA YANG PUNYA TAG "PLAYER"
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Tentukan posisi tujuan (posisi karakter + jarak aman)
            Vector3 desiredPosition = target.position + offset;

            // Lerp bikin gerakan kamera halus (smooth)
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update posisi kamera
            transform.position = smoothedPosition;
        }
    }
}