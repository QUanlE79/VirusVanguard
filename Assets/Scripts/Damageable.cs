using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
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
            healthChanged?.Invoke(_health, MaxHealth);
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

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationString.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationString.lockVelocity, value);
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
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        Debug.Log(damage);
        if (isAlive && !isInvincible)
        {
            health -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimationString.hit);
            LockVelocity= true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.CharacterTookDamaged.Invoke(gameObject, damage);
            return true;
        }
        return false;
    }
    public bool Health(int healthRestore)
    {
        if (isAlive && health<MaxHealth)
        {
            int maxheal = Mathf.Max(MaxHealth - health, 0);
            int actualHeal = Mathf.Min(maxheal, healthRestore);
            health += actualHeal;
            CharacterEvents.CharacterHealthed.Invoke(gameObject, actualHeal);
            return true;
        }
        return false;
    }
}
