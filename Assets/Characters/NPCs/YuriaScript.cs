using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YuriaScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        damageable = player.GetComponent<PlayerDamageable>();
        UpGradeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHPBar();
    }
    public Image HPBar;
    public TMP_Text HPText;
    PlayerDamageable damageable;
    public int UpGradeTime;
    public void UpdateHPBar()
    {
        if (UpGradeTime <= 10)
        {
            HPBar.fillAmount = UpGradeTime / 10f;

            //int crrAtk = damageable.damage.GetValue();
            int crrHP = damageable.MaxHealth;
            //Debug.Log(crrAtk);
            HPText.text = crrHP.ToString();
        }

    }
    public void UpdateHP()
    {
        if (UpGradeTime < 10)
        {
            UpGradeTime++;
            damageable.MaxHealth += 20;
            damageable.health = damageable.MaxHealth;
        }

    }
}
