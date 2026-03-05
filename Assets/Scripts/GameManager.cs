using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject winPanel;
    public GameObject losePanel; 
    public GameObject player;
    public GameObject enemySpawner;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI menu;
   private AudioSource audioSource;
    public AudioClip winSound;
    public AudioClip loseSound;
    
    private int score =  0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        player.SetActive(false);
        enemySpawner.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        player.SetActive(true);
        enemySpawner.SetActive(true);
        enemySpawner.GetComponent<WaveManager>().StartWave();

        player.GetComponent<PlayerController>().currentHealth =
        player.GetComponent<PlayerController>().maxHealth;
        
        UpdateHealthUI(player.GetComponent<PlayerController>().currentHealth);
    }
    
    public void WinGame()
    {
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
        audioSource.PlayOneShot(winSound);
    }

    public void LoseGame()
    {
        gamePanel.SetActive(false);
        losePanel.SetActive(true);
        audioSource.PlayOneShot(loseSound);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
    void Update()
    {
   
    }
    public void UpdateHealthUI(int health)
    {
       healthText.text = "Health: " + health;
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "score: " + score;
    }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
