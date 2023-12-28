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
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        damageable = player.GetComponent<PlayerDamageable>();
       
    }
    void Start()
    {
       
        BgMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAtkBar();
    }
    public Image AtkBar;
    public TMP_Text AtkText;
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
        }

    }
    public void UpdateDmg()
    {
        if (UpGradeTime < 10)
        {
            UpGradeTime++;
            damageable.damage.AddModifier(10);
        }

    }
}
