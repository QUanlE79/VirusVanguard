using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arrow : MonoBehaviour
{
    
    // Start is called before the first frame update
    public int damage= 10;
    public Vector2 speed = new Vector2 (3f,0);
    public Vector2 knockback = Vector2.zero;
    Rigidbody2D rb2d;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb2d.velocity = new Vector2(speed.x * transform.localScale.x, speed.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if(damageable != null)
        {
            bool gotHit = damageable.Hit(damage,knockback);
          
        }
    }
}
