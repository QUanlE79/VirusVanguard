using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    PlayerDamageable damageable;
    public static int CurStage;
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
        FileManager.SaveEquipmentAtEnd();
        damageable = collision.GetComponent<PlayerDamageable>();
        PlayerDamageableData data = new PlayerDamageableData(damageable);
        FileManager.SavePlayerDamageableData(data);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnEnable()
    {
        // Subscribe to the SceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {

        // Unsubscribe from the SceneLoaded event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurStage=SceneManager.GetActiveScene().buildIndex;
        // This method will be called whenever a new scene is loaded
        Debug.Log("Scene loaded: " + scene.name);
    }
}
