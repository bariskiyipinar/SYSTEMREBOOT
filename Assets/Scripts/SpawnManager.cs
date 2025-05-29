using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Image rebootBar;
    [SerializeField] private float spawnInterval = 3f;

    private int waveCount = 1;
    private int maxWaves = 5;
    private int enemiesPerWave = 1;

    [SerializeField] private Volume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private float glitchIntensity = 0f;

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

        UpdateRebootBar();
    }

    void IncreaseDifficulty()
    {
        enemiesPerWave = Mathf.Min(10, enemiesPerWave + 1);

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
                SceneManager.LoadScene("Menu");
            }
        }
    }

    IEnumerator TriggerGlitchEffect()
    {
        float maxIntensity = Mathf.Clamp01(0.3f + waveCount * 0.15f);
        float glitchDuration = 2f;
        float fadeOutDuration = 1.5f;


        chromaticAberration.intensity.value = maxIntensity;


        yield return new WaitForSeconds(glitchDuration);


    }
}
