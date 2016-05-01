using UnityEngine;
using System.Collections;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public RapunzelManager RapunzelManager;

    [Tooltip("who consumes this inventory item")]
    public string CollectableByTag = "Player";
    
    public CollectableItems ItemType;

    [Tooltip("How many get added or removed from a colliding inventory")]
    public int NumberOfItems;

    void Start()
    {

    }
    
    void Update()
    {

    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(this.name + "'s Inventory collided with " + collision.gameObject.name);
        if (collision.gameObject.tag == CollectableByTag)
        {
            yield return null; // wait a frame to let PlaysAudio check current item levels before this modifies RapunzelManager.PlayerInventory
            if (NumberOfItems > 0) // add NumberOfItems of ItemType to the players inventory
                for (int i = 0; i < NumberOfItems; i++)
                    RapunzelManager.PlayerInventory.Add(ItemType);
            else if (NumberOfItems < 0 && hasNumber(ItemType, -1 * NumberOfItems)) // remove NumberOfItems of ItemType from the players inventory
                for (int i = 0; i > NumberOfItems; i--)
                    RapunzelManager.PlayerInventory.Remove(ItemType);
            else if (NumberOfItems == 0) // HACK: allow Inventory to be used for destructable items, such as doors
                ;
            else
                yield break;
            Destroy(this.gameObject);
        }
    }

    private bool hasNumber(CollectableItems itemType, int requiredQuanity)
    {
        var numInInventory = RapunzelManager.PlayerInventory.Where(i => i == itemType).Count();
        return numInInventory >= requiredQuanity;
    }
}
