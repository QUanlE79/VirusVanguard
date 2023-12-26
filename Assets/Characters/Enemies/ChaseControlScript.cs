using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseControlScript : MonoBehaviour
{
    //public FlyingEnemies[] enemyArray;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FlyingEnemies enemy = GetComponentInParent<FlyingEnemies>();
            if (enemy != null)
            {
                enemy.chase = true;
            }         
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {    
        if (collision.CompareTag("Player"))
        {
            FlyingEnemies enemy = GetComponentInParent<FlyingEnemies>();

            if (enemy != null)
            {
                enemy.chase = false;
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //enemyArray = FindObjectsOfType<FlyingEnemies>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
