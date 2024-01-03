using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 10;
    public Vector2 speed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(2f, 0);
    Rigidbody2D rb2d;
    Animator animator;
    CircleCollider2D circleCollider;
    Vector2 startPonit;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPonit = new Vector2(transform.position.x, transform.position.y);
        if (SceneManager.GetActiveScene().buildIndex > 6)
        {
            damage *= 2;
        }
    }
    void Start()
    {

        rb2d.velocity = new Vector2(speed.x * transform.localScale.x, speed.y);
        animator = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
            new Vector2(startPonit.x, startPonit.y));
        if (distance > 20)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDamageable damageable = collision.GetComponent<PlayerDamageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(knockback.x, knockback.y) * new Vector2(-1, 1);
            bool gotHit = damageable.Hit(damage, deliveredKnockback);
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
