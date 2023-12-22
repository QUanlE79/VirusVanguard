using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarController : MonoBehaviour
{
    public Image FrontHB;
    public Image BackHB;
    public TMP_Text HealthAmountText;
    public float chipSeed= 2f;
    Damageable damageable;
    float lerpTimer = 0f;
    bool isHealthChanged=false;
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
        damageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }
    private void OnDisable()
    {
        damageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateHealthBar(damageable.health, damageable.MaxHealth);
    }
    private void UpdateHealthBar(float health, float MaxHealth)
    {
        //Debug.Log(damageable.health);
        float fillF = FrontHB.fillAmount;
        float fillB = BackHB.fillAmount;
        float hFraction= health / MaxHealth;
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
        HealthAmountText.text = hFraction*100+ "%";
    }
    private void OnPlayerHealthChanged(int health, int MaxHealth)
    {
        UpdateHealthBar(health, MaxHealth);
    }
}
