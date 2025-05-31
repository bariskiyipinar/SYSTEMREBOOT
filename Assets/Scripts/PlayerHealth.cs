using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject[] Hearths;
    private int health;
    private int maxHealth;

    private void Start()
    {
        maxHealth = Hearths.Length;
        health = maxHealth;
        UpdateHearts();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0) health = 0;
        UpdateHearts();

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddHealth(int amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < Hearths.Length; i++)
        {
            Hearths[i].SetActive(i < health);
        }
    }
}
