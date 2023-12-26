using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{

    public Image icon;          // Reference to the Icon image
    public Button removeButton; // Reference to the remove button
    GameObject player;
    Item item;  // Current item in the slot
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Add item to the slot
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    // Clear the slot
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    // Called when the remove button is pressed
    public void OnRemoveButton()
    {
        int direction = player.transform.localScale.x > 0 ? 1 : -1;
        Debug.Log(direction);
        Vector3 spawnPoint = new Vector3((player.transform.position.x + direction), player.transform.position.y, 0);
        if(item!=null )
        {
            GameObject newitem = Instantiate(item.ItemPrefab, spawnPoint, Quaternion.identity);
            Debug.Log(newitem.transform.position);
        }
        else
        {
            Debug.Log("CC");
        }
        Inventory.instance.Remove(item);
    }

    // Called when the item is pressed
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

}   