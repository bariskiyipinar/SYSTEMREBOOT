using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeMenu : MonoBehaviour
{
    [SerializeField] private Animator fadePanel;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject bullet;


    private void Start()
    {
        fadePanel.enabled = false;
        Enemy.SetActive(true);
        bullet.SetActive(true);
        Player.SetActive(true);
    }
    public void FadeButton()
    {
        StartCoroutine(fadeTime(2));

    }


    IEnumerator fadeTime(int amount)
    {
        fadePanel.enabled = true;
        yield return new WaitForSeconds(amount);
        Enemy.SetActive(false);
        yield return new WaitForSeconds(amount);
        bullet.SetActive(false);
        yield return new WaitForSeconds(amount);
        Player.SetActive(false);
        yield return new WaitForSeconds(amount);
        SceneManager.LoadScene("Game");

    }
}
