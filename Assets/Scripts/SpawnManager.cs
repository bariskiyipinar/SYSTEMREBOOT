using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] chestPoint;
    [SerializeField] private Image rebootBar;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private GameObject HealthChestPrefab;

    private int waveCount = 1;
    private int maxWaves = 5;
    private int enemiesPerWave = 0;

    RebootScore score;
    EnemyController enemy;

    [SerializeField] private Volume postProcessVolume;
    private ChromaticAberration chromaticAberration;


    [SerializeField] private GameObject winningPanel;
    [SerializeField] private TextMeshProUGUI winningScore;
    [SerializeField] private TextMeshProUGUI enemyCount;
    [SerializeField] private GameObject Player;

    private void Start()
    {
        StartCoroutine(SpawnWaves());

        if (postProcessVolume != null)
        {
            if (!postProcessVolume.profile.TryGet(out chromaticAberration))
            {
                Debug.LogWarning("Chromatic Aberration not found in post-processing profile!");
            }
        }

        winningPanel.SetActive(false);
        score = FindAnyObjectByType<RebootScore>();
        enemy=FindAnyObjectByType<EnemyController>();
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            SpawnWave();
            yield return new WaitForSeconds(spawnInterval);
            IncreaseDifficulty();
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);

            GameObject enemyToSpawn;

            if (waveCount < 2 || enemies.Length < 2)
            {
                enemyToSpawn = enemies[0];
                
            }
            else
            {
                int enemyIndex = Random.Range(0, Mathf.Min(waveCount, enemies.Length));
                enemyToSpawn = enemies[enemyIndex];
            }

            GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoints[spawnIndex].position, Quaternion.identity);

            Rigidbody2D rb = spawnedEnemy.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float speed = 1f + waveCount * 0.6f;
                rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed;
            }
        }

        if (waveCount == 2 && HealthChestPrefab != null)
        {
            int spawnIndex = Random.Range(0, chestPoint.Length);
            Instantiate(HealthChestPrefab, chestPoint[spawnIndex].position, Quaternion.identity);
        }
        if (waveCount == 3 && HealthChestPrefab != null)
        {
            int spawnIndex=Random.Range(0, chestPoint.Length);
            Instantiate(HealthChestPrefab, chestPoint[spawnIndex].position, Quaternion.identity);
        }
        if (waveCount == 4 && HealthChestPrefab != null)
        {
            int spawnIndex = Random.Range(0, chestPoint.Length);
            Instantiate(HealthChestPrefab, chestPoint[spawnIndex].position, Quaternion.identity);
        }
        if (waveCount == 5 && HealthChestPrefab != null)
        {
            int spawnIndex = Random.Range(0, chestPoint.Length);
            Instantiate(HealthChestPrefab, chestPoint[spawnIndex].position, Quaternion.identity);
        }




        UpdateRebootBar();
    }

    void IncreaseDifficulty()
    {
        enemiesPerWave = Mathf.Min(5, enemiesPerWave + 1);

    }

    void UpdateRebootBar()
    {
        rebootBar.fillAmount += 0.1f;

        if (rebootBar.fillAmount >= 1f)
        {
            rebootBar.fillAmount = 0f;
            waveCount++;

            if (chromaticAberration != null)
            {
                StartCoroutine(TriggerGlitchEffect());
            }

            if (waveCount > maxWaves)
            {
                Player.GetComponent<SpriteRenderer>().enabled = false;
                Time.timeScale = 0;
                winningPanel.SetActive(true);
                winningScore.text=score.score.ToString();
                enemyCount.text = score.GetEnemyKillCount().ToString();
            


                if (SoundManager.instance != null && SoundManager.instance.BGSound != null)
                {
                    SoundManager.instance.BGSound.pitch = 0.5f; 
                }

            }
        }
    }
    public void NewGameButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (SoundManager.instance != null && SoundManager.instance.BGSound != null)
        {
            SoundManager.instance.BGSound.pitch = 1f;
        }
    }

    IEnumerator TriggerGlitchEffect()
    {
        float maxIntensity = Mathf.Clamp01(0.3f + waveCount * 0.15f);
        float glitchDuration = 2f;
    


        chromaticAberration.intensity.value = maxIntensity;


        yield return new WaitForSeconds(glitchDuration);


    }
}
