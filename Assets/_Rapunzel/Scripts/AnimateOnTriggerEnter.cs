using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimateOnTriggerEnter : MonoBehaviour
{
    public string TagOfConsumer = "Player";

    [Tooltip("Animator variable that is set to true if an item can be consumed."
    + " Set to false on TriggerEnter, which should start an animation.")]
    public string AnimReadyVariable = "Ready";

    private Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    IEnumerator OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == TagOfConsumer && collider.GetType() == typeof(BoxCollider2D))
        {
            yield return null;

            anim.SetBool(AnimReadyVariable, false);
        }
    }
}
