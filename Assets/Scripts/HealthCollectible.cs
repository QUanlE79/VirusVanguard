using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int healAmount = 20;
    public Vector3 spinRotationSpeed= new Vector3(0,180,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable= collision.GetComponent<Damageable>();
        if (damageable)
        {
            bool washealed=damageable.Health(healAmount);
            if (washealed)
            {
                Destroy(gameObject);
            }
        }
    }
}