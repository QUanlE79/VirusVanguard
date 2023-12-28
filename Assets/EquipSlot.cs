
using UnityEngine;
using UnityEngine.UI;


public class EquipSlot : MonoBehaviour
{
    public Image icon;          // Reference to the Icon image
    public Button removeButton;
    public EquipmentSlot slotIndex;
    Equipment equipment;
    // Start is called before the first frame update
    void Start()
    {
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnRemoveButton()
    {
        EquipmentManager.instance.Unequip((int)equipment.equipSlot);
    }

    public void AddItem(Equipment newEquipment)
    {
        equipment = newEquipment;

        icon.sprite = newEquipment.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    // Clear the slot
    public void ClearSlot()
    {
        equipment = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }
}
