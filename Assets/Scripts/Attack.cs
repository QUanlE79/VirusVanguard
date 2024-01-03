using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
   
{
    public int damage = 10;
    public Vector2 knockback = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        int curstage = SceneManager.GetActiveScene().buildIndex;
        if (curstage > 4)
        {
            damage = damage + 7;
        }
        if (curstage > 6)
        {
            damage = damage + 10;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(knockback.x, knockback.y)* new Vector2(-1,1);
        PlayerDamageable damageable= collision.GetComponent<PlayerDamageable>();
        if (damageable != null)
        {
            damageable.Hit(damage, deliveredKnockback);
            //Debug.Log(collision.name + "hit for " + damage);
        }
    }
}
