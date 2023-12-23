using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
   
{
    public int damage = 10;
    public Vector2 knockback = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(knockback.x, knockback.y)* new Vector2(-1,1);
        Damageable damageable= collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.Hit(damage, deliveredKnockback);
            //Debug.Log(collision.name + "hit for " + damage);
        }
    }
}
