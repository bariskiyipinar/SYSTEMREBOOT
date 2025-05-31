using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RebootScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public int score = 0;
    public int enemyKillCount = 0;

    private void Start()
    {
        scoreText.text = score.ToString();
    }


    public void UpdateScore(int amount)
    {
        score += amount;

        scoreText.text = score.ToString();

    }


    public void AddEnemyKill()
    {
        enemyKillCount++;
      
    }

    public int GetEnemyKillCount()
    {
        return enemyKillCount;
    }
}
