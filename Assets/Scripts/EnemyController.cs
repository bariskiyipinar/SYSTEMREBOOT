using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Transform playerPos;
    public float speed;

    RebootScore rebootScore;
    public int enemycount = 0;

    private void Start()
    {
        playerPos=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       rebootScore=FindAnyObjectByType<RebootScore>();
    }



    private void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

      
        Vector2 direction = (playerPos.position - transform.position).normalized;

        if (direction.x >= 0)
        {
         
            GetComponent<SpriteRenderer>().flipX = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
           
            GetComponent<SpriteRenderer>().flipX = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
            rebootScore.UpdateScore(100);
            rebootScore.AddEnemyKill();
        }
    }

 
}
