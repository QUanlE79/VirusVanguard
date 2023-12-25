using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    public TMP_Text coinAmountText;
    CoinManager coinManager;
     
    // Start is called before the first frame update
    void Start()
    {
        coinManager = CoinManager.instance;
        coinAmountText.text = coinManager.coinCount.ToString()+" x";
        coinManager.onCoinChanged += OnCoinChanged;
    }

    private void OnCoinChanged()
    {
        coinAmountText.text = (coinManager.coinCount).ToString() + " x";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
