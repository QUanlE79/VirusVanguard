using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Canvas Dialog;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Dialog.gameObject.SetActive(false);
        
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }

    public Canvas ModalUpgrade;

    public void DisplayDialog()
    {
        if (ModalUpgrade != null)
        {
            if (!ModalUpgrade.gameObject.activeSelf)
            {
                // If Modal is not active, make it active.
                ModalUpgrade.gameObject.SetActive(true);
            }
            else
            {
                // If Modal is active, make it inactive.
                ModalUpgrade.gameObject.SetActive(false);
            }
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
