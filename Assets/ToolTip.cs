using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ToolTip : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text Name;
    public TMP_Text ATK;
    public TMP_Text DEF;
    public LayoutElement layoutElement;
    public int characterWrapLimit;
    InputSystemUIInputModule inputModule;
    private void Update()
    {
        Vector2 position= Input.mousePosition;
        transform.position = position;
    }
    
}
