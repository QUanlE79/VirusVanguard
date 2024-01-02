using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHBController : MonoBehaviour
{
    public Image FrontHB;
    public Image BackHB;
    public TMP_Text HealthAmountText;
    public float chipSeed = 2f;
    Damageable damageable;
    float lerpTimer = 0f;

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        damageable.healthChanged.AddListener(OnHealthChanged);
    }
    private void OnDisable()
    {
        damageable.healthChanged.RemoveListener(OnHealthChanged);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateHealthBar(damageable.health, damageable.MaxHealth);
    }
    private void UpdateHealthBar(float health, float MaxHealth)
    {

        float fillF = FrontHB.fillAmount;
        float fillB = BackHB.fillAmount;
        float hFraction = health / MaxHealth;
        //FrontHB.fillAmount = hFraction;
        if (fillB > hFraction)
        {
            FrontHB.fillAmount = hFraction;
            BackHB.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSeed;
            BackHB.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            BackHB.color = Color.green;
            BackHB.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSeed;
            FrontHB.fillAmount = Mathf.Lerp(fillF, BackHB.fillAmount, percentComplete);
        }
        //isHealthChanged = false;
        HealthAmountText.text = (int)(hFraction * 100) + "%";
    }
    private void OnHealthChanged(int health, int MaxHealth)
    {
        UpdateHealthBar(health, MaxHealth);
    }
}
