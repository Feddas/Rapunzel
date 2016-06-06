using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// Items that can be merged into an inventory
/// </summary>
public enum CollectableItems
{
    Coin,
    Key,
}

[Serializable]
public class ItemStacks
{
    public CollectableItems Item;  // the item Id
    public int Quantity = 1;       // data of the item, currently only how many are in the stack
}

/// <summary>
/// Collectable inventory.
/// If successfully merged with a CollectableByTag's inventory, this gameobject gets destroyed.
/// Merge can be unsuccessful if an ItemStack Quanity is negative.
/// </summary>
public class Inventory : MonoBehaviour
{
    [Tooltip("GameObject Tag check of who can consume this inventory item")]
    public string CollectableByTag = "Player";

    [Tooltip("This should be a dictionary, but then it'd be harder to show it in the Unity3d inspector")]
    public List<ItemStacks> MyInventory;
    public Dictionary<CollectableItems, int> DictMyInventory { get; set; }
    
    void Start()
    {
        DictMyInventory = MyInventory.ToDictionary(i => i.Item, i => i.Quantity);
    }
    
    void Update()
    {
    }

    public void UpdateInspectorInventory() // update Unity3d friendly list
    {
        MyInventory = DictMyInventory.Select(i => new ItemStacks() { Item = i.Key, Quantity = i.Value }).ToList();
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
        yield return null;
    }

    IEnumerator OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == CollectableByTag && collider.GetType() == typeof(BoxCollider2D))
        {
            yield return null; // wait a frame to let PlaysAudio check current item levels before this modifies RapunzelManager.PlayerInventory
            var collidedInventory = collider.gameObject.GetComponent<Inventory>();

            // if inventories can be merged, destroy this game object as it has been added to the other inventory
            if (mergeInventory(DictMyInventory, collidedInventory.DictMyInventory))
            {
                // Debug.Log(this.name + "'s merged with " + collider.gameObject.name);
                collidedInventory.UpdateInspectorInventory();
                Destroy(this.gameObject);
            }
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.GetType() == typeof(BoxCollider2D))
    //    {
    //        // do stuff only for the box collider on the object
    //    }
    //    else if (collision.collider.GetType() == typeof(CircleCollider2D))
    //    {
    //        // do stuff only for the circle collider on the object
    //    }
    //}

    private bool mergeInventory(Dictionary<CollectableItems, int> fromInventory, Dictionary<CollectableItems, int> toInventory)
    {
        // ensure toInventory doesn't end up with stacks of negative values after a merge
        Dictionary<CollectableItems, int> subtractiveInventory = fromInventory.Where(d => d.Value < 0).ToDictionary(i => i.Key, i => i.Value);
        foreach (var item in subtractiveInventory)
        {
            if (toInventory.ContainsKey(item.Key) == false || (toInventory[item.Key] + item.Value) < 0)
            {
                return false;
            }
        }

        // merge inventory
        foreach (var item in fromInventory)
        {
            // add or remove NumberOfItems of ItemType to the players inventory
            if (toInventory.ContainsKey(item.Key))
            {
                // Debug.Log("Added " + item.Value + " to " + toInventory[item.Key]);
                toInventory[item.Key] += item.Value;
                if (toInventory[item.Key] == 0)
                    toInventory.Remove(item.Key);
            }
            else
            {
                // Debug.Log("Added " + item.Value + " to empty");
                toInventory.Add(item.Key, item.Value);
            }
        }

        return true;
    }


    //private void addInventory(ItemStacks item, Dictionary<CollectableItems, int> toInventory)
    //{
    //    if (toInventory.ContainsKey(item.Item) && (toInventory[item.Item] + item.Quantity) >= 0)
    //    {
    //        toInventory[item.Item] += item.Quantity;
    //    }
    //}

    //private bool hasNumber(CollectableItems itemType, int requiredQuanity)
    //{
    //    var numInInventory = RapunzelManager.PlayerInventory.Where(i => i == itemType).Count();
    //    return numInInventory >= requiredQuanity;
    //}
}
