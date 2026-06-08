using UnityEngine;
using UnityEngine.UI; // Wajib untuk Button
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons; // Tarik semua tombol level ke sini nanti

    void Start()
    {
        // Ambil data level mana yang sudah terbuka (defaultnya level 1)
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Jika urutan tombol lebih besar dari level yang dicapai, maka matikan
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false; // Tombol jadi abu-abu/mati

                // Opsional: bikin gambarnya agak transparan biar kelihatan "gelap"
                Color c = levelButtons[i].GetComponent<Image>().color;
                c.a = 0.5f;
                levelButtons[i].GetComponent<Image>().color = c;
            }
        }
    }

    // Fungsi untuk dipanggil saat tombol diklik
    public void PilihLevel(string namaLevel)
    {
        SceneManager.LoadScene(namaLevel);
    }
}