using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    InventorySlot inventorySlot;
    private void Awake()
    {
        inventorySlot = GetComponent<InventorySlot>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventorySlot.item)
        {
            Debug.Log(inventorySlot.item.name);
            TooltipSystem.ShowTooltip(inventorySlot.item.name, inventorySlot.item.damageModifier,inventorySlot.item.armorModifier);

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.HideTooltip();
    }
}
