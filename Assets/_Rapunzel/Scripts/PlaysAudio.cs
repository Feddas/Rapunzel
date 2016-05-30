using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// Maps sounds to what items are required to make each of sound
/// </summary>
[Serializable]
public class StoryAudio
{
    public AudioClip AudioClip;
    public List<CollectableItems> FlagsRequiredToPlay;
}

public class PlaysAudio : MonoBehaviour
{
    public AudioSource noiseMaker;
    public List<StoryAudio> SpeakingList;

    void Start()
    {
    }
    
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var inventoryCollidedInto = collision.gameObject.GetComponent<Inventory>();
        if (inventoryCollidedInto != null
            //&& collision.gameObject.tag == "Player"
            && collision.collider.GetType() == typeof(BoxCollider2D))
        {
            PlayAudioFor(inventoryCollidedInto);
        }
        else
        {
            // should it play audio with 0 FlagsRequiredToPlay. This will mean it plays sounds when colliding with the ground.
        }
        // else if (collision.gameObject.tag == "Player") // another option for play audio with 0 FlagsRequiredToPlay that won't play sounds with the ground
    }

    public void PlayAudioFor(Inventory listenersInventory)
    {
        // filter down all speakers clips to only those that the player can hear. What they can hear depends on what they have in their StoryInventory
        var availableClips = SpeakingList
            .Where(speakerClip => hasInventoryFor(speakerClip, listenersInventory)).ToList();

        if (availableClips.Count == 0) // no audio to play
            return;

        // futher filter by chosing the single clip that has the most flags required to be played.
        var maxInventory = availableClips
            .OrderByDescending(speakerClip => speakerClip.FlagsRequiredToPlay.Count)
            .First();

        //Debug.Log("Playing " + maxInventory.AudioClip.name);
        noiseMaker.PlayOneShot(maxInventory.AudioClip);
    }

    /// <summary> Determines if a storyClip can be played with the players current StoryInventory </summary>
    bool hasInventoryFor(StoryAudio storyClip, Inventory listenersInventory)
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
