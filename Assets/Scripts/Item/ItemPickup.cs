using UnityEngine;

public class ItemPickup : MonoBehaviour {

	public Equipment item;   // Item to put in the inventory on pickup
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    // When the player interacts with the item
    //public override void Interact()
    //{
    //	base.Interact();

    //	PickUp();	// Pick it up!
    //}

    // Pick up the item
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
    void PickUp ()
	{
		Debug.Log("Picking up " + item.name);
		bool wasPickedUp = Inventory.instance.Add(item);	// Add to inventory

		// If successfully picked up
		if (wasPickedUp)
			Destroy(gameObject);	// Destroy item from scene
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
		if(collision.gameObject.GetComponent<PlayerDamageable>() != null)
        {
            PickUp();
        }
       
    }
}
