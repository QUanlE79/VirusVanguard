using UnityEngine;
using UnityEngine.InputSystem;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour
{

    public Transform itemsParent;   // The parent object of all the items
    public GameObject inventoryUI;  // The entire UI
    private CanvasGroup inventoryCanvasGroup;
    Inventory inventory;    // Our current inventory

    InventorySlot[] slots;  // List of all the slots
    private void Awake()
    {
        
    }
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        
          // Subscribe to the onItemChanged callback

        // Populate our slots array
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        Transform inventoryTransform = inventoryUI.transform; // Replace "Inventory" with the actual name of your inventory UI GameObject

        // Get the CanvasGroup component
        inventoryCanvasGroup = inventoryTransform.GetComponent<CanvasGroup>();

        // Hide the inventory UI at the start
        HideInventory();
    }

    void Update()
    {
        // Check to see if we should open/close the inventory
        
    }

    // Update the inventory UI by:
    //		- Adding items
    //		- Clearing empty slots
    // This is called using a delegate on the Inventory.
    void UpdateUI()
    {
        // Loop through all the slots
        
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)  // If there is an item to add
            {
                slots[i].AddItem(inventory.items[i]);   // Add it
            }
            else
            {
                // Otherwise clear the slot
                slots[i].ClearSlot();
            }
        }
    }
    public void onOpen_Close(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
           ToggleInventoryVisibility();
            //
        }
    }
    private void HideInventory()
    {
        // Set alpha to 0 (completely transparent) and disable interactability
        inventoryCanvasGroup.alpha = 0f;
        inventoryCanvasGroup.interactable = false;
        inventoryCanvasGroup.blocksRaycasts = false;
    }

    private void ShowInventory()
    {
        // Set alpha to 1 (fully opaque) and enable interactability
        inventoryCanvasGroup.alpha = 1f;
        inventoryCanvasGroup.interactable = true;
        inventoryCanvasGroup.blocksRaycasts = true;
    }

    private void ToggleInventoryVisibility()
    {
        if (inventoryCanvasGroup.alpha > 0f)
        {
            HideInventory();
            
        }
        else
        {
            ShowInventory();
           
        }
    }
}