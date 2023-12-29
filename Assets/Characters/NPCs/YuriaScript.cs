using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YuriaScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static YuriaScript instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        damageable = player.GetComponent<PlayerDamageable>();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HPBar != null && PriceText!=null)
        {
            UpdateHPBar();
        }
        
    }
    public Image HPBar;
    public TMP_Text HPText;
    public TMP_Text PriceText;
    PlayerDamageable damageable;
    public static int UpGradeTime=0;
    public void UpdateHPBar()
    {
        if (UpGradeTime <= 10)
        {
            HPBar.fillAmount = UpGradeTime / 10f;
            //int crrAtk = damageable.damage.GetValue();
            int crrHP = damageable.MaxHealth;
            //Debug.Log(crrAtk);
            HPText.text = crrHP.ToString();
            PriceText.text = (50 * UpGradeTime + 1).ToString();
        }
    }
    public void UpdateHP()
    {
        int price = 50 * (UpGradeTime + 1);
        if (UpGradeTime < 10 && (CoinManager.instance.coinCount > price))
        {
            UpGradeTime++;
            damageable.MaxHealth += 50;
            damageable.health += 50;
            CoinManager.instance.SpendCoins(50 * (UpGradeTime + 1));
        }
    }
    
}
