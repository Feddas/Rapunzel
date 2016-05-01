using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// Maps things that can make sounds to what items are required for them to make each of their sounds
/// </summary>
[Serializable]
public class StoryAudio
{
    public Speaker Speaker;
    public AudioClip AudioClip;
    public List<CollectableItems> FlagsRequiredToPlay;
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public RapunzelManager RapunzelManager;
    public List<StoryAudio> AudioSounds;

    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
    
    void Update()
    {
    }

    public void PlayAudioFor(Speaker speaker)
    {
        // find all audio clips that belong to speaker
        var speakersAudio = AudioSounds.Where(a => a.Speaker == speaker);

        // filter down all speakers clips to only those that the player can hear. What they can hear depends on what they have in their StoryInventory
        var availableClips = speakersAudio
            .Where(speakerClip => hasInventoryFor(speakerClip)).ToList();

        if (availableClips.Count == 0) // no audio to play
            return;

        // futher filter by chosing the single clip that has the most flags required to be played.
        var maxInventory = availableClips
            .OrderByDescending(speakerClip => speakerClip.FlagsRequiredToPlay.Count)
            .First();

        Debug.Log("Playing " + maxInventory.AudioClip.name);
        audioSource.PlayOneShot(maxInventory.AudioClip);
    }

    /// <summary> Determines if a storyClip can be played with the players current StoryInventory </summary>
    bool hasInventoryFor(StoryAudio storyClip)
    {
        return ContainsAll(RapunzelManager.PlayerInventory, storyClip.FlagsRequiredToPlay);
    }

    // http://stackoverflow.com/questions/1520642/does-net-have-a-way-to-check-if-list-a-contains-all-items-in-list-b
    bool ContainsAll<T>(IEnumerable<T> largeList, IEnumerable<T> ofList)
    {
        return ofList.Except(largeList).Any() == false;
    }
}
