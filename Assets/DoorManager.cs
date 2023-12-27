using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    PlayerDamageable damageable;
    private void Awake()
    {
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        damageable = collision.GetComponent<PlayerDamageable>();
        PlayerDamageableData data = new PlayerDamageableData(damageable);
        FileManager.SavePlayerDamageableData(data);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
