using UnityEngine;

public class Koin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Tetap pakai logika nama "Karakter" sesuai bawaan game kamu
        if (collision.gameObject.name == "Karakter")
        {
            // Tambahkan logika untuk ngabarin PlayerMovement biar skornya nambah
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TambahSkorKoin(1); // Skor nambah 1
            }

            // Tambahkan logika buat manggil suara cling dari AudioManager
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.coinSound);
            }

            Destroy(gameObject); // Koin hilang!
        }
    }
}