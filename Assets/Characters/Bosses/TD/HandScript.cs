using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    // Start is called before the first frame update
   
    Rigidbody2D rb2d;
    CircleCollider2D circleCollider;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("del", 0.5f, 0);
    }
    
    private void del()
    {
        Destroy(gameObject);
    }
}
