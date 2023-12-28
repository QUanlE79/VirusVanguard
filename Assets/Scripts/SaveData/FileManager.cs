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

    public static PlayerDamageableData LoadPlayerDamageableData()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            return JsonUtility.FromJson<PlayerDamageableData>(jsonData);
        }
        else
        {
            Debug.LogWarning("Save file not found.");
            return null;
        }
    }
}
