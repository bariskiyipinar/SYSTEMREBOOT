using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform playerPos;
    public float speed;

    private void Start()
    {
        playerPos=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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

}
