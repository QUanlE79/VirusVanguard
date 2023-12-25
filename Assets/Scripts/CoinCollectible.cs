using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    // Start is called before the first frame update
    public int coinsAmount = 10;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        CoinManager.instance.AddCoins(coinsAmount);
        Destroy(gameObject);
    }

}
