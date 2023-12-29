using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject armor;
    public GameObject helmet;
    public GameObject gloves;
    public GameObject arrow;
     bool isAlive;
    GameObject boss;
    private bool isDropped=false;
    QSScript qs;
    BossDDScript dd;
    BossTDScript td;
    private void Awake()
    {
         qs=GetComponent<QSScript>();  
         dd=GetComponent<BossDDScript>();
         td=GetComponent<BossTDScript>();
         isDropped = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (qs != null)
        {
            if (!qs.isAlive && !isDropped)
            {
                Vector3 spawnpoint=new Vector3(transform.position.x + 0.5f, transform.position.y, 0);
                Instantiate(armor, spawnpoint, Quaternion.identity);
                spawnpoint = new Vector3(transform.position.x + 0.6f, transform.position.y, 0);
                Instantiate(gloves, spawnpoint, Quaternion.identity);
                spawnpoint = new Vector3(transform.position.x + 0.7f, transform.position.y, 0);
                Instantiate(helmet, spawnpoint, Quaternion.identity);
                spawnpoint = new Vector3(transform.position.x + 0.8f, transform.position.y, 0);
                if(arrow!=null)
                {
                    Instantiate(arrow, spawnpoint, Quaternion.identity);
                }
                
                isDropped=true;
            }
        }
        else if (dd != null)
        {
            if (!dd.isAlive && !isDropped)
            {
                Debug.Log("DD");
                Vector3 spawnpoint = new Vector3(transform.position.x + 0.5f, transform.position.y, 0);
                Instantiate(armor, spawnpoint, Quaternion.identity);
                spawnpoint = new Vector3(transform.position.x + 0.6f, transform.position.y, 0);
                Instantiate(gloves, spawnpoint, Quaternion.identity);
                spawnpoint = new Vector3(transform.position.x + 0.7f, transform.position.y, 0);
                Instantiate(helmet, spawnpoint, Quaternion.identity);
                spawnpoint = new Vector3(transform.position.x + 0.8f, transform.position.y, 0);
                if (arrow != null)
                {
                    Instantiate(arrow, spawnpoint, Quaternion.identity);
                }
                isDropped = true;
            }
        }
        else if (td != null)
        {
            if (!td.isAlive && !isDropped )
            {
                Vector3 spawnpoint = new Vector3(transform.position.x + 0.5f, transform.position.y, 0);
                Instantiate(armor, spawnpoint, Quaternion.identity);
                spawnpoint = new Vector3(transform.position.x + 0.6f, transform.position.y, 0);
                Instantiate(gloves, spawnpoint, Quaternion.identity);
                spawnpoint = new Vector3(transform.position.x + 0.7f, transform.position.y, 0);
                Instantiate(helmet, spawnpoint, Quaternion.identity);
                spawnpoint = new Vector3(transform.position.x + 0.8f, transform.position.y, 0);
                if (arrow != null)
                {
                    Instantiate(arrow, spawnpoint, Quaternion.identity);
                }
                isDropped = true;
            }
        }
    }
}
