using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrrnScript : MonoBehaviour
{
    public AudioSource BgMusic;
    public static OrrnScript instance;
    //CoinManager CoinManager;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        damageable = player.GetComponent<PlayerDamageable>();
        //CoinManager = player.GetComponent<CoinManager>();
    }
    void Start()
    {
        BgMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (AtkBar != null && PriceText != null)
        {
            UpdateAtkBar();
        }
        
    }
    public Image AtkBar;
    public TMP_Text AtkText;
    public TMP_Text PriceText;
    PlayerDamageable damageable;
    public static int UpGradeTime=0;
    public void UpdateAtkBar()
    {
        if (UpGradeTime <= 10)
        {
            AtkBar.fillAmount = UpGradeTime / 10f;

            int crrAtk = damageable.damage.GetValue();
            //Debug.Log(crrAtk);
            AtkText.text = crrAtk.ToString();
            PriceText.text = (50 * (UpGradeTime + 1)).ToString();
        }

    }
    public void UpdateDmg()
    {
        int price = 50 * (UpGradeTime + 1);
        if (UpGradeTime < 10 && (CoinManager.instance.coinCount >= price))
        {
            UpGradeTime++;
            damageable.damage.AddModifier(10);
            CoinManager.instance.SpendCoins(price);
        }
        else
        {

        }
    }
}
