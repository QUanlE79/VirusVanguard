using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerDamageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    Animator animator;
    public Stat damage;
    public Stat armor;
    VirusController controller;
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
    private int curWeaponDamage;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller=GetComponent<VirusController>();
    }
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        //curWeaponDamage=EquipmentManager.instance.GetCurWeaponDamage();
    }
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        
        // Add new modifiers
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        // Remove old modifiers
        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }

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
        
        if (isAlive && !isInvincible && !controller.isDashing)
        {
            damage -= armor.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            health -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimationString.hit);
            LockVelocity= true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.CharacterTookDamaged.Invoke(gameObject, damage);
            if (health < 0)
            {
                health = 0;
            }
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
    public PlayerDamageableData SavePlayerDamageableData()
    {
        //damage.RemoveModifier(curWeaponDamage);
        return new PlayerDamageableData(this);
    }

    public void LoadPlayerDamageableData(PlayerDamageableData loadedData)
    {
        this.MaxHealth = loadedData.maxHealth;
        this.health = loadedData.health;
        this.damage.SetModifiers(loadedData.damageModifiers);
        this.armor.SetModifiers(loadedData.armorModifiers);
        YuriaScript.UpGradeTime = loadedData.upgradeHPTime;
        OrrnScript.UpGradeTime = loadedData.upgradeATKTime;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            YuriaScript.instance.UpdateHPBar();
            OrrnScript.instance.UpdateAtkBar();
        }
        
        CoinManager.instance.AddCoins(loadedData.cointAmount);
        //CoinManager.instance.onCoinChanged += OnCoinChanged;
        
        // Load other data as needed
    }

   
   
}
