using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    private CollectAble itemInRange;

    void Update()
    {
        if (itemInRange != null && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem(itemInRange);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CollectAble collectable))
        {
            itemInRange = collectable;
            // MÙûeö zobraziù UI "Press E to pick up"
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CollectAble collectable))
        {
            if (itemInRange == collectable)
                itemInRange = null;
        }
    }

    void CollectItem(CollectAble collectable)
    {
        InventoryUI.instance.AddItem(collectable.itemName);
        Destroy(collectable.gameObject);
        itemInRange = null;
    }
}

