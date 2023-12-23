using UnityEngine;

public class ItemPickup : MonoBehaviour {

	public Item item;	// Item to put in the inventory on pickup

	// When the player interacts with the item
	//public override void Interact()
	//{
	//	base.Interact();

	//	PickUp();	// Pick it up!
	//}

	// Pick up the item
	void PickUp ()
	{
		Debug.Log("Picking up " + item.name);
		bool wasPickedUp = Inventory.instance.Add(item);	// Add to inventory

		// If successfully picked up
		if (wasPickedUp)
			Destroy(gameObject);	// Destroy item from scene
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
		Debug.Log("item interact");
        PickUp();
    }
}
