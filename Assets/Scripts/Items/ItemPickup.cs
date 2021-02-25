using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;

    public override void Interact()
    {
        base.Interact();
        Pickup();
    }

    void Pickup()
    {
        Debug.Log("Picking up " + item.name);

        bool itemWasPickedUp = Inventory.instance.Add(item);
        if(itemWasPickedUp)
            // gameObject.SetActive(false); // Instead of destroying an object just deactivate it, so when dropped there's no need to create a new one
            Destroy(gameObject);
    }
}
