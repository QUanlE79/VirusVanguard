using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    [SerializeField]
    private int _maxhealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxhealth;
        }
        set
        {
            _maxhealth = value;
        }
    }
    [SerializeField]
    private int _health = 100;
    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if(_health <= 0 )
            {
                isAlive = false;
            }
        }
    }
    [SerializeField]
    private bool _isAlive = true;
    private bool isInvincible = false;
    private float timeSinceHit=0;
    public float InvincibilityTime = 0.25f;
    public bool isAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationString.isAlive,value);
            Debug.Log("isAlive:" + value);
        }
    }

   

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
           
            if (timeSinceHit > InvincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
        Hit(10);
    }

    public void Hit(int damage)
    {
        if (isAlive && !isInvincible)
        {
            health -= damage;
            isInvincible = true;
        }

    }
}
