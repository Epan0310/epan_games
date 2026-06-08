using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    void Update()
    {
        // 1. Cek kalau jatuh (kalau posisi Y di bawah lantai)
        if (transform.position.y < -6f)
        {
            Restart();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 2. Cek kalau nabrak objek bernama "Saw"
        if (collision.gameObject.name == "Saw")
        {
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}