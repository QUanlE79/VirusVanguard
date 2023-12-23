using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public Vector2 knockback = Vector2.zero;
    public int damage = 10;

    private void OnTriggerStay2D(Collider2D collision) {
        // VirusController controller = other.GetComponent<VirusController>();
        // if(controller != null){
        //     controller.onHit(1,Vector2.zero);
        // }
        // Debug.Log(controller.name);
        Damageable damageable= collision.GetComponent<Damageable>();
        if (damageable != null)
        {

            damageable.Hit(damage, knockback);
            Debug.Log(collision.name + "hit for " + damage);
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision) {
    //     // VirusController controller = other.GetComponent<VirusController>();
    //     // if(controller != null){
    //     //     controller.onHit(1,Vector2.zero);
    //     // }
    //     // Debug.Log(controller.name);
    //     Damageable damageable= collision.GetComponent<Damageable>();
    //     if (damageable != null)
    //     {

    //         damageable.Hit(damage, knockback);
    //         Debug.Log(collision.name + "hit for " + damage);
    //     }
    // }
}
