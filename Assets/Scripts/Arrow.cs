using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage= 10;
    public Vector2 speed = new Vector2 (3f,0);
    public Vector2 knockback = new Vector2 (2f,0);
    Rigidbody2D rb2d;
    Animator animator;
    CircleCollider2D circleCollider;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb2d.velocity = new Vector2(speed.x * transform.localScale.x, speed.y);
        animator= GetComponent<Animator>();
        circleCollider= GetComponent<CircleCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(knockback.x, knockback.y) * new Vector2(-1, 1);
            bool gotHit=damageable.Hit(damage, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log(collision.name + "hit for " + damage);
               
            }
            
            
        }
        rb2d.velocity = Vector2.zero;
        Destroy(circleCollider);
        animator.SetTrigger(AnimationString.hit);
        InvokeRepeating("del", 0.7f, 0);
    }
    private void del()
    {
        Destroy(gameObject);
    }
}
