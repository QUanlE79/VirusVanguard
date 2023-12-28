using UnityEngine;

[System.Serializable]
public class EquipmentData
{
    public string name;
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int damageModifier;
    public string iconPath;
    public string equipmentPrefabPath;
    public string itemPrefabPath;
    public bool isDefaultItem;
    //public EquipmentData(Equipment equipment)
    //{
    //    name = equipment.name;
    //    equipSlot = equipment.equipSlot;
    //    armorModifier = equipment.armorModifier;
    //    damageModifier = equipment.damageModifier;
    //    isDefaultItem=equipment.isDefaultItem;

    //    // Add other fields as needed
    //}
}