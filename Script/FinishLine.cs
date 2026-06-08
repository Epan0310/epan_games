using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [Header("Settings Level")]
    public string namaSceneTujuan; // Contoh isi di Inspector: Level 2
    public int levelIni; // Contoh isi di Inspector: 1 (untuk Level 1)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Gunakan Tag "Player" supaya lebih akurat dibanding cek nama
        if (collision.CompareTag("Player"))
        {
            // --- LOGIKA UNLOCK LEVEL ---
            // Kita tentukan level mana yang terbuka selanjutnya
            int levelTerbuka = levelIni + 1;

            // Ambil data level yang sudah pernah dicapai sebelumnya
            int levelDulu = PlayerPrefs.GetInt("levelReached", 1);

            // Jika level yang baru diselesaikan lebih besar dari rekor sebelumnya, update datanya
            if (levelTerbuka > levelDulu)
            {
                PlayerPrefs.SetInt("levelReached", levelTerbuka);
                PlayerPrefs.Save(); // Memastikan data tersimpan di memori hp/laptop
                Debug.Log("Level " + levelTerbuka + " Terbuka!");
            }

            // Pindah ke scene berikutnya
            SceneManager.LoadScene(namaSceneTujuan);
        }
    }
}