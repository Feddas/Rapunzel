using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Items that can be used to progress the story
/// </summary>
public enum CollectableItems
{
    Coin,
    Key,
}

/// <summary>
/// Things that can make sounds
/// </summary>
public enum Speaker
{
    Toad,
    Rock,
    Door,
    Key,
    Mom,
    Coin,
    CameleonFamily,
}

public class RapunzelManager : MonoBehaviour
{
    public List<CollectableItems> PlayerInventory;

    void Start()
    {
    }
    
    void Update()
    {
    }
}
