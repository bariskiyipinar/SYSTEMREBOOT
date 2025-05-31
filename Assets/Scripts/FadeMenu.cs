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


    IEnumerator fadeTime(float duration)
    {
        fadePanel.enabled = true;

        float elapsed = 0f;
        float startPitch = SoundManager.instance.BGSound.pitch;
        float targetPitch = 0f; 

     
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            SoundManager.instance.BGSound.pitch = Mathf.Lerp(startPitch, targetPitch, t);
            yield return null;
        }

        Enemy.SetActive(false);
        yield return new WaitForSeconds(duration);

        bullet.SetActive(false);
        yield return new WaitForSeconds(duration);

        Player.SetActive(false);
        yield return new WaitForSeconds(duration);

    
        SceneManager.LoadScene("Game");

     
    }

}
