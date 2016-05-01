using UnityEngine;
using System.Collections;

public class PlaysAudio : MonoBehaviour
{
    public Speaker SpeakingAs;

    public AudioManager PlayUsing;
    
    void Start()
    {

    }
    
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(this.name + "'s PlaysAudio collided with " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            PlayUsing.PlayAudioFor(SpeakingAs);
        }
    }
}
