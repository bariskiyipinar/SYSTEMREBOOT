using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
     public AudioSource BGSound;

    private void Start()
    {
        GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (BGSound != null)
            {
                BGSound.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }

      
    }
}
