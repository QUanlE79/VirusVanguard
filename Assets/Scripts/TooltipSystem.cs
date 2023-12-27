using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static TooltipSystem instance;
    public ToolTip toolTip;
    private void Awake()
    {
        instance= this;
    }
    public static void ShowTooltip(string name,int atk,int def)
    {
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
