﻿
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
        onItemChangedCallback += UpdateUI;
    }

    private void UpdateUI()
    {
        if (InventoryUI.Instance != null)
        {
            InventoryUI.Instance.UpdateUI();
        }
    }

    #endregion

    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;  // Amount of slots in inventory

    // Current list of items in inventory
    public List<Equipment> items = new List<Equipment>();

    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public bool Add(Equipment item)
    {

        // Don't do anything if it's a default item
        if (!item.isDefaultItem)
        {
            // Check if out of space
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            items.Add(item);    // Add item to list

            // Trigger callback
            if (onItemChangedCallback != null)
            {
                Debug.Log("CC");
                onItemChangedCallback.Invoke();
            }

        }

        return true;
    }

    // Remove an item
    public void Remove(Equipment item)
    {
        items.Remove(item);     // Remove item from list

        // Trigger callback
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    public void AddItems(List<Equipment> items)
    {
        foreach (Equipment item in items)
        {
            Add(item);
        }
    }
    public void SaveInventory(string filePath)
    {
        InventoryListWrapper wrapper = new InventoryListWrapper
        {
            inventoryList = new List<EquipmentData>()
        };

        foreach (Equipment item in items)
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

            wrapper.inventoryList.Add(itemData);
        }

        string json = JsonUtility.ToJson(wrapper);
        System.IO.File.WriteAllText(filePath, json);
    }

    // Load inventory from a file into the inventory
    public void LoadInventory(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            InventoryListWrapper wrapper = JsonUtility.FromJson<InventoryListWrapper>(json);

            // Clear current inventory before adding loaded items
            items.Clear();

            foreach (EquipmentData itemData in wrapper.inventoryList)
            {
                Equipment item = ScriptableObject.CreateInstance<Equipment>();
                item.name = itemData.name;
                item.icon = AssetDatabase.LoadAssetAtPath<Sprite>(itemData.iconPath);  // Use the path or other identifier
                item.isDefaultItem = itemData.isDefaultItem;
                // Add other necessary fields here
                item.damageModifier = itemData.damageModifier;
                item.armorModifier = itemData.armorModifier;
                item.equipSlot = itemData.equipSlot;
                item.EquipmentPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(itemData.equipmentPrefabPath);
                item.ItemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(itemData.itemPrefabPath);
                Add(item);
            }
        }
    }
}
[System.Serializable]
public class InventoryListWrapper
{
    public List<EquipmentData> inventoryList;
}