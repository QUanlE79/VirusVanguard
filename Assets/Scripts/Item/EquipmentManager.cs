using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

/* Keep track of equipment. Has functions for adding and removing items. */

public class EquipmentManager : MonoBehaviour {

	#region Singleton

    //public enum MeshBlendShape {Torso, Arms, Legs };
    public Equipment[] defaultEquipment;
	
	public static EquipmentManager instance;
	GameObject player;
	VarusArrowController weapon;
	//public SkinnedMeshRenderer targetMesh;

 //   SkinnedMeshRenderer[] currentMeshes;

	void Awake ()
	{
		instance = this;
		player = GameObject.FindGameObjectWithTag("Player");
		weapon=player.GetComponent<VarusArrowController>();
		
	}

	#endregion

	Equipment[] currentEquipment;   // Items we currently have equipped

	// Callback for when an item is equipped/unequipped
	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public OnEquipmentChanged onEquipmentChanged;
	public Transform equipmentParent;

	Inventory inventory;    // Reference to our inventory
	EquipSlot[] slots;
	void Start ()
	{
		inventory = Inventory.instance;     // Get a reference to our inventory
        slots = equipmentParent.GetComponentsInChildren<EquipSlot>();
        // Initialize currentEquipment based on number of equipment slots
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
        //currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaults();
	}

	// Equip a new item
	public void Equip (Equipment newItem)
	{
		// Find out what slot the item fits in
		if (newItem == null)
			return;
		int slotIndex = (int)newItem.equipSlot;
		
        Equipment oldItem = Unequip(slotIndex);

		// An item has been equipped so we trigger the callback
		if (onEquipmentChanged != null)
		{
			onEquipmentChanged.Invoke(newItem, oldItem);
		}

		// Insert the item into the slot
		if (newItem.equipSlot == EquipmentSlot.Weapon)
		{
			weapon.projectilePrefab = newItem.EquipmentPrefab;
		}
		currentEquipment[slotIndex] = newItem;
        slots[slotIndex].AddItem(newItem);
        //AttachToMesh(newItem, slotIndex);
    }

	// Unequip an item with a particular index
	public Equipment Unequip (int slotIndex)
	{
        Equipment oldItem = null;
		// Only do this if an item is there
		if (currentEquipment[slotIndex] != null)
		{
			// Add the item to the inventory
			oldItem = currentEquipment[slotIndex];
			inventory.Add(oldItem);

           
			// Remove the item from the equipment array
			currentEquipment[slotIndex] = null;
			slots[slotIndex].ClearSlot();
			Equip(defaultEquipment[slotIndex]);
            // Equipment has been removed so we trigger the callback
            if (onEquipmentChanged != null)
			{
                onEquipmentChanged.Invoke(null, oldItem);
			}
			
		}
        
        return oldItem;
	}

	// Unequip all items
	public void UnequipAll ()
	{
		if (currentEquipment.Length > 0)
		{
            for (int i = 0; i < currentEquipment.Length; i++)
            {
				if (currentEquipment[i] != null)
				{
                    Unequip(i);
                }
               
            }
        }
		

        EquipDefaults();
	}

    public void UpdateUI()
    {
        //for (int i = 0; i < slots.Length; i++)
        //{
        //    if (i < currentEquipment.Length)  // If there is an item to add
        //    {
        //        slots[i].AddItem(inventory.items[i]);   // Add it
        //    }
        //    else
        //    {
        //        // Otherwise clear the slot
        //        slots[i].ClearSlot();
        //    }
        //}
    }

    void EquipDefaults()
    {
		foreach (Equipment e in defaultEquipment)
		{
			if (e != null)
			{
                Equip(e);
            }
			
		}
    }

	void Update ()
	{
		// Unequip all items if we press U
		if (Input.GetKeyDown(KeyCode.U))
			UnequipAll();
	}

    public void SaveEquipment(string filePath)
    {
        EquipmentListWrapper wrapper = new EquipmentListWrapper
        {
            equipmentList = new List<EquipmentData>()
        };
        foreach (Equipment item in currentEquipment)
        {
            if(item != null)
            {
                EquipmentData itemData = new EquipmentData
                {
                    name = item.name,
                    iconPath = AssetDatabase.GetAssetPath(item.icon),  // Use the path or other identifier
                    isDefaultItem = item.isDefaultItem,
                    // Add other necessary fields here
                    damageModifier = item.damageModifier,
                    armorModifier = item.armorModifier,
                    equipSlot = item.equipSlot,
                    equipmentPrefabPath = item.EquipmentPrefab != null ? AssetDatabase.GetAssetPath(item.EquipmentPrefab) : "",
                    itemPrefabPath = item.ItemPrefab != null ? AssetDatabase.GetAssetPath(item.ItemPrefab) : ""
                };

                wrapper.equipmentList.Add(itemData);
                
            }
           
        }

        string json = JsonUtility.ToJson(wrapper);
        System.IO.File.WriteAllText(filePath, json);
    }

    // Load equipment from a file into the equipment manager
    public void LoadEquipment(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            
            EquipmentListWrapper wrapper = JsonUtility.FromJson<EquipmentListWrapper>(json);

            // Clear current equipment before adding loaded equipment
			if(wrapper != null && wrapper.equipmentList.Count> 0)
			{
                UnequipAll();
            }
            

            foreach (EquipmentData equipment in wrapper.equipmentList)
            {
                Equipment item = ScriptableObject.CreateInstance<Equipment>();
                item.name = equipment.name;
                item.icon = AssetDatabase.LoadAssetAtPath<Sprite>(equipment.iconPath);  // Use the path or other identifier
                item.isDefaultItem = equipment.isDefaultItem;
                // Add other necessary fields here
                item.damageModifier = equipment.damageModifier;
                item.armorModifier = equipment.armorModifier;
                item.equipSlot = equipment.equipSlot;
                item.EquipmentPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(equipment.equipmentPrefabPath);
                item.ItemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(equipment.itemPrefabPath);
                Equip(item);
                
            }
        }
    }
    public int GetCurWeaponDamage()
    {
        return currentEquipment[(int)EquipmentSlot.Weapon].damageModifier;
    }
}
[System.Serializable]
public class EquipmentListWrapper
{
    public List<EquipmentData> equipmentList;
}
