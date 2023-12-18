using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed= new Vector3(0,75,0);
    public float timeToFade = 1f;
    TextMeshProUGUI textMeshPro;
    RectTransform rectTransform;
    private float timeElapsed = 0f;
    private Color startColor;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform= GetComponent<RectTransform>();
        textMeshPro= GetComponent<TextMeshProUGUI>();
        startColor=textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed+= Time.deltaTime;

        if(timeElapsed<timeToFade)
        {
            float fadeAlpha=startColor.a*(1-(timeElapsed/timeToFade));
            textMeshPro.color = new Color(startColor.r,startColor.g,startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
