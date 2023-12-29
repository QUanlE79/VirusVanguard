using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    private static string savePath = "playerDamageableData.json";

    public static void SavePlayerDamageableData(PlayerDamageableData playerData)
    {
        string jsonData = JsonUtility.ToJson(playerData);
        File.WriteAllText(savePath, jsonData);
    }
    public static void DeletePlayerDamageableData()
    {
        
        File.WriteAllText(savePath, "{\"maxHealth\":100,\"health\":100,\"damageModifiers\":[10],\"armorModifiers\":[],\"upgradeHPTime\":0,\"upgradeATKTime\":0,\"cointAmount\":0}");
    }
    public static PlayerDamageableData LoadPlayerDamageableData()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            Debug.Log(jsonData);
            return JsonUtility.FromJson<PlayerDamageableData>(jsonData);
        }
        else
        {
            Debug.LogWarning("Save file not found.");
            return null;
        }
    }
    public static void LoadEquipmentAtStart()
    {
        // Replace the following with your actual equipment data

        // Load equipment into the equipment manager
        EquipmentManager.instance.LoadEquipment("EquipmentData.json");

        // Load inventory from a file
        Inventory.instance.LoadInventory("InventoryData.json");
    }
    public static void SaveEquipmentAtEnd()
    {
        // Replace the following with your actual equipment data

        // Load equipment into the equipment manager
        EquipmentManager.instance.SaveEquipment("EquipmentData.json");

        // Load inventory from a file
        Inventory.instance.SaveInventory("InventoryData.json");
    }
}
