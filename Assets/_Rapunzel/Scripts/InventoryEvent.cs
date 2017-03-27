using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Events;

/// <summary>
/// Maps sounds to what items are required to make each of sound
/// </summary>
[Serializable]
public class InventoryRequired
{
    [Help("0 to n items required to be in the colliding object for this event to trigger.", UnityEditor.MessageType.None)]
    public List<CollectableItems> FlagsRequiredToPlay;
    public UnityEvent ActionOnMatch;
}

public class InventoryEvent : MonoBehaviour
{
    [Help("This component executes events based on Inventory items that collide with this GameObject.\nEvery scene should have a single GameObject with an InventoryAction component that InventoryEvents can use to preform their actions", UnityEditor.MessageType.None)]
    [Tooltip("Routes PlayAudio. Allows InventoryAction UnityEvent handlers to be stored in a prefab")]
    public InventoryAction ActionManager;
    public List<InventoryRequired> EventByInventoryMatch;

    void Start()
    {
    }

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var inventoryCollidedInto = collider.gameObject.GetComponent<Inventory>();

        // Determine which, if any, InventoryEvent to raise
        if (inventoryCollidedInto != null
            && collider.GetType() == typeof(BoxCollider2D))
        {
            InvokeEventUsing(inventoryCollidedInto);
        }
    }

    #region [ Route UnityEvents to ActionManager ]
    public void PlayAudio(AudioClip clip)
    {
        ActionManager.PlayAudio(clip);
    }

    public void SetActiveFalse(GameObject targetGameObject)
    {
        ActionManager.SetActiveFalse(targetGameObject);
    }

    public void LoadScene(int sceneIndex)
    {
        ActionManager.LoadScene(sceneIndex);
    }

    public void CenterOnParent(Transform target)
    {
        ActionManager.CenterOnParent(target);
    }
    #endregion [ Route UnityEvents to ActionManager ]

    public void InvokeEventUsing(Inventory listenersInventory)
    {
        // filter down InventoryEvents to only those that the player meets the requirements with their Inventory.
        var availableEvents = EventByInventoryMatch
            .Where(speakerClip => hasInventoryFor(speakerClip, listenersInventory)).ToList();

        if (availableEvents.Count == 0) // InventoryEvent to raise
            return;

        // futher filter by chosing the single InventoryEvent that has the most flags required met.
        var maxInventory = availableEvents
            .OrderByDescending(inventoryEvent => inventoryEvent.FlagsRequiredToPlay.Count)
            .First();

        maxInventory.ActionOnMatch.Invoke();
    }

    /// <summary> Determines if a storyClip can be played with the players current StoryInventory </summary>
    bool hasInventoryFor(InventoryRequired storyClip, Inventory listenersInventory)
    {
        var inventoryCollidedWith = listenersInventory.DictMyInventory.Keys.ToList();
        return ContainsAll(inventoryCollidedWith, storyClip.FlagsRequiredToPlay);
    }

    // http://stackoverflow.com/questions/1520642/does-net-have-a-way-to-check-if-list-a-contains-all-items-in-list-b
    bool ContainsAll<T>(IEnumerable<T> largeList, IEnumerable<T> ofList)
    {
        return ofList.Except(largeList).Any() == false;
    }
}
