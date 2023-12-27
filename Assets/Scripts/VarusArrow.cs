using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarusArrow : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage=0;
    public Vector2 speed = new Vector2 (3f,0);
    public Vector2 knockback = new Vector2 (2f,0);
    Rigidbody2D rb2d;
    Animator animator;
    CircleCollider2D circleCollider;
    public Equipment weapon;
    public ParticleSystem effect;
    private float localScaleX;
    Vector2 startPoint;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        effect = GetComponentInChildren<ParticleSystem>();
        if(weapon== null)
        {
            damage = 0;
        }
        else
        {
            damage = weapon.damageModifier;
        }
        startPoint = new Vector2(transform.position.x, transform.position.y);

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

        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
            new Vector2(startPoint.x, startPoint.y));
        if (distance > 20)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            if (effect != null)
            {
                effect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); 
            }
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
        InvokeRepeating("del", 0.5f, 0);
    }
    private void del()
    {
        Destroy(gameObject);
    }
    public void SetLocalScale(float scaleX)
    {
        localScaleX = scaleX;
       
    }
    public void UpdateRotation()
    {
        if (effect == null)
        {
            
            
        }
        else
        {

            // Set the start rotation of the Particle System to match the parent object's rotation
            ParticleSystem.MainModule mainModule = effect.main;
            int direction = localScaleX > 0 ? 1 : -1;
            mainModule.startRotation = direction > 0 ? 0 : 180 * Mathf.Deg2Rad;
            
        }
    }
}
