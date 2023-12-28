using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TooltipSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static TooltipSystem instance;
    public ToolTip toolTip;
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
        toolTip = GetComponentInChildren<ToolTip>(includeInactive: true);
        if(toolTip == null)
        {
            Debug.Log("Toll");
        }
        // This method will be called whenever a new scene is loaded
        Debug.Log("Scene loaded: " + scene.name);
    }
    private void Awake()
    {
        instance= this;
        toolTip=GetComponentInChildren<ToolTip>(includeInactive: true);
    }
    public static void ShowTooltip(string name,int atk,int def)
    {
        Debug.Log(name);
        instance.toolTip.Name.text = name;
        instance.toolTip.ATK.text = atk.ToString();
        instance.toolTip.DEF.text = def.ToString();
        instance.toolTip.gameObject.SetActive(true);
    }

    public static void HideTooltip()
    {
        instance.toolTip.gameObject.SetActive(false);
    }
}
