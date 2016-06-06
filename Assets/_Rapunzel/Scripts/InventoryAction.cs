using UnityEngine;
using System.Collections;

public class InventoryAction : MonoBehaviour
{
    [Tooltip("Only needs to be set if PlayAudio() is used")]
    public AudioSource NoiseMaker;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LoadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        NoiseMaker.PlayOneShot(audioClip);
    }
}
