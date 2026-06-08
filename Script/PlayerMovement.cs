using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Wajib ada untuk membaca teks skor di layar

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Sistem Game Tambahan")]
    public int maxNyawa = 3;
    private int nyawaSekarang;
    private int totalSkor = 0;

    // Variabel untuk menampung teks koin di layar secara otomatis
    private TextMeshProUGUI skorText;

    private Rigidbody2D rb;
    private static PlayerMovement instance;

    void Awake()
    {
        // Supaya karakter cuma ada satu di semua level
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            nyawaSekarang = maxNyawa; // Set nyawa penuh pas game pertama jalan
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CariUlangUI(); // Cari teks skor dan hati di awal level
        PindahKeSpawnPoint();
    }

    void Update()
    {
        // Pergerakan
        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        // Lompat + Efek Suara Lompat
        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // --- SUARA LOMPAT JALAN DI SINI ---
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.jumpSound);
            }
        }

        // Cek Jatuh Jurang
        if (transform.position.y < -10f)
        {
            KenaDamageAtauKalah();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek kena Gerinda (Tag harus Enemy atau nama mengandung Saw)
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.name.Contains("Saw"))
        {
            KenaDamageAtauKalah();
        }
    }

    // --- LOGIKA NYAWA BERKURANG / GAME OVER ---
    void KenaDamageAtauKalah()
    {
        nyawaSekarang--; // Nyawa berkurang 1

        // Mainkan suara kalah/kena damage
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.dieSound);
        }

        UpdateTampilanHati(); // Update gambar hati di layar

        if (nyawaSekarang <= 0)
        {
            // Jika nyawa habis total, reset game ke awal banget
            Debug.Log("Game Over! Kembali ke Main Menu.");
            nyawaSekarang = maxNyawa; // Reset nyawa jadi 3 lagi
            totalSkor = 0;            // Reset koin jadi 0 lagi
            SceneManager.LoadScene("Main Menu"); // Balik ke menu utama
        }
        else
        {
            // Jika nyawa masih ada, cuma restart di level tempat dia mati (Checkpoint)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // --- FUNGSI TAMBAH SKOR (Dipanggil oleh script koin nanti) ---
    public void TambahSkorKoin(int jumlah)
    {
        totalSkor += jumlah;
        UpdateTampilanSkor();
    }

    // --- LOGIKA OTOMATIS PINDAH KE PIPA SPAWNPOINT DAN UPDATE UI ---
    private void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    private void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CariUlangUI(); // Cari ulang UI karena di scene baru UI-nya juga baru
        PindahKeSpawnPoint();
    }

    void CariUlangUI()
    {
        // Cari otomatis objek teks bernama "SkorText" di level baru
        GameObject textObj = GameObject.Find("SkorText");
        if (textObj != null)
        {
            skorText = textObj.GetComponent<TextMeshProUGUI>();
        }

        UpdateTampilanSkor();
        UpdateTampilanHati();
    }

    void UpdateTampilanSkor()
    {
        if (skorText != null)
        {
            skorText.text = "Koin: " + totalSkor;
        }
    }

    void UpdateTampilanHati()
    {
        // Sistem otomatis mencari objek gambar "Hati1", "Hati2", "Hati3" di Canvas level baru
        for (int i = 0; i < 3; i++)
        {
            GameObject hati = GameObject.Find("Hati" + (i + 1));
            if (hati != null)
            {
                // Kalau sisa nyawa mencukupi dia aktif, kalau gak mencukupi dia sembunyi
                hati.SetActive(i < nyawaSekarang);
            }
        }
    }

    void PindahKeSpawnPoint()
    {
        // Cari pipa hijau yang kamu beri nama SpawnPoint
        GameObject spawn = GameObject.Find("SpawnPoint");
        if (spawn != null)
        {
            transform.position = spawn.transform.position;
            // Reset gravitasi biar gak jatuh ngebut pas baru muncul
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }
    }
}