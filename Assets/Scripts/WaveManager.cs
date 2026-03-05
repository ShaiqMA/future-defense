using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 1;
    public int maxWaves = 5;
    public GameObject enemyPrefab;
    public TextMeshProUGUI waveText;

    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
        NextWave();
        }
    }
    public void StartWave()
    {
        waveText.text = "Wave: " + currentWave;

        int enemyCount = currentWave * 2;

        float topOfScreen = Camera.main.ScreenToWorldPoint(
            new Vector3(0, Screen.height, 0)
        ).y;

        float leftSide = Camera.main.ScreenToWorldPoint(
            new Vector3(0, 0, 0)
        ).x;

        float rightSide = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, 0, 0)
        ).x;

        for(int i = 0; i < enemyCount; i++)
        {
            Vector2 randomPos = new Vector2(
                Random.Range(leftSide, rightSide),
                topOfScreen + 1f
        );


            GameObject enemyObj = Instantiate(enemyPrefab, randomPos, Quaternion.identity);

            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.maxHealth = 3 + (currentWave - 2) * 2;
                enemy.currentHealth = enemy.maxHealth;
            }
        }

    }

    public void NextWave()
    {
        currentWave++;

        if(currentWave > maxWaves)
        {
            FindObjectOfType<GameManager>().WinGame();
        }
        else
        {
            StartWave();
        }
    }
}