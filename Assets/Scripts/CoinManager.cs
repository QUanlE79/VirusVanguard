using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public int coinCount;

    public delegate void OnCoinChanged();
    public OnCoinChanged onCoinChanged;
    private void Awake()
    {
        instance = this;
    }
    // Method to add coins
    public void AddCoins(int amount)
    {
        coinCount += amount;
        onCoinChanged.Invoke();
        // You can also add logic here to update the UI or perform other actions when coins are added
    }

    // Method to spend coins
    public bool SpendCoins(int amount)
    {
        if (coinCount >= amount)
        {
            coinCount -= amount;
            // You can also add logic here to update the UI or perform other actions when coins are spent
            return true; // Return true to indicate that the coins were successfully spent
        }
        else
        {
            return false; // Return false to indicate that the player does not have enough coins
        }
    }
}
