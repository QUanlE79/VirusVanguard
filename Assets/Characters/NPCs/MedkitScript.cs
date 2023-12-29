using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Canvas Dialog;
    PlayerDamageable damageable;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Dialog.gameObject.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        damageable = player.GetComponent<PlayerDamageable>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecoverHP()
    {
        int price = 50;
        if ((CoinManager.instance.coinCount > price))
        {
            damageable.health = damageable.MaxHealth;
            CoinManager.instance.SpendCoins(50);
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dialog.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dialog.gameObject.SetActive(true);
        }

    }
}
